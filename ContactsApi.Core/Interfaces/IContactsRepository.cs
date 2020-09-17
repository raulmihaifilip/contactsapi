using ContactsApi.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsApi.Core.Interfaces
{
    public interface IContactsRepository : IRepository<Contact>
    {
        Task AddContactSkillsAsync(IEnumerable<ContactSkill> contactSkills);
        Task<Contact> GetWithSkillsAsync(int id);
    }
}
