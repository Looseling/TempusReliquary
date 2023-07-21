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

namespace TimeCapsuleBackend.Controllers
{
    [EnableCors] // Specify the CORS policy name or remove the attribute if not required
    [Route("api/timecapsules")]
    [ApiController]
    public class TimeCapsuleController : ControllerBase
    {
        private readonly ITimeCapsuleRepository _timeCapsuleRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public TimeCapsuleController(ITimeCapsuleRepository timeCapsuleRepository, IMapper mapper, IUserRepository userRepository)
        {
            _timeCapsuleRepository = timeCapsuleRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet("uploaded")]
        public async Task<ActionResult<List<TimeCapsuleDTO>>> GetUploadedTimeCapsules()
        {
            var timeCapsules = await _timeCapsuleRepository.GetAllUploaded();
            if (timeCapsules != null)
            {
                var timeCapsulesDTO = _mapper.Map<List<TimeCapsuleDTO>>(timeCapsules);
                return Ok(timeCapsulesDTO);
            }
            return Ok(new List<TimeCapsuleDTO>()); // Return an empty list instead of NotFound
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<TimeCapsuleDTO>>> GetUserTimeCapsules()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var timeCapsules = await _timeCapsuleRepository.GetByUserId(int.Parse(userId));
            if (timeCapsules != null)
            {
                var timeCapsulesDTO = _mapper.Map<List<TimeCapsuleDTO>>(timeCapsules);
                return Ok(timeCapsulesDTO);
            }
            return Ok(new List<TimeCapsuleDTO>());
        }

        [HttpGet("{timeCapsuleId}")]
        public async Task<ActionResult<TimeCapsuleDTO>> GetTimeCapsuleById(int timeCapsuleId)
        {
            var timeCapsule = await _timeCapsuleRepository.GetByIdAsync(timeCapsuleId);
            if (timeCapsule == null)
            {
                return NotFound();
            }
            var timeCapsuleDTO = _mapper.Map<TimeCapsuleDTO>(timeCapsule);

            return Ok(timeCapsuleDTO);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(TimeCapsuleDTO timeCapsuleDTO)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var timeCapsule = _mapper.Map<TimeCapsule>(timeCapsuleDTO);

            await _timeCapsuleRepository.InsertAsync(timeCapsule, int.Parse(userId));

            return Ok(timeCapsuleDTO); // Return the created TimeCapsuleDTO or appropriate response
        }

        [HttpPut("{timeCapsuleId}")]
        public async Task<IActionResult> Update(int timeCapsuleId, TimeCapsuleDTO timeCapsuleDTO)
        {
            var timeCapsuleDb = await _timeCapsuleRepository.GetByIdAsync(timeCapsuleId);
            if (timeCapsuleDb == null)
            {
                return NotFound();
            }
            else if (timeCapsuleDb.IsUploaded == true)
            {
                return BadRequest("Can't update uploaded TimeCapsules :/");
            }
            var timeCapsule = _mapper.Map(timeCapsuleDTO, timeCapsuleDb);
            timeCapsule.Id = timeCapsuleId;

            await _timeCapsuleRepository.UpdateAsync(timeCapsule);

            return NoContent();
        }

        [HttpDelete("{timeCapsuleId}")]
        public async Task<IActionResult> Delete(int timeCapsuleId)
        {
            await _timeCapsuleRepository.DeleteAsync(timeCapsuleId);
            return NoContent();
        }

        [HttpGet("mostviewed")]
        [Authorize]
        public async Task<ActionResult<List<TimeCapsuleDTO>>> GetMostViewed()
        {
            var timeCapsules = await _timeCapsuleRepository.GetMostViewedAsync();
            if (timeCapsules != null)
            {
                var timeCapsulesDTO = _mapper.Map<List<TimeCapsuleDTO>>(timeCapsules);
                return Ok(timeCapsulesDTO);
            }
            return Ok(new List<TimeCapsuleDTO>());
        }
    }
}
