using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

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

            // Update LastLogin field
            user.LastLogin = DateTime.UtcNow;
            _userRepository.Update(user); // Save changes to DB

            var token = _jwtService.GenerateToken(user.Email, user.Role);

            return Ok(new { Token = token, Role = user.Role, Id = user.Id });
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var users = _userRepository.GetAllUsers();
            if(users == null || !users.Any())
            {
                return NotFound("Users Not Found.");
            }
            var UserDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(UserDTOs);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<UserDTO> GetUser(int id)
        {
            var user = _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        // PUT: api/Users/5
        [HttpPatch("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
        public IActionResult PatchUser(int id, [FromBody] JsonPatchDocument<User> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var user = _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Apply patch to the user entity
            patchDoc.ApplyTo(user, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUser = _userRepository.Update(user);

            if (updatedUser == null)
            {
                return StatusCode(500, "Error updating user.");
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
            var user = _userRepository.GetUser(id);

            if (user == null)
            {
                return NotFound("User Not Found.");
            }
            _userRepository.Delete(id);
            return Ok("User Deleted " + id);
        }

        [HttpPost("update-password")]
        [AllowAnonymous]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordDTO updatePasswordDto)
        {
            var user = _userRepository.FindByEmail(updatePasswordDto.Email);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Verify the old password
            if (!BCrypt.Net.BCrypt.Verify(updatePasswordDto.OldPassword, user.Password))
            {
                return Unauthorized(new { message = "Old password is incorrect" });
            }

            // Hash the new password
            user.Password = BCrypt.Net.BCrypt.HashPassword(updatePasswordDto.NewPassword);

            _userRepository.Update(user);

            return Ok(new { message = "Password updated successfully" });
        }

        // POST: api/Users/5/upload-image
        [HttpPost("{id}/upload-image")]
        [Authorize(Policy = "EmployeePolicy")]
        public IActionResult UploadProfileImage(int id, IFormFile file)
        {
            var user = _userRepository.GetUser(id);
            if (user == null) return NotFound("User not found.");

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using var ms = new MemoryStream();
            file.CopyTo(ms);
            user.ProfileImage = ms.ToArray();
            _userRepository.Update(user);

            return Ok("Profile image uploaded.");
        }

        // GET: api/Users/5/profile-image
        [HttpGet("{id}/profile-image")]
        [Authorize(Policy = "EmployeePolicy")]
        public IActionResult GetProfileImage(int id)
        {
            var user = _userRepository.GetUser(id);
            if (user == null) return NotFound("User not found.");
            if (user.ProfileImage == null) return NotFound("No profile image.");

            return File(user.ProfileImage, "image/png");
        }

    }
}