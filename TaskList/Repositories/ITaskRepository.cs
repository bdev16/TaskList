using TaskList.Model;
using Task = TaskList.Model.Task;

namespace TaskList.Repositories
{
    public interface ITaskRepository : IRepository<Task>  
    {
    }
}