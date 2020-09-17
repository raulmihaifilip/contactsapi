using System.Collections.Generic;

namespace ContactsApi.Core.Entities
{
    public class SkillLevel
    {        
        public Enums.SkillLevel Id { get; set; }
        public string Name { get; set; }
        public List<Skill> Skills { get; set; }
    }
}