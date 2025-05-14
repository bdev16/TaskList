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
using Task = TaskList.Model.Task;
using AutoMapper;
using TaskList.Pagination;

namespace TaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        public TaskController(IUnityOfWork unityOfWork, IMapper mapper)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
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

                var tasksDTO = _mapper.Map<IEnumerable<TaskDTO>>(tasks);

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

                var taskDTO = _mapper.Map<TaskDTO>(task);

                return Ok(taskDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpGet("{id},{date}", Name = "GetTasksForDate")]
        public ActionResult<IEnumerable<TaskDTO>> GetTasksForDate(string id, string date)
        {
            try
            {
                var tasks = _unityOfWork.TaskRepository.GetTasksForDate(id,date);

                if (!tasks.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "Não existe nenhum tarefa cadastrada com a data informada..."});
                }

                var tasksDTO = _mapper.Map<IEnumerable<TaskDTO>>(tasks);

                return Ok(tasksDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpGet("pagination")]
        public ActionResult<IEnumerable<TaskDTO>> Get([FromQuery] TasksParameters tasksParameters)
        {
            var tasks = _unityOfWork.TaskRepository.GetTasks(tasksParameters);

            var tasksDto = _mapper.Map<IEnumerable<TaskDTO>>(tasks);

            return Ok(tasksDto);
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

                var task = _mapper.Map<Task>(taskDTO);

                _unityOfWork.TaskRepository.Create(task);
                _unityOfWork.Commit();

                var newTaskDTO = _mapper.Map<TaskDTO>(task);

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

                var task = _mapper.Map<Task>(taskDTO);

                _unityOfWork.TaskRepository.Update(task);
                _unityOfWork.Commit();

                var updateTaskDTO = _mapper.Map<TaskDTO>(task);

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

                var removedTaskDTO = _mapper.Map<TaskDTO>(removedTask);

                return Ok(removedTaskDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }
    }
}
