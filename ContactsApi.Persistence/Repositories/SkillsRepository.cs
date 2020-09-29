using ContactsApi.Core.Entities;
using ContactsApi.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsApi.Persistence.Repositories
{
    public class SkillsRepository : Repository<Skill>, ISkillsRepository
    {
        public SkillsRepository(ContactsApiDbContext dbContext) : base(dbContext) { }

        public async Task<Skill> GetWithLevelAsync(int id)
        {
            return await DbContext.Skills
                        .Include(s => s.Level)
                        .SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IList<Skill>> GetAllWithLevelAsync()
        {
            return await DbContext.Skills
                         .Include(s => s.Level)
                         .ToListAsync();
        }

        public async Task<Skill> GetWithContactsAsync(int id)
        {
            return await DbContext.Skills
                         .Include(s => s.ContactSkills)
                         .ThenInclude(s => s.Contact)
                         .SingleOrDefaultAsync(s => s.Id == id);
        }
    }
}
