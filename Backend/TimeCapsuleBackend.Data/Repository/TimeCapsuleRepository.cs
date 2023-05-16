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
    public class TimeCapsuleRepository : ITimeCapsuleRepository
    {
        private readonly TImeCapsuleDBContext _dbContext;
        
        public TimeCapsuleRepository(TImeCapsuleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(int TimeCapsuleId)
        {
            var TimeCapsule = await _dbContext.TimeCapsules.FindAsync(TimeCapsuleId);
            if (TimeCapsule != null)
            {
                _dbContext.TimeCapsules.Remove(TimeCapsule);
                await SaveAsync();
            }
        }

        public async Task<IEnumerable<TimeCapsule>> GetAllAsync()
        {

            return await _dbContext.TimeCapsules.ToListAsync();
        }

        public async Task<TimeCapsule> GetByIdAsync(int TimeCapsuleId)
        {
            return await _dbContext.TimeCapsules.FindAsync(TimeCapsuleId);
        }

        public async Task InsertAsync(TimeCapsule TimeCapsule)
        {
            TimeCapsule.CreatedAt = DateTime.Now;
            TimeCapsule.UpdatedAt = DateTime.Now;
            _dbContext.TimeCapsules.Add(TimeCapsule);
            await SaveAsync();

        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TimeCapsule TimeCapsule)
        {
            TimeCapsule.UpdatedAt = DateTime.Now;
            _dbContext.TimeCapsules.Update(TimeCapsule);
            await SaveAsync();
        }
    }
}
