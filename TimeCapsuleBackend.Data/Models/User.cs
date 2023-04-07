using System;
using System.Collections.Generic;

#nullable disable

namespace TimeCapsuleBackend.Data.Models
{
    public partial class User
    {
        public User()
        {
            Collaborators = new HashSet<Collaborator>();
            TimeCapsules = new HashSet<TimeCapsule>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Collaborator> Collaborators { get; set; }
        public virtual ICollection<TimeCapsule> TimeCapsules { get; set; }
    }
}
