using TaskManager.Models;

namespace TaskManager.Repository.Interface
{
    public interface ITaskListRepository
    {
        Task<IEnumerable<TaskList>> GetTaskList();
        Task<TaskList> GetTaskList(int taskId);
        Task<TaskList> AddTaskList(TaskList tasklist);
        Task<TaskList> UpdateTaskList(TaskList tasklist);
        void DeleteTaskList(int task);
    }
}
