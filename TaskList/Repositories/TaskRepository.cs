using Microsoft.EntityFrameworkCore;
using TaskList.Data;
using TaskList.Pagination;
using Task = TaskList.Model.Task;


namespace TaskList.Repositories
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Task> GetTasks(TasksParameters tasksParameters)
        {
            return GetAll()
                .OrderBy(task => task.Title)
                .Skip((tasksParameters.PageNumber - 1) * tasksParameters.PageSize)
                .Take(tasksParameters.PageSize).ToList();
        }

        public IEnumerable<Task> GetTasksForDate(string id, string date)
        {
            return _context.Tasks.AsNoTracking().Where(task => task.UserId == id && task.Date == date).ToList();
        }
    }
}
