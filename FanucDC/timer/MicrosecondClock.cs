using System.Diagnostics;

public class MicrosecondClock
{
    private readonly Stopwatch stopwatch; private readonly DateTime startTime;
    public MicrosecondClock() { stopwatch = new Stopwatch(); startTime = DateTime.Now; stopwatch.Start(); }
    public DateTime Now { get { long ticks = stopwatch.Elapsed.Ticks; return startTime.AddTicks(ticks); } }
    public string GetFormattedTime() { DateTime now = Now; long microseconds = stopwatch.Elapsed.Ticks / (TimeSpan.TicksPerMillisecond / 1000); return $"{now:yyyy-MM-dd HH:mm:ss}.{microseconds:D6}"; }
}
