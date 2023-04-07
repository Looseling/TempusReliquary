using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCapsuleBackend.Data.Models
{
    public partial class Collaborator
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TimeCapsuleId { get; set; }

        public virtual TimeCapsule TimeCapsule { get; set; }
        public virtual User User { get; set; }
    }
}
