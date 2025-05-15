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
        public PagedList<Task> GetTasks(TasksParameters tasksParameters)
        {
            var tasks = GetAll().OrderBy(task => task.Id).AsQueryable();
            var orderedTasks = PagedList<Task>.ToPagedList(tasks, tasksParameters.PageNumber, tasksParameters.PageSize);
            return orderedTasks;
        }

        public IEnumerable<Task> GetTasksForDate(string id, string date)
        {
            return _context.Tasks.AsNoTracking().Where(task => task.UserId == id && task.Date == date).ToList();
        }
    }
}
