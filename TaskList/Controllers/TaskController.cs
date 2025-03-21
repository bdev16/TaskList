using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading.Tasks;
using TaskList.Data;
using TaskList.Model;
using TaskList.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Task = TaskList.Model.Task;

namespace TaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IRepository<Task> _repository;
        private readonly ITaskRepository _taskRepository;
        public TaskController(IRepository<Task> repository, ITaskRepository taskRepository)
        {
            _repository = repository;
            _taskRepository = taskRepository;
        }

        //[Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Task>> Get()
        {
            try
            {
                var tasks =  _repository.GetAll();
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
                var task = _repository.Get(task => task.Id == id);
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

        [HttpGet("{id},{date}", Name = "GetTasksForDate")]
        public ActionResult<Task> GetTasksForDate(string id, string date)
        {
            try
            {

                var tasks = _taskRepository.GetTasksForDate(id,date);

                if (!tasks.Any())
                {
                    return NotFound("Não existe nenhum tarefa cadastrada com a data informada...");
                }

                return Ok(tasks);
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

                _repository.Create(task);

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

                _repository.Update(task);

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
                var task = _repository.Get(task => task.Id == id);
                if (task is null)
                {
                    return NotFound("Tarefa não encontrada...");
                }

                _repository.Delete(task);

                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            }
        }
    }
}
