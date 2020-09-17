using ContactsApi.Core.Entities;
using ContactsApi.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsApi.Persistence.Repositories
{
    public class ContactsRepository : Repository<Contact>, IContactsRepository
    {
        public ContactsRepository(ContactsApiDbContext dbContext) : base(dbContext) { }

        public async Task AddContactSkillsAsync(IEnumerable<ContactSkill> contactSkills)
        {
            var contactIds = contactSkills.Select(cs => cs.ContactId).ToArray();
            var existingContactSkills = DbContext.ContactSkills.Where(cs => contactIds.Contains(cs.ContactId)).ToList();
            DbContext.ContactSkills.RemoveRange(existingContactSkills);

            var newContactSkills = contactSkills.Where(cs => cs.SkillId != 0).ToList();
            await DbContext.ContactSkills.AddRangeAsync(newContactSkills);

            await DbContext.SaveChangesAsync();
        }

        public async Task<Contact> GetWithSkillsAsync(int id)
        {
            return await DbContext.Contacts
                        .Include(c => c.ContactSkills)
                        .ThenInclude(c => c.Skill)
                        .ThenInclude(c => c.Level)
                        .SingleOrDefaultAsync(c => c.Id == id);

        }
    }
}
