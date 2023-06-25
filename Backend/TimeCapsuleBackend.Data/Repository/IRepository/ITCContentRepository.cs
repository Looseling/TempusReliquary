using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.Models;

namespace TimeCapsuleBackend.Data.Repository.IRepository
{
    public interface ITCContentRepository
    {
        Task<IEnumerable<TimeCapsuleContent>> GetAllAsync();
        Task<User>GetByIdAsync(int UserId);
        Task InsertAsync(TimeCapsuleContent user);
        Task UpdateAsync(TimeCapsuleContent user);
        Task DeleteAsync(int UserId);
        Task SaveAsync();
    }
}

