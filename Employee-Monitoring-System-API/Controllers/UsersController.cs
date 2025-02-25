using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var users = _userRepository.GetAllUsers();
            var UserDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(UserDTOs);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetUser(int id)
        {
            var user = _userRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }

        // PUT: api/Users/5
        [HttpPatch("{id}")]
        public IActionResult PatchUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var updatedUser = _userRepository.Update(user);

            if (updatedUser == null)
            {
                return NotFound();
            }
            var updatedUserDTO = _mapper.Map<UserDTO>(updatedUser);
            return Ok(updatedUserDTO);
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<UserDTO> PostUser(User user)
        {
            var addedUser = _userRepository.Add(user);

            var createdUserDTO = _mapper.Map<UserDTO>(addedUser);
            return CreatedAtAction(nameof(GetUser), new { id = createdUserDTO.Id }, createdUserDTO);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepository.Delete(id);

            if (user == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}