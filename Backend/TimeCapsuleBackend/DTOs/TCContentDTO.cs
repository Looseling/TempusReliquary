using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.Models;

namespace TimeCapsuleBackend.Data.DTOs
{
    public class TCContentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? TimeCapsuleId { get; set; }
    }
}
