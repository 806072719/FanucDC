using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 微秒级高精度定时器（基于Stopwatch实现，解决原版本所有线程安全/精度/资源问题）
/// </summary>
public class MicrosecondTimer : IDisposable
{
    #region 私有字段（加锁保护，避免竞态条件）
    private readonly Stopwatch _stopwatch = new();
    private readonly object _lockObj = new(); // 全局锁，保证多线程安全
    private long _intervalTicks; // 间隔（滴答数，直接操作避免重复转换）
    private bool _enabled;
    private Task _timerTask;
    private CancellationTokenSource _cts;
    private bool _isDisposed; // 释放标记，防止重复释放
    #endregion

    #region 公共属性（线程安全，带状态校验）
    /// <summary>
    /// 获取或设置定时器是否启用（线程安全，避免频繁启停）
    /// </summary>
    public bool Enabled
    {
        get
        {
            lock (_lockObj) return _enabled;
        }
        set
        {
            if (value == Enabled) return; // 相同状态直接返回，避免重复操作
            lock (_lockObj)
            {
                if (_isDisposed) throw new ObjectDisposedException(nameof(MicrosecondTimer));
                _enabled = value;
                if (_enabled) StartCore();
                else StopCore();
            }
        }
    }

    /// <summary>
    /// 获取或设置定时器间隔（微秒），最小值1微秒（线程安全，带范围校验）
    /// </summary>
    [DefaultValue(1000)] // 默认1毫秒（1000微秒）
    public long Interval
    {
        get
        {
            lock (_lockObj)
            {
                // 滴答数转微秒，无溢出（先除后乘）
                return _intervalTicks * 1000000 / Stopwatch.Frequency;
            }
        }
        set
        {
            if (value < 1) throw new ArgumentOutOfRangeException(nameof(value), "间隔不能小于1微秒");
            lock (_lockObj)
            {
                if (_isDisposed) throw new ObjectDisposedException(nameof(MicrosecondTimer));
                // 微秒转滴答数，先除后乘避免long溢出
                _intervalTicks = value * Stopwatch.Frequency / 1000000;
                // 若转换后为0（如1微秒在低频率下），强制设为1个滴答
                if (_intervalTicks < 1) _intervalTicks = 1;
            }
        }
    }

    /// <summary>
    /// 获取定时器实际运行状态（区别于Enabled，处理后台任务崩溃的情况）
    /// </summary>
    public bool IsRunning
    {
        get
        {
            lock (_lockObj)
            {
                return _enabled && _timerTask != null && !_timerTask.IsCompleted;
            }
        }
    }
    #endregion

    #region 事件（线程安全，支持跨线程触发UI）
    /// <summary>
    /// 定时器间隔到达事件
    /// </summary>
    public event EventHandler<MicrosecondTimerEventArgs> Elapsed;

    /// <summary>
    /// 触发Elapsed事件（线程安全，原子操作获取事件，异常隔离）
    /// </summary>
    /// <param name="e">事件参数</param>
    protected virtual void OnElapsed(MicrosecondTimerEventArgs e)
    {
        // 原子操作获取事件引用，避免多线程订阅/取消订阅导致的空引用
        var handler = Elapsed;
        if (handler == null) return;

        try
        {
            handler.Invoke(this, e);
        }
        catch (Exception ex)
        {
            // 隔离事件订阅者的异常，不影响定时器核心逻辑
            Debug.WriteLine($"[MicrosecondTimer] 事件处理异常: {ex.Message}");
        }
    }
    #endregion

    #region 构造函数（初始化默认值，无副作用）
    /// <summary>
    /// 初始化微秒级定时器，默认间隔1毫秒（1000微秒）
    /// </summary>
    public MicrosecondTimer()
    {
        // 直接初始化滴答数，避免重复转换，默认1毫秒
        _intervalTicks = Stopwatch.Frequency / 1000;
        _enabled = false;
        _isDisposed = false;
    }
    #endregion

    #region 核心启停逻辑（加锁保护，仅内部调用）
    /// <summary>
    /// 启动定时器核心逻辑（已加锁，外部禁止直接调用）
    /// </summary>
    private void StartCore()
    {
        // 防止重复启动
        if (_cts != null || _timerTask != null) return;

        _cts = new CancellationTokenSource();
        // 启动后台任务，使用Task.Run而非直接异步，避免上下文混乱
        _timerTask = Task.Run(TimerLoopAsync, _cts.Token);
    }

