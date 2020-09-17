using System.Collections.Generic;

namespace ContactsApi.Core.Entities
{
    public class Contact : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobilePhoneNumber { get; set; }
        public int UserId { get; set; }
        public List<ContactSkill> ContactSkills { get; set; }
    }
}
