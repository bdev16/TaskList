using Microsoft.EntityFrameworkCore;
using TaskList.Data;
using TaskList.Model;
using TaskList.Repositories;

namespace TaskList.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<User> GetUserTasks()
        {
            return _context.Users.AsNoTracking().Include(user => user.Tasks).ToList();
        }
    }
}
