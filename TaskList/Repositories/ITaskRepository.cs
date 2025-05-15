using TaskList.Model;
using TaskList.Pagination;
using Task = TaskList.Model.Task;

namespace TaskList.Repositories
{
    public interface ITaskRepository : IRepository<Task>  
    {
        PagedList<Task> GetTasks(TasksParameters tasksParameters);
        IEnumerable<Task> GetTasksForDate(string id, string date);
    }
}