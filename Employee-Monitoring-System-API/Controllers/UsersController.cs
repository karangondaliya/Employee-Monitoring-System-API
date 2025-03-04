using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using BCrypt.Net;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public UsersController(IUserRepository userRepository, IMapper mapper, JwtService jwtService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO loginDto)
        {
            var user = _userRepository.FindByEmail(loginDto.email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.password, user.Password))
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user.Email, user.Role);

            return Ok(new { Token = token });
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var users = _userRepository.GetAllUsers();
            var UserDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(UserDTOs);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(Policy = "TeamLeadPolicy")]
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
        [Authorize(Policy = "AdminPolicy")]
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
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<UserDTO> PostUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var addedUser = _userRepository.Add(user);

            var createdUserDTO = _mapper.Map<UserDTO>(addedUser);
            return CreatedAtAction(nameof(GetUser), new { id = createdUserDTO.Id }, createdUserDTO);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
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