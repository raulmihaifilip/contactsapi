using ContactsApi.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsApi.Core.Interfaces
{
    public interface ISkillsRepository : IRepository<Skill>
    {
        Task<Skill> GetWithLevelAsync(int id);
        Task<IList<Skill>> GetAllWithLevelAsync();
        Task<Skill> GetWithContactsAsync(int id);
    }
}
