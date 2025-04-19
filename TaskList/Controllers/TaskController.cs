using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading.Tasks;
using TaskList.Data;
using TaskList.DTOs;
using TaskList.DTOs.Extensions;
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
        public ActionResult<IEnumerable<TaskDTO>> Get()
        {
            try
            {
                var tasks = _unityOfWork.TaskRepository.GetAll();
                if (!tasks.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "Nenhuma tarefa foi criada até o momento..."});
                }

                var tasksDTO = TaskDTOMappingExtensions.ToTaskDTOList(tasks);

                return Ok(tasksDTO);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpGet("{id:int}", Name ="GetTask")]
        public ActionResult<TaskDTO> Get(int id)
        {
            try
            {
                var task = _unityOfWork.TaskRepository.Get(task => task.Id == id);
                if (task is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "O Id informado não corresponde a nenhuma das tarefas cadastradas..."});
                }

                var taskDTO = TaskDTOMappingExtensions.ToTaskDTO(task);

                return Ok(taskDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpGet("{id},{date}", Name = "GetTasksForDate")]
        public ActionResult<TaskDTO> GetTasksForDate(string id, string date)
        {
            try
            {
                var tasks = _unityOfWork.TaskRepository.GetTasksForDate(id,date);

                if (!tasks.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "Não existe nenhum tarefa cadastrada com a data informada..."});
                }

                var tasksDTO = TaskDTOMappingExtensions.ToTaskDTOList(tasks);

                return Ok(tasksDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpPost]
        public ActionResult<TaskDTO> Post(TaskDTO taskDTO)
        {
            try
            {
                if (taskDTO is null)
                {
                    return BadRequest();
                }

                var task = TaskDTOMappingExtensions.ToTask(taskDTO);

                _unityOfWork.TaskRepository.Create(task);
                _unityOfWork.Commit();

                var newTaskDTO = TaskDTOMappingExtensions.ToTaskDTO(task);

                return Ok(newTaskDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, TaskDTO taskDTO) 
        {
            try
            {
                if (id != taskDTO.Id)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO { Status = "Error", Message = "O Id informado não corresponde a nenhuma das tarefas cadastradas..."});
                }

                var task = TaskDTOMappingExtensions.ToTask(taskDTO);

                _unityOfWork.TaskRepository.Update(task);
                _unityOfWork.Commit();

                var updateTaskDTO = TaskDTOMappingExtensions.ToTaskDTO(task);

                return Ok(updateTaskDTO);
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

                var removedTask = _unityOfWork.TaskRepository.Delete(task);
                _unityOfWork.Commit();

                var removedTaskDTO = TaskDTOMappingExtensions.ToTaskDTO(task);

                return Ok(removedTaskDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }
    }
}
