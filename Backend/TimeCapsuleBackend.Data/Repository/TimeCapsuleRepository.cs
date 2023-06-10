using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
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
        private readonly IDistributedCache _cache;
        private readonly TImeCapsuleDBContext _dbContext;
        private readonly ICollaboratorRepository _collaboratorRepository;

        public TimeCapsuleRepository(TImeCapsuleDBContext dbContext, ICollaboratorRepository collaboratorRepository, IDistributedCache cache)
        {
            _dbContext = dbContext;
            _collaboratorRepository = collaboratorRepository;
            _cache = cache;
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

        public async Task<IEnumerable<TimeCapsule>> GetByUserId(int userId)
        {
            var collaborators = await _collaboratorRepository.GetByUserIdAsync(userId);
            if (collaborators.Any())
            {
                var collaboratorIds = collaborators.Select(c => c.TimeCapsuleId);
                var timeCapsules = await _dbContext.TimeCapsules.Where(tc => collaboratorIds.Contains(tc.Id)).ToListAsync();
                return timeCapsules;
            }
            return null;
        }

        public async Task<IEnumerable<TimeCapsule>> GetMostViewedAsync()
        {
            string cacheKey = "MostViewedTimeCapsules";
            string serializedData = await _cache.GetStringAsync(cacheKey);

            if (serializedData == null)
            {
                var data = await _dbContext.TimeCapsules.OrderBy(capsule => capsule.Veiws).Take(10).ToListAsync();
                serializedData = JsonConvert.SerializeObject(data);

                await _cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }

            var result = JsonConvert.DeserializeObject<List<TimeCapsule>>(serializedData);
            return result;
        }

        public async Task InsertAsync(TimeCapsule TimeCapsule, int userId)
        {
            TimeCapsule.CreatedAt = DateTime.Now;
            TimeCapsule.UpdatedAt = DateTime.Now;

            _dbContext.TimeCapsules.Add(TimeCapsule);
            await SaveAsync();

            Collaborator collaborator = new Collaborator()
            {
                TimeCapsuleId = TimeCapsule.Id, // Assign the generated Id
                UserId = userId,
            };
            await _collaboratorRepository.InsertAsync(collaborator);
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
