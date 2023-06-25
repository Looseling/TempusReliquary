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
using Microsoft.AspNetCore.Authorization;
using TimeCapsuleBackend.Helper;

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
        [Authorize]
        public async Task<ActionResult<IEnumerable<string>>> GetContent(string timeCapsuleId)
        {
            //var tcContents = await _tcContentRepository.GetAllAsync();
            //var tcContentDTOs = _mapper.Map<IEnumerable<TCContentDTO>>(tcContents);

            //prepare needed variables
            string currentUser = HelperFunctions.GetCurrentUser(HttpContext).Username.ToString();
            var fileList = await _blobService.ListBlobsAsync(currentUser, timeCapsuleId);
            
            return Ok(fileList);
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
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post( IFormFile file, [FromForm] TCContentDTO tcContentDTO)
        {
            var tcContent = _mapper.Map<TimeCapsuleContent>(tcContentDTO);
            //Insert to db
            await _tcContentRepository.InsertAsync(tcContent);
            //prepare needed variables
            string timeCapsuleIdString = tcContent.TimeCapsuleId.ToString();
            string currentUser = HelperFunctions.GetCurrentUser(HttpContext).Username.ToString();

            //upload file to blob storage
            await _blobService.UploadContentBlobAsync(file, file.FileName,timeCapsuleIdString, currentUser);
            return Ok("success");
        }
    }
}
