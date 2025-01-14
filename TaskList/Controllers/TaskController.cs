using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskList.Data;
using Task = TaskList.Model.Task;

namespace TaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Task>> Get()
        {
            try
            {
                var tasks = _context.Tasks.AsNoTracking().ToList();
                if (!tasks.Any())
                {
                    return NotFound("Nenhuma tarefa foi criada até o momento...");
                }
                return Ok(tasks);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            }
        }

        [HttpGet("{id:int}", Name ="GetTask")]
        public ActionResult<Task> Get(int id)
        {
            try
            {
                var task = _context.Tasks.AsNoTracking().FirstOrDefault(task => task.Id == id);
                if (task is null)
                {
                    return NotFound("O Id informado não corresponde a nenhuma das tarefas cadastradas...");
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            }
        }

        [HttpPost]
        public ActionResult Post(Task task)
        {
            try
            {
                if (task is null)
                {
                    return BadRequest();
                }

                _context.Tasks.Add(task);
                _context.SaveChanges();

                return new CreatedAtRouteResult("GetTask", new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Task task) 
        {
            try
            {
                if (id != task.Id)
                {
                    return BadRequest();
                }

                _context.Entry(task).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(task);
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
                var task = _context.Tasks.FirstOrDefault(task => task.Id == id);
                if (task is null)
                {
                    return NotFound("Tarefa não encontrada...");
                }

                _context.Tasks.Remove(task);
                _context.SaveChanges();

                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            }
        }
    }
}
