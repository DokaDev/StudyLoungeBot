namespace SequenceScheduler {
    public interface ISessionStrategy {
        public string TaskName { get; set; }
        public string? Author { get; set; }
        public Task Task { get; set; }
    }
}