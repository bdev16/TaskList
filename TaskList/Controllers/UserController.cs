using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskList.Data;
using TaskList.Model;
using TaskList.Repositories;

namespace TaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _repository;
        private readonly IUserRepository _userRepository;

        public UserController(IRepository<User> repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;

        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            try
            {
                var users =  _repository.GetAll();
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
                var user = _repository.Get(user => user.Id == id);
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

        //[Authorize]
        [HttpGet("tasks")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserTasks()
        {
            try
            {
                var userTasks = _userRepository.GetUserTasks();
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

                _repository.Create(user);

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

                _repository.Update(user);

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
                var user = _repository.Get(user => user.Id == id);
                if (user == null)
                {
                    return NotFound("Usuario não encontrado...");
                }

                _repository.Delete(user);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            }   
        }

    }
}
