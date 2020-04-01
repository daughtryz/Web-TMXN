﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMXN.Data.Common.InputModels.Enums;
using TMXN.Data.Common.Repositories;
using TMXN.Data.Models;
using TMXN.Services.Mapping;

namespace TMXN.Services.Data
{
    public class AwardsService : IAwardsService
    {
        private readonly IDeletableEntityRepository<Award> awardsRepository;

        public AwardsService(IDeletableEntityRepository<Award> awardsRepository)
        {
            this.awardsRepository = awardsRepository;
        }
        public async Task CreateAsync(string name, PlacingType placingType)
        {
            var award = new Award
            {
                Name = name,
                PlacingType = placingType,
            };

            await this.awardsRepository.AddAsync(award);
            await this.awardsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TViewModel>> GetAll<TViewModel>()
        {
            return await this.awardsRepository.All().To<TViewModel>().ToListAsync();
        }
    }
}