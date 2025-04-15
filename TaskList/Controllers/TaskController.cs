using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading.Tasks;
using TaskList.Data;
using TaskList.DTOs;
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
        private readonly IUnityOfWork _unityOfWork;
        public TaskController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        //[Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Task>> Get()
        {
            try
            {
                var tasks = _unityOfWork.TaskRepository.GetAll();
                if (!tasks.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "Nenhuma tarefa foi criada até o momento..."});
                }
                return Ok(tasks);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpGet("{id:int}", Name ="GetTask")]
        public ActionResult<Task> Get(int id)
        {
            try
            {
                var task = _unityOfWork.TaskRepository.Get(task => task.Id == id);
                if (task is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "O Id informado não corresponde a nenhuma das tarefas cadastradas..."});
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpGet("{id},{date}", Name = "GetTasksForDate")]
        public ActionResult<Task> GetTasksForDate(string id, string date)
        {
            try
            {

                var tasks = _unityOfWork.TaskRepository.GetTasksForDate(id,date);

                if (!tasks.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "Não existe nenhum tarefa cadastrada com a data informada..."});
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
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

                _unityOfWork.TaskRepository.Create(task);
                _unityOfWork.Commit();

                return new CreatedAtRouteResult("GetTask", new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Task task) 
        {
            try
            {
                if (id != task.Id)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO { Status = "Error", Message = "O Id informado não corresponde a nenhuma das tarefas cadastradas..."});
                }

                _unityOfWork.TaskRepository.Update(task);
                _unityOfWork.Commit();

                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) 
        {
            try
            {
                var task = _unityOfWork.TaskRepository.Get(task => task.Id == id);
                if (task is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "Tarefa não encontrada..."});
                }

                _unityOfWork.TaskRepository.Delete(task);
                _unityOfWork.Commit();

                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }
    }
}
