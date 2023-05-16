using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using TimeCapsuleBackend.Data.DTOs;
using TimeCapsuleBackend.Data.Models;
using TimeCapsuleBackend.Data.Repository;
using TimeCapsuleBackend.Data.Repository.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeCapsuleBackend.Controllers
{
    [Route("api/collabortators")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IMapper _mapper;

        public CollaboratorController(ICollaboratorRepository collaboratorRepository, IMapper mapper)
        {
            _collaboratorRepository = collaboratorRepository;
            _mapper = mapper;
        }

        // GET: api/collabortators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollaboratorDTO>>> Get()
        {
            var collaborators = await _collaboratorRepository.GetAllAsync();
            var collaboratorsDTO = _mapper.Map<IEnumerable<CollaboratorDTO>>(collaborators);
            return Ok(collaboratorsDTO);
        }

        // GET api/collabortators/5
        [HttpGet("{collaboratorid}")]
        public async Task<ActionResult<CollaboratorDTO>> GetCollaboratorById(int collaboratorid)
        {
            var collaborator = await _collaboratorRepository.GetByIdAsync(collaboratorid);
            if (collaborator  == null)
            {
                return NotFound();
            }
            var collaboratorDTO = _mapper.Map<CollaboratorDTO>(collaborator);

            return Ok(collaboratorDTO);
        }

        // POST api/collaborators
        [HttpPost]
        public async Task<IActionResult> Post(CollaboratorDTO collaboratorDTO)
        {

            var collaborator = _mapper.Map<Collaborator>(collaboratorDTO);

            await _collaboratorRepository.InsertAsync(collaborator);
            return CreatedAtAction(nameof(GetCollaboratorById), new { collaboratorId = collaborator.Id }, collaborator);
        }

        // PUT api/collaborators/5
        //[HttpPut("{collaboratorId}")]
        //public async Task<IActionResult> Update(int collaboratorId, CollaboratorDTO collaboratorDTO)
        //{
        //    var collaboratordb = await _collaboratorRepository.GetByIdAsync(collaboratorId);
        //    if (collaboratordb == null)
        //    {
        //        return NotFound();
        //    }
        //    var collaborator = _mapper.Map<CollaboratorDTO, Collaborator>(collaboratorDTO, collaboratordb);
        //    collaborator.Id = collaboratorId;
        //    await _collaboratorRepository.UpdateAsync(collaborator);
        //    return NoContent();
        //}
        // create method PUT for my controller



        // DELETE api/collaborators/5
        [HttpDelete("{collaboratorId}")]
        public async Task<IActionResult> Delete(int collaboratorId)
        {
            await _collaboratorRepository.DeleteAsync(collaboratorId);
            return NoContent();
        }
    }
}
