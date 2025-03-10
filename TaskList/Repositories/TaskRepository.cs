using Microsoft.EntityFrameworkCore;
using TaskList.Data;
using Task = TaskList.Model.Task;


namespace TaskList.Repositories
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Task> GetTasksForDate(string id, string date)
        {
            return _context.Tasks.AsNoTracking().Where(task => task.UserId == id && task.Date == date).ToList();
        }
    }
}
