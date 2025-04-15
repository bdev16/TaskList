using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskList.Data;
using TaskList.DTOs;
using TaskList.Model;
using TaskList.Repositories;
using TaskList.DTOs;

namespace TaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork;

        public UserController(UnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            try
            {
                var users = _unityOfWork.UserRepository.GetAll();
                if (!users.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "Nenhuma usuario foi criado até o momento..."});
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            try
            {
                var user = _unityOfWork.UserRepository.Get(user => user.Id == id);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "O Id informado não corresponde a nenhum dos usuarios cadastrados..."});
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        //[Authorize]
        [HttpGet("tasks")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserTasks()
        {
            try
            {
                var userTasks = _unityOfWork.UserRepository.GetUserTasks();
                if (!userTasks.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "" });
                }
                return Ok(userTasks);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
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

                _unityOfWork.UserRepository.Create(user);
                _unityOfWork.Commit();

                return new CreatedAtRouteResult("GetUser", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpPut("{id}")]
        public ActionResult<User> Put(string id, UserUpdateDTO userUpdate)
        {
            try
            {
                var user = _unityOfWork.UserRepository.Get(user => user.Id == id);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "O Id informado não corresponde a nenhum dos usuarios cadastrados..."});
                }

                if (!string.IsNullOrEmpty(userUpdate.UserName))
                    user.UserName = userUpdate.UserName;

                if (!string.IsNullOrEmpty(userUpdate.Email))
                    user.Email = userUpdate.Email;

                _unityOfWork.UserRepository.Update(user);
                _unityOfWork.Commit();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var user = _unityOfWork.UserRepository.Get(user => user.Id == id);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "Usuario não encontrado..."});

                }

                _unityOfWork.UserRepository.Delete(user);
                _unityOfWork.Commit();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

    }
}
