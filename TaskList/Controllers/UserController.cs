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
            try
            {
                var users = _context.Users.AsNoTracking().ToList();
                if (!users.Any())
                {
                    return NotFound("Nenhuma tarefa foi criada até o momento...");
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            }    
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        public ActionResult<User> Get(int id)
        {
            try
            {
                var user = _context.Users.AsNoTracking().FirstOrDefault(user => user.Id == id);
                if (user == null)
                {
                    return NotFound("O Id informado não corresponde a nenhum dos usuarios cadastrados...");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            }
        }

        [HttpGet("tasks")]
        public ActionResult<IEnumerable<User>> GetUserTasks()
        {
            try
            {
                var userTasks = _context.Users.AsNoTracking().Include(user => user.Tasks).ToList();
                if (!userTasks.Any())
                {
                    return NotFound();
                }
                return Ok(userTasks);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            }
        }

        [HttpPost()]
        public ActionResult<User> Post(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                _context.Users.Add(user);
                _context.SaveChanges();

                return new CreatedAtRouteResult("GetUser", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            }  
        }

        [HttpPut("{id:int}")]
        public ActionResult<User> Put(int id, User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return BadRequest();
                }

                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            } 
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(user => user.Id == id);
                if (user == null)
                {
                    return NotFound("Usuario não encontrado...");
                }

                _context.Users.Remove(user);
                _context.SaveChanges();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            }   
        }

    }
}
