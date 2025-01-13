using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskList.Data;
using TaskList.Model;

namespace TaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _context.Users.ToList();
            if (!users.Any())
            {
                return NotFound("Nenhuma tarefa foi criada até o momento...");
            }
            return Ok(users);
        }
    }
}
