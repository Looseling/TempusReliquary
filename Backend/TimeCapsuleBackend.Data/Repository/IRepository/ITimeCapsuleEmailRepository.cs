using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.Models;

namespace TimeCapsuleBackend.Data.Repository.IRepository
{
    public interface ITimeCapsuleEmailRepository
    {
        Task<IEnumerable<TimeCapsuleEmail>> GetEmailsByTimeCapsuleId(int timeCapsuleId);
        Task InsertAsync(string email, int timeCapsuleId);
        Task UpdateAsync(string email, int Id);
        Task DeleteAsync(int Id);
        Task SaveAsync();
    }
}
