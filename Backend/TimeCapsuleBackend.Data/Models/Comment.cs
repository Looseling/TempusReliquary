using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCapsuleBackend.Data.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string Comment1 { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
