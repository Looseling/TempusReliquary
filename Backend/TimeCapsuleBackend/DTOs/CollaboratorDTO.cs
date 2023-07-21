using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCapsuleBackend.Data.DTOs
{
    public class CollaboratorDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TimeCapsuleId { get; set; }
    }
}
