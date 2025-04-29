using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskList.Data;
using TaskList.DTOs;
using TaskList.Model;
using TaskList.Repositories;
using TaskList.DTOs;
using AutoMapper;

namespace TaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;

        public UserController(IUnityOfWork unityOfWork, IMapper mapper)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> Get()
        {
            try
            {
                var users = _unityOfWork.UserRepository.GetAll();
                if (!users.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "Nenhuma usuario foi criado até o momento..."});
                }

                var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(users);

                return Ok(usersDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<UserDTO> Get(string id)
        {
            try
            {
                var user = _unityOfWork.UserRepository.Get(user => user.Id == id);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "O Id informado não corresponde a nenhum dos usuarios cadastrados..."});
                }

                var userDTO = _mapper.Map<UserDTO>(user);

                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

        //[Authorize]
        [HttpGet("tasks")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUserTasks()
        {
            try
            {
                var userTasks = _unityOfWork.UserRepository.GetUserTasks();
                if (!userTasks.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "" });
                }

                var userTasksDTO = _mapper.Map<IEnumerable<UserDTO>>(userTasks);

                return Ok(userTasksDTO);
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
        public ActionResult<UserDTO> Put(string id, UserUpdateDTO userUpdateDTO)
        {
            try
            {
                var user = _unityOfWork.UserRepository.Get(user => user.Id == id);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "O Id informado não corresponde a nenhum dos usuarios cadastrados..."});
                }

                var userUpdate = _mapper.Map<User>(userUpdateDTO);

                if (!string.IsNullOrEmpty(userUpdate.UserName))
                    user.UserName = userUpdate.UserName;

                if (!string.IsNullOrEmpty(userUpdate.Email))
                    user.Email = userUpdate.Email;

                _unityOfWork.UserRepository.Update(user);
                _unityOfWork.Commit();

                var userDTO = _mapper.Map<UserUpdateDTO>(user);

                return Ok(userDTO);
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

                var removedUser = _unityOfWork.UserRepository.Delete(user);
                _unityOfWork.Commit();

                var removedUserDTO = _mapper.Map<UserDTO>(removedUser);

                return Ok(removedUserDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "Ocorreu um erro ao tentar tratar a sua solicitação..."});
            }
        }

    }
}
