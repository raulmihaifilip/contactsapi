﻿using ContactsApi.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsApi.Core.Interfaces
{
    public interface IContactsService
    {
        Task<IList<ContactGetViewModel>> GetAllAsync();
        Task<ContactGetViewModel> GetAsync(int id);
        Task<int> AddAsync(ContactSaveViewModel contactViewModel);
        Task UpdateAsync(int id, ContactSaveViewModel contactViewModel);
        Task DeleteAsync(int id);
        Task AssignSkills(IEnumerable<SkillsToContactViewModel> skillsToContacts);
    }
}
