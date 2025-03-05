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
    }
}
