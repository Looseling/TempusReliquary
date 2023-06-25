using AutoMapper;
using BussinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.DTOs;
using TimeCapsuleBackend.Data.Models;
using TimeCapsuleBackend.Data.Repository.IRepository;
using TimeCapsuleBackend.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeCapsuleBackend.Controllers
{
    [EnableCors]
    [Route("api/timecapsules")]
    [ApiController]
    public class TimeCapsuleController : ControllerBase
    {
        public readonly ITimeCapsuleRepository _TimeCapsuleRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public TimeCapsuleController(ITimeCapsuleRepository TimeCapsuleRepository, IMapper mapper, IUserRepository userRepository)
        {
            _TimeCapsuleRepository = TimeCapsuleRepository;
            _mapper = mapper;
            _userRepository = userRepository;   
        }


        // GET: api/timecapsules
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<TimeCapsuleDTO>>> GetTimeCapsules()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user ID from token

            var TimeCapsules = await _TimeCapsuleRepository.GetByUserId(int.Parse(userId)); // Get time capsules for this user
            if (TimeCapsules != null)
            {
                var TimeCapsulesDTO = _mapper.Map<List<TimeCapsuleDTO>>(TimeCapsules);
                return Ok(TimeCapsulesDTO);
            }
            return NotFound();

        }

        // GET api/timecapsules/5
        [HttpGet("{timecapsuleId}")]
        public async Task<ActionResult<TimeCapsuleDTO>> GetTimeCapsuleById(int TimeCapsuleId)
        {
            var TimeCapsule = await _TimeCapsuleRepository.GetByIdAsync(TimeCapsuleId);
            if (TimeCapsule == null)
            {
                return NotFound();
            }
            var TimeCapsuleDTO = _mapper.Map<TimeCapsuleDTO>(TimeCapsule);

            return Ok(TimeCapsuleDTO);
        }

        // POST api/timecapsules
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post( TimeCapsuleDTO timeCapsuleDTO)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user ID from token
            var timeCapsule = _mapper.Map<TimeCapsule>(timeCapsuleDTO);



            await _TimeCapsuleRepository.InsertAsync(timeCapsule, int.Parse(userId));
            var currentUser = HelperFunctions.GetCurrentUser(HttpContext);



            return Ok();
        }

        // PUT api/timecapsules/5
        [HttpPut("{TimeCapsuleId}")]
        public async Task<IActionResult> Update(int TimeCapsuleId, TimeCapsuleDTO TimeCapsuleDTO)
        {
            var TimeCapsuledb = await _TimeCapsuleRepository.GetByIdAsync(TimeCapsuleId);
            if (TimeCapsuledb == null)
            {
                return NotFound();
            }
            var TimeCapsule = _mapper.Map<TimeCapsuleDTO, TimeCapsule>(TimeCapsuleDTO, TimeCapsuledb);
            TimeCapsule.Id = TimeCapsuleId;
            await _TimeCapsuleRepository.UpdateAsync(TimeCapsule);
            return NoContent();
        }

        // DELETE api/timecapsules/5
        [HttpDelete("{TimeCapsuleId}")]
        public async Task<IActionResult> Delete(int TimeCapsuleId)
        {
            await _TimeCapsuleRepository.DeleteAsync(TimeCapsuleId);
            return NoContent();
        }

        [HttpGet("most-viewed")]
        [Authorize]
        public async Task<ActionResult<List<TimeCapsuleDTO>>> GetMostViewed()
        {
            var TimeCapsules = await _TimeCapsuleRepository.GetMostViewedAsync();
            if (TimeCapsules != null)
            {
                var TimeCapsulesDTO = _mapper.Map<List<TimeCapsuleDTO>>(TimeCapsules);
                return Ok(TimeCapsulesDTO);
            }
            return NotFound();
        }
    }
}
