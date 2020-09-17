using System.Collections.Generic;

namespace ContactsApi.Core.ViewModels
{
    public class SkillsToContactViewModel
    {
        public int ContactId { get; set; }
        public IEnumerable<int> SkillIds { get; set; }
    }
}
