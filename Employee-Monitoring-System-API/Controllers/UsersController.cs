﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest();
            }

            var user = _mapper.Map<User>(userDTO);
            var updatedUser = _userRepository.Update(user);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<UserDTO> PostUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
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