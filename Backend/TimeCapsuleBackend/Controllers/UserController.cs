using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.DTOs;
using TimeCapsuleBackend.Data.Models;
using TimeCapsuleBackend.Data.Repository.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeCapsuleBackend.Controllers
{
    [EnableCors]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            
            var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(users);
            
            return Ok(usersDTO);
        }

        // GET api/users/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDTO>>GetUserById(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var userDTO = _mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post(/*[FromBody]*/ UserDTO userDTO)
        {

            var user = _mapper.Map<User>(userDTO);

            await _userRepository.InsertAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, user);
        }

        // PUT api/users/5
        [HttpPut("{userId}")]
        public async Task<IActionResult>  Update(int userId, UserDTO userDTO)
        {
            var userdb = await _userRepository.GetByIdAsync(userId);
            if (userdb == null)
            {
                return NotFound();
            }
            var user = _mapper.Map<UserDTO, User>(userDTO, userdb);
            user.Id = userId;
            await _userRepository.UpdateAsync(user);
            return NoContent();
        }

        // DELETE api/users/5
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            await _userRepository.DeleteAsync(userId);
            return NoContent();
        }
    }
}
