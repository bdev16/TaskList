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
            var tasks = _context.Tasks.ToList();
            if (!tasks.Any())
            {
                return NotFound("Nenhuma tarefa foi criada até o momento...");
            }
            return Ok(tasks);
        }

        [HttpGet("{id:int}", Name ="GetTask")]
        public ActionResult<Task> Get(int id)
        {
            var task = _context.Tasks.FirstOrDefault(task => task.Id == id);
            if (task is null)
            {
                return NotFound("O Id informado não corresponde a nenhuma das tarefas cadastradas...");
            }
            return Ok(task);
        }

        [HttpPost]
        public ActionResult Post(Task task)
        {

            if (task is null)
            {
                return BadRequest();
            }

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetTask", new { id = task.Id }, task);

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Task task) 
        {
            if (id != task.Id) 
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(task);
        }
    }
}
