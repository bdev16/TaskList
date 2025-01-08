using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskList.Data;

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


    }
}
