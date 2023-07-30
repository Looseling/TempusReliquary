using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCapsuleBackend.Data.Models
{
    public partial class TimeCapsuleEmail1
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int TimeCapsuleId { get; set; }

        public virtual TimeCapsule TimeCapsule { get; set; }
    }
}
