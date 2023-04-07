using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCapsuleBackend.Data.Models
{
    public partial class TimeCapsuleContent
    {
        public int Id { get; set; }
        public string Files { get; set; }
        public string Text { get; set; }
        public string ContentType { get; set; }
        public int? TimeCapsuleId { get; set; }

        public virtual TimeCapsule TimeCapsule { get; set; }
    }
}
