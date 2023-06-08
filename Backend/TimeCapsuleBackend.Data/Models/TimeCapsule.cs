using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCapsuleBackend.Data.Models
{
    public partial class TimeCapsule
    {
        public TimeCapsule()
        {
            Collaborators = new HashSet<Collaborator>();
            TimeCapsuleContents = new HashSet<TimeCapsuleContent>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsUploaded { get; set; }
        public int? Veiws { get; set; }

        public virtual ICollection<Collaborator> Collaborators { get; set; }
        public virtual ICollection<TimeCapsuleContent> TimeCapsuleContents { get; set; }
    }
}
