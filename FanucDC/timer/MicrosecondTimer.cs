using System.Diagnostics;

public class MicrosecondTimer : IDisposable
{
    private readonly Stopwatch stopwatch;
    private long interval;
    private bool enabled; 
    private Task timerTask; 
    private CancellationTokenSource cancellationTokenSource;
    public event EventHandler<MicrosecondTimerEventArgs> Elapsed;
    public MicrosecondTimer()
    {
        stopwatch = new Stopwatch(); 
        interval = Stopwatch.Frequency / 1000; // 默认1毫秒
        enabled = false;
    }
    public bool Enabled { get => enabled; set { if (enabled != value) { enabled = value; if (enabled) Start(); else Stop(); } } }
    public long Interval { get => interval * 1000000 / Stopwatch.Frequency; set => interval = value * Stopwatch.Frequency / 1000000; }
    private void Start() { cancellationTokenSource = new CancellationTokenSource(); timerTask = Task.Run(TimerLoop, cancellationTokenSource.Token); }
    private void Stop() { cancellationTokenSource?.Cancel(); timerTask?.Wait(); }
    private async Task TimerLoop() { stopwatch.Restart(); while (!cancellationTokenSource.Token.IsCancellationRequested) { long elapsedTicks = stopwatch.ElapsedTicks; if (elapsedTicks >= interval) { OnElapsed(new MicrosecondTimerEventArgs(elapsedTicks * 1000000 / Stopwatch.Frequency)); stopwatch.Restart(); } await Task.Delay(1); } }
    protected virtual void OnElapsed(MicrosecondTimerEventArgs e) { Elapsed?.Invoke(this, e); }
    public void Dispose() { Stop(); cancellationTokenSource?.Dispose(); }
}
public class MicrosecondTimerEventArgs : EventArgs
{
    public long ElapsedMicroseconds { get; }
    public MicrosecondTimerEventArgs(long elapsedMicroseconds) { ElapsedMicroseconds = elapsedMicroseconds; }
}
