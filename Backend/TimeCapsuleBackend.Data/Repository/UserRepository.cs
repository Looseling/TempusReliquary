using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.Models;
using TimeCapsuleBackend.Data.Repository.IRepository;

namespace TimeCapsuleBackend.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TImeCapsuleDBContext _dbContext;
        
        public UserRepository(TImeCapsuleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(int UserId)
        {
            var user = await _dbContext.Users.FindAsync(UserId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await SaveAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {

            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int UserId)
        {
            return await _dbContext.Users.FindAsync(UserId);
        }

        public async Task InsertAsync(User user)
        {
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            _dbContext.Users.Add(user);
            await SaveAsync();

        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            user.UpdatedAt = DateTime.Now;
            _dbContext.Users.Update(user);
            await SaveAsync();
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> RandomUser()
        {
            var users = await _dbContext.Users.ToListAsync();
            Random random = new Random();
            int index = random.Next(users.Count);
            return users[index];
        }
    }
}
