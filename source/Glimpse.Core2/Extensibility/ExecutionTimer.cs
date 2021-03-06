using System;
using System.Diagnostics;

namespace Glimpse.Core2.Extensibility
{
    public interface IExecutionTimer
    {
        TimerResult<T> Time<T>(Func<T> func);
        TimerResult Time(Action action);
    }

    public class ExecutionTimer : IExecutionTimer
    {
        internal Stopwatch Stopwatch { get; set; }

        public ExecutionTimer(Stopwatch stopwatch)
        {
            if (!stopwatch.IsRunning)
                stopwatch.Start();

            Stopwatch = stopwatch;
        }

        public TimerResult<T> Time<T>(Func<T> func)
        {
            var offset = Stopwatch.ElapsedMilliseconds;
            return new TimerResult<T>
                       {
                           Offset = offset,
                           Result = func(),
                           Duration = TimeSpan.FromMilliseconds(Stopwatch.ElapsedMilliseconds - offset)
                       };
        }

        public TimerResult Time(Action action)
        {
            var offset = Stopwatch.ElapsedMilliseconds;

            action();
            
            return new TimerResult
                       {
                           Offset = offset,
                           Duration = TimeSpan.FromMilliseconds(Stopwatch.ElapsedMilliseconds - offset)
                       };
        }
    }
}