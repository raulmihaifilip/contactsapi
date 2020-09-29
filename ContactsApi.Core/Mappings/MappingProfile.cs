using AutoMapper;
using ContactsApi.Core.Entities;
using ContactsApi.Core.ViewModels;

namespace ContactsApi.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactGetViewModel>();
        }
    }
}
