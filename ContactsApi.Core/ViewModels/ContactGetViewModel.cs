using System.Collections.Generic;

namespace ContactsApi.Core.ViewModels
{
    public class ContactGetViewModel : ContactSaveViewModel
    {
        public int Id { get; set; }
        public string Fullname => $"{Firstname} {Lastname}";
        public IEnumerable<SkillGetViewModel> Skills { get; set; }
    }
}
