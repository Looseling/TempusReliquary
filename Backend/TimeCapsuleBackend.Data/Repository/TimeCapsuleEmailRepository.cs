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
    public class TimeCapsuleEmailRepository : ITimeCapsuleEmailRepository
    {
        private readonly TImeCapsuleDBContext _dbContext;

        public TimeCapsuleEmailRepository(TImeCapsuleDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task DeleteAsync(int Id)
        {
            var emailModel = await _dbContext.TimeCapsuleEmails.FindAsync(Id);
            _dbContext.TimeCapsuleEmails.Remove(emailModel);
                await SaveAsync();
        }

        public async Task<IEnumerable<TimeCapsuleEmail>> GetEmailsByTimeCapsuleId(int timeCapsuleId)
        {
            return await _dbContext.TimeCapsuleEmails.Where(x => x.TimeCapsuleId == timeCapsuleId).ToListAsync();
        }

        public async Task InsertAsync(string email, int timeCapsuleId)
        {

            var timeCapsule = await _dbContext.TimeCapsules.FindAsync(timeCapsuleId);
            var emailModel = new TimeCapsuleEmail
            {
                Email = email,
                TimeCapsule = timeCapsule,
            };

            _dbContext.TimeCapsuleEmails.Add(emailModel);
            await SaveAsync();
        }

        
        public async Task UpdateAsync(string email,int Id)
        {
            var emailModel = await _dbContext.TimeCapsuleEmails.FindAsync(Id);
            emailModel.Email = email;

            _dbContext.TimeCapsuleEmails.Update(emailModel);
            await SaveAsync();
        }
        
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
