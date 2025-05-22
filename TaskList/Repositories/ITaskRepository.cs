using TaskList.Model;
using TaskList.Pagination;
using Task = TaskList.Model.Task;

namespace TaskList.Repositories
{
    public interface ITaskRepository : IRepository<Task>  
    {
        PagedList<Task> GetTasks(TasksParameters tasksParameters);
        PagedList<Task> GetTasksTitleFilter(TasksTitleFilter tasksTitleFilter);
        PagedList<Task> GetTasksStatusFilter(TasksStatusFilter tasksStatusFilter);
        IEnumerable<Task> GetTasksForDate(string id, string date);
    }
}