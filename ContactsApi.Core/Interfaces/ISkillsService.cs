﻿using ContactsApi.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsApi.Core.Interfaces
{
    public interface ISkillsService
    {
        Task<SkillGetViewModel> GetAsync(int id);
        Task<IList<SkillGetViewModel>> GetAllAsync();
        Task<int> AddAsync(SkillSaveViewModel skillViewModel);
        Task UpdateAsync(int id, SkillSaveViewModel skillViewModel);
        Task DeleteAsync(int id);
    }
}
