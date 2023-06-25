using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.Models;
using TimeCapsuleBackend.Data.Repository.IRepository;

namespace TimeCapsuleBackend.Data.Repository
{
    public class CollaboratorRepository : ICollaboratorRepository
    {

        private readonly TImeCapsuleDBContext _dbContext;

        public CollaboratorRepository(TImeCapsuleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(int CollaboratorId)
        {
            var collaborator = await _dbContext.Collaborators.FindAsync(CollaboratorId);
            if (collaborator != null)
            {
                _dbContext.Collaborators.Remove(collaborator);
            }
        }

        public async Task<IEnumerable<Collaborator>> GetAllAsync()
        {
            return await _dbContext.Collaborators.ToListAsync();
        }

        public async Task<Collaborator> GetByIdAsync(int UserId)
        {
            return await _dbContext.Collaborators.FindAsync(UserId);

        }

        public async Task<IEnumerable<Collaborator>> GetByUserIdAsync(int UserId)
        {
            var collaborators = await _dbContext.Collaborators.Where(c => c.UserId == UserId).ToListAsync();
            return collaborators;
        }

        public async Task InsertAsync(Collaborator collaborator)
        {
            _dbContext.Collaborators.Add(collaborator);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Collaborator collaborator)
        {
            _dbContext.Collaborators.Update(collaborator);
            await SaveAsync();
        }

      
    }
}
