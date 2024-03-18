namespace SequenceScheduler {
    public class TaskManager {
        public static List<ISessionStrategy> Tasks { get; private set; } = new();

        public TaskManager() {
            RegisterDefaultTask();
        }

        public void RegisterDefaultTask() {
            //Tasks.Add(new AlarmStrategy());

        }

        public void CreateTask(string taskName, Task task) {
            ISessionStrategy strategy;
        }

        public void RemoveTask(string taskName) {
            Tasks.RemoveAt(Tasks.FindIndex(x => x.TaskName == taskName));
        }


    }
}