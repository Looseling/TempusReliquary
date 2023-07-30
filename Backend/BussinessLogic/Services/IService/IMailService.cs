using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.Models;

namespace BussinessLogic.Services
{
    public interface IMailService
    {
        bool SendMail(List<TimeCapsuleEmail1> EmailReceivers);
    }
}
