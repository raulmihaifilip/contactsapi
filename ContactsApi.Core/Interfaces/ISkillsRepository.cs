using ContactsApi.Core.Entities;
using System.Threading.Tasks;

namespace ContactsApi.Core.Interfaces
{
    public interface ISkillsRepository : IRepository<Skill>
    {
        Task<Skill> GetWithLevelAsync(int id);
        Task<Skill> GetWithContactsAsync(int id);
    }
}
