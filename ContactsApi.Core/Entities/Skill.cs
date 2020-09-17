using System.Collections.Generic;

namespace ContactsApi.Core.Entities
{
    public class Skill : BaseEntity
    {
        public string Name { get; set; }
        public Enums.SkillLevel LevelId { get; set; }
        public SkillLevel Level { get; set; }
        public List<ContactSkill> ContactSkills { get; set; }
    }
}
