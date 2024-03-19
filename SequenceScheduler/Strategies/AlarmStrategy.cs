using System.Diagnostics;

namespace SequenceScheduler.Strategies {
    public class AlarmStrategy : IDisposable {
        public Timer? Timer { get; private set; }



        public AlarmStrategy(TimeSpan due, TimeSpan interval) {
            Timer = new(OnTimerElpased, null, due, interval);
        }

        private void OnTimerElpased(object? state) {
            Debug.WriteLine("Timer elapsed");
        }

        public void Dispose() {
            Timer?.Dispose();
        }
    }
}