using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLogic.Services;
using TimeCapsuleBackend.Data.DTOs;
using TimeCapsuleBackend.Data.Models;
using TimeCapsuleBackend.Data.Repository;
using TimeCapsuleBackend.Data.Repository.IRepository;

namespace TimeCapsuleBackend.Controllers
{
    [Route("api/timecapsule/content")]
    [ApiController]
    public class TCContentController : ControllerBase
    {
        private readonly IBlobService _blobService; 
        private readonly ITCContentRepository _tcContentRepository;
        private readonly IMapper _mapper;
        

        public TCContentController(ITCContentRepository tcContentRepository, IMapper mapper, IBlobService blobService)
        {
            _tcContentRepository = tcContentRepository;
            _mapper = mapper;
            _blobService = blobService;
        }

        // GET: api/content
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TCContentDTO>>> GetContent()
        {
            var tcContents = await _tcContentRepository.GetAllAsync();
            var tcContentDTOs = _mapper.Map<IEnumerable<TCContentDTO>>(tcContents);
            return Ok(tcContentDTOs);
        }

        //GET: api/content/5
        [HttpGet("{tcContentId}")]
        public async Task<ActionResult<TCContentDTO>> GetContentById(int tcContentId)
        {
            var tcContent = await _tcContentRepository.GetByIdAsync(tcContentId);
            if (tcContent == null)
            {
                return NotFound();
            }
            var tcContentDTO = _mapper.Map<TCContentDTO>(tcContent);
            return Ok(tcContentDTO);
        }

        // POST api/content
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post( IFormFile file, [FromForm] TCContentDTO tcContentDTO)
        {
            var tcContent = _mapper.Map<TimeCapsuleContent>(tcContentDTO);
            //Insert to db
            await _tcContentRepository.InsertAsync(tcContent);
            //upload file to blob storage
            await _blobService.UploadContentBlobAsync(file, file.FileName,"testUser");
            return Ok("success");
        }
    }
}
