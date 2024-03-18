namespace SequenceScheduler;

public class Session {
    public string TaskName { get; set; }
    public string Author { get; set; }
    public Task? Task { get; set; }

    public Session(string taskName, Task task) {
        TaskName = taskName;
        Task = task;

        if(Task is null)
            throw new ArgumentNullException(nameof(Task), "Task cannot be null.");

        // run the task
        Task.Run(async delegate {
            await Task;
        });
    }
}