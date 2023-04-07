using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.Models;

namespace TimeCapsuleBackend.Data.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User>GetByIdAsync(int UserId);
        Task InsertAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int UserId);
        Task SaveAsync();
    }
}
