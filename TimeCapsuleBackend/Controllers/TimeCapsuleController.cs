using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.DTOs;
using TimeCapsuleBackend.Data.Models;
using TimeCapsuleBackend.Data.Repository.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeCapsuleBackend.Controllers
{
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
        public async Task<ActionResult<IEnumerable<TimeCapsuleDTO>>> GetTimeCapsules()
        {
            var TimeCapsules = await _TimeCapsuleRepository.GetAllAsync();

            var TimeCapsulesDTO = _mapper.Map<IEnumerable<TimeCapsuleDTO>>(TimeCapsules);

            return Ok(TimeCapsulesDTO);
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
        public async Task<IActionResult> Post(/*[FromBody]*/ TimeCapsuleDTO timeCapsuleDTO)
        {

          var timeCapsule = _mapper.Map<TimeCapsule>(timeCapsuleDTO);
          timeCapsule.User = await _userRepository.GetByIdAsync(timeCapsule.UserId);

            await _TimeCapsuleRepository.InsertAsync(timeCapsule);
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
    }
}
