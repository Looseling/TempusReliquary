using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.Models;

namespace TimeCapsuleBackend.Data.Repository.IRepository
{
    public interface ITimeCapsuleRepository
    {
        Task<IEnumerable<TimeCapsule>> GetAllAsync();
        Task<TimeCapsule> GetByIdAsync(int TimeCapsuleId);
        Task InsertAsync(TimeCapsule TimeCapsule);
        Task UpdateAsync(TimeCapsule TimeCapsule);
        Task DeleteAsync(int TimeCapsuleId);
        Task SaveAsync();
    }
}
