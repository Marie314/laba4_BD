﻿using HairdressingSalon.DAL.Interfaces;
using HairdressingSalon.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairdressingSalon.DAL.Database.Repositories
{
    public class ServicesRepository : BaseRepository<Service>, IServicesRepository
    {
        private readonly IMemoryCache _memoryCache;

        public ServicesRepository(ApplicationDbContext dbContext, IMemoryCache memoryCache)
            : base(dbContext)
        {
            _memoryCache = memoryCache;
        }

        public async Task Create(Service model) =>
            await CreateEntity(model);

        public async Task Create(IEnumerable<Service> models) =>
            await CreateEntities(models);

        public async Task Delete(Service model) =>
            await DeleteEntity(model);

        public async Task<IEnumerable<Service>> GetAll(bool trackChanges) =>
            await GetAllEntities(trackChanges).ToListAsync();

        public async Task<Service> GetById(int id, bool trackChanges) =>
            await GetByCondition(x => x.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task Update(Service model) =>
            await UpdateEntity(model);

        public async Task<IEnumerable<Service>> Get(int rowsCount, string cacheKey)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Service> models))
            {
                models = await dbContext.Services.Take(rowsCount).Include(s => s.Order).ThenInclude(o => o.Client).Include(s => s.ServiceKind).ToListAsync();
                if (models != null)
                {
                    _memoryCache.Set(cacheKey, models,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(CachingTime)));
                }
            }
            return models;
        }
    }
}
