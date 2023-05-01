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
    public class TimeCapsuleContentRepository : ITimeCapsuleContentRepository
    {
        private readonly TImeCapsuleDBContext _dbContext;
        
        public TimeCapsuleContentRepository(TImeCapsuleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(int TimeCapsuleContentId)
        {
            var TimeCapsuleContent = await _dbContext.TimeCapsuleContents.FindAsync(TimeCapsuleContentId);
            if (TimeCapsuleContent != null)
            {
                _dbContext.TimeCapsuleContents.Remove(TimeCapsuleContent);
                await SaveAsync();
            }
        }

        public async Task<IEnumerable<TimeCapsuleContent>> GetAllAsync()
        {

            return await _dbContext.TimeCapsuleContents.ToListAsync();
        }

        public async Task<TimeCapsuleContent> GetByIdAsync(int TimeCapsuleContentId)
        {
            return await _dbContext.TimeCapsuleContents.FindAsync(TimeCapsuleContentId);
        }

        public async Task InsertAsync(TimeCapsuleContent TimeCapsuleContent)
        {
            //TimeCapsuleContent. = DateTime.Now;
            //TimeCapsuleContent.UpdatedAt = DateTime.Now;
            //_dbContext.TimeCapsuleContents.Add(TimeCapsuleContent);
            //await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TimeCapsuleContent TimeCapsuleContent)
        {
            //TimeCapsuleContent.UpdatedAt = DateTime.Now;
            //_dbContext.TimeCapsuleContents.Update(TimeCapsuleContent);
            //await SaveAsync();
        }

        Task<User> ITimeCapsuleContentRepository.GetByIdAsync(int UserId)
        {
            throw new NotImplementedException();
        }
    }
}
