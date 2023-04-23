using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.Models;

namespace TimeCapsuleBackend.Data.Repository.IRepository
{
    public interface ICollaboratorRepository
    {
        Task<IEnumerable<Collaborator>> GetAllAsync();
        Task<Collaborator> GetByIdAsync(int UserId);
        Task InsertAsync(Collaborator collaborator);
        Task UpdateAsync(Collaborator collaborator);
        Task DeleteAsync(int CollaboratorId);
        Task SaveAsync();

    }
}
