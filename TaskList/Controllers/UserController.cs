using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("{id:int}", Name = "GetUser")]
        public ActionResult<User> Get(int id)
        {
            var user = _context.Users.FirstOrDefault(user => user.Id == id);
            if (user == null)
            {
                return NotFound("O Id informado não corresponde a nenhum dos usuarios cadastrados...");
            }
            return Ok(user);
        }

        [HttpGet("tasks")]
        public ActionResult<IEnumerable<User>> GetUserTasks()
        {
            var userTasks = _context.Users.Include(user => user.Tasks).ToList();
            if (!userTasks.Any())
            {
                return NotFound();
            }
            return Ok(userTasks);
        }

        [HttpPost()]
        public ActionResult<User> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetUser", new { id = user.Id }, user);
        }
    }
}