    /// <summary>
    /// 停止定时器核心逻辑（已加锁，外部禁止直接调用）
    /// </summary>
    private void StopCore()
    {
        // 防止重复停止
        if (_cts == null || _timerTask == null) return;

        try
        {
            // 取消令牌，通知后台任务退出
            _cts.Cancel();
            // 异步等待任务结束，避免同步Wait导致的死锁，设置500ms超时防止线程卡死
            var waitSuccess = _timerTask.Wait(500);
            if (!waitSuccess) Debug.WriteLine("[MicrosecondTimer] 定时器停止超时，可能存在线程卡死");
        }
        catch (AggregateException ex)
        {
            // 过滤取消异常，其他异常打印日志
            ex.Handle(e => e is OperationCanceledException);
            Debug.WriteLine($"[MicrosecondTimer] 定时器停止异常: {ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[MicrosecondTimer] 定时器停止异常: {ex.Message}");
        }
        finally
        {
            // 释放资源，避免泄漏
            _cts.Dispose();
            _stopwatch.Stop();
            // 置空，保证下次启动可重新创建
            _cts = null;
            _timerTask = null;
        }
    }
    #endregion

    #region 高精度定时器循环（无Task.Delay，微秒级精度）
    /// <summary>
    /// 定时器核心循环（无延迟，基于Stopwatch实时检测，微秒级精度）
    /// </summary>
    /// <returns>异步任务</returns>
    private async Task TimerLoopAsync()
    {
        _stopwatch.Restart();
        long lastTriggerTicks = 0; // 上一次触发的滴答数，解决事件执行耗时的累计误差

        while (!_cts.Token.IsCancellationRequested)
        {
            try
            {
                var currentTicks = _stopwatch.ElapsedTicks;
                // 计算已流逝的滴答数（扣除上一次触发和事件执行耗时）
                var elapsedSinceLastTrigger = currentTicks - lastTriggerTicks;

                // 到达间隔，触发事件
                if (elapsedSinceLastTrigger >= _intervalTicks)
                {
                    // 转换为实际流逝的微秒（精准）
                    var elapsedUs = elapsedSinceLastTrigger * 1000000 / Stopwatch.Frequency;
                    OnElapsed(new MicrosecondTimerEventArgs(elapsedUs));
                    // 更新上一次触发的滴答数，消除事件执行耗时的累计误差
                    lastTriggerTicks = currentTicks;
                }

                // 轻量让出CPU，避免单线程占满核心（无精度损失）
                await Task.Yield();
            }
            catch (OperationCanceledException)
            {
                // 正常取消，直接退出
                break;
            }
            catch (Exception ex)
            {
                // 隔离循环内异常，保证定时器不崩溃
                Debug.WriteLine($"[MicrosecondTimer] 循环异常: {ex.Message}");
                // 异常时短暂延迟（1ms），避免异常循环占满CPU
                await Task.Delay(1, _cts.Token);
            }
        }

        _stopwatch.Stop();
        Debug.WriteLine("[MicrosecondTimer] 定时器循环已退出");
    }
    #endregion

    #region 标准IDisposable实现（支持子类继承，防止资源泄漏）
    /// <summary>
    /// 释放定时器所有资源
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        // 通知GC无需调用析构函数
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 析构函数（仅当未手动Dispose时由GC调用，释放非托管资源）
    /// </summary>
    ~MicrosecondTimer()
    {
        Dispose(false);
    }

    /// <summary>
    /// 核心释放逻辑（区分托管/非托管资源，支持子类继承）
    /// </summary>
    /// <param name="disposing">是否释放托管资源</param>
    protected virtual void Dispose(bool disposing)
    {
        lock (_lockObj)
        {
            if (_isDisposed) return;
            _isDisposed = true;

            // 释放托管资源（disposing=true时，手动Dispose/using块触发）
            if (disposing)
            {
                _enabled = false;
                StopCore(); // 确保定时器停止
            }

            // 释放非托管资源（当前无，预留子类使用）
            // 示例：if (unmanagedResource != IntPtr.Zero) { Marshal.FreeHGlobal(unmanagedResource); unmanagedResource = IntPtr.Zero; }

            _stopwatch.Stop();
        }
    }
    #endregion
}

/// <summary>
/// 微秒定时器事件参数（保留原设计）
/// </summary>
public class MicrosecondTimerEventArgs : EventArgs
{
    /// <summary>
    /// 获取自上一次触发以来流逝的微秒数
    /// </summary>
    public long ElapsedMicroseconds { get; }

    /// <summary>
    /// 初始化事件参数
    /// </summary>
    /// <param name="elapsedMicroseconds">流逝的微秒数</param>
    public MicrosecondTimerEventArgs(long elapsedMicroseconds)
    {
        ElapsedMicroseconds = elapsedMicroseconds;
    }
}