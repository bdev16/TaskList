using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskList.Model;
using TaskList.Repositories;

namespace TaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDateController : ControllerBase
    {
        private readonly IRepository<TaskDate> _repository;

        public TaskDateController(IRepository<TaskDate> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskDate>> Get()
        {
            try
            {
                var taskDates = _repository.GetAll();
                if (!taskDates.Any())
                {
                    return NotFound("Nenhuma tarefa foi criada até o momento...");
                }
                return Ok(taskDates);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            }
        }

        [HttpPost()]
        public ActionResult<TaskDate> Post(TaskDate taskDate)
        {
            try
            {
                if (taskDate == null)
                {
                    return BadRequest();
                }

                _repository.Create(taskDate);

                return new CreatedAtRouteResult("GetUser", new { id = taskDate.Id }, taskDate);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tentar tratar a sua solicitação...");
            }
        }
    }
}
