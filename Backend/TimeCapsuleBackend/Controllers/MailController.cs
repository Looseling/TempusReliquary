using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using BussinessLogic.Services;
using BussinessLogic.Models;
using AutoMapper.Configuration.Conventions;
using TimeCapsuleBackend.Data.Repository.IRepository;
using TimeCapsuleBackend.Data.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeCapsuleBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly ITimeCapsuleEmailRepository _emailRepository;

        public MailController(ITimeCapsuleEmailRepository emailRepository, IMailService mailService)
        {
            _emailRepository = emailRepository;
            _mailService = mailService;
        }

        [HttpPost("Receivers")]
        public async Task<IActionResult> Post([FromBody] string[] emails, int timeCapsuleId)
        {
            foreach (var email in emails)
            {
                await _emailRepository.InsertAsync(email, timeCapsuleId);
            }
            return Ok();
        }

        [HttpGet("GetReceivers")]
        public async Task<IActionResult> Get(int timeCapsuleId)
        {
            var emails = await _emailRepository.GetEmailsByTimeCapsuleId(timeCapsuleId);

            return Ok(emails);
        }

        [HttpGet("SendEmail")]
        public IActionResult Get()
        {
            var receivers = new List<TimeCapsuleEmail1> { 
                new TimeCapsuleEmail1 { Email = "batyrkhan.akzholov@gmail.com"},
                new TimeCapsuleEmail1 { Email = "batyrkhan.akzholov@edu.uni.lodz.pl"}

            }; 
            var isSent = _mailService.SendMail(receivers);
            return Ok(isSent);
        }
    }
}
