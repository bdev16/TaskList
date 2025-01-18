using TaskList.Model;

namespace TaskList.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUserTasks();
    }
}
