using AutoMapper;
using ContactsApi.Core.Entities;
using ContactsApi.Core.Exceptions;
using ContactsApi.Core.Interfaces;
using ContactsApi.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsApi.Core.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IAuthClaimsService _authClaimsService;
        private readonly IMapper _mapper;

        public ContactsService(IAuthClaimsService authClaimsService, IContactsRepository contactsRepository, IMapper mapper)
        {
            _authClaimsService = authClaimsService;
            _contactsRepository = contactsRepository;
            _mapper = mapper;
        }

        public async Task<ContactGetViewModel> GetAsync(int id)
        {
            var contactEntity = await ValidateOperationAsync(id, false);

            var contactViewModel = _mapper.Map<ContactGetViewModel>(contactEntity);
            contactViewModel.Skills = contactEntity.ContactSkills.Select(cs => new SkillGetViewModel()
            {
                Id = cs.Skill.Id,
                Name = cs.Skill.Name,
                LevelId = cs.Skill.LevelId,
                LevelName = cs.Skill.Level.Name
            });

            return contactViewModel;
        }

        public async Task<IList<ContactGetViewModel>> GetAllAsync()
        {
            var contactEntities = await _contactsRepository.GetAllWithSkillsAsync();

            var contactViewModels = contactEntities.Select(contactEntity => new ContactGetViewModel()
            {
                Id = contactEntity.Id,
                Firstname = contactEntity.Firstname,
                Lastname = contactEntity.Lastname,
                Address = contactEntity.Address,
                Email = contactEntity.Email,
                MobilePhoneNumber = contactEntity.MobilePhoneNumber,
                Skills = contactEntity.ContactSkills.Select(cs => new SkillGetViewModel()
                {
                    Id = cs.Skill.Id,
                    Name = cs.Skill.Name,
                    LevelId = cs.Skill.LevelId,
                    LevelName = cs.Skill.Level.Name
                })
            }).ToList();

            return contactViewModels;
        }

        public async Task<int> AddAsync(ContactSaveViewModel contactViewModel)
        {
            var currentUserId = int.Parse(_authClaimsService.GetUserId());

            var contactEntity = new Contact()
            {
                Firstname = contactViewModel.Firstname,
                Lastname = contactViewModel.Lastname,
                Address = contactViewModel.Address,
                Email = contactViewModel.Email,
                MobilePhoneNumber = contactViewModel.MobilePhoneNumber,
                UserId = currentUserId
            };

            return await _contactsRepository.AddAsync(contactEntity);
        }

        public async Task UpdateAsync(int id, ContactSaveViewModel contactViewModel)
        {
            var contactEntity = await ValidateOperationAsync(id);

            contactEntity.Firstname = contactViewModel.Firstname;
            contactEntity.Lastname = contactViewModel.Lastname;
            contactEntity.Address = contactViewModel.Address;
            contactEntity.Email = contactViewModel.Email;
            contactEntity.MobilePhoneNumber = contactViewModel.MobilePhoneNumber;

            await _contactsRepository.UpdateAsync(contactEntity);
        }

        public async Task DeleteAsync(int id)
        {
            var contactEntity = await ValidateOperationAsync(id);

            await _contactsRepository.DeleteAsync(contactEntity);
        }

        public async Task AssignSkills(IEnumerable<SkillsToContactViewModel> skillsToContacts)
        {
            var currentUserId = int.Parse(_authClaimsService.GetUserId());

            var contactIds = skillsToContacts.Select(sk => sk.ContactId).ToArray();
            var hasContactsFromAnotherUser = _contactsRepository.Find(c => c.UserId != currentUserId && contactIds.Contains(c.Id)).Any();

            if (hasContactsFromAnotherUser)
            {
                throw new UnauthorizedAccessException("You cannot assign skills to the contacts of another user.");
            }

            var contactSkills = skillsToContacts
                                .SelectMany(s => s.SkillIds.DefaultIfEmpty(), (contact, skillId) => new
                                {
                                    contact.ContactId,
                                    SkillId = skillId
                                })
                                .Select(p => new ContactSkill()
                                {
                                    ContactId = p.ContactId,
                                    SkillId = p.SkillId
                                }).ToArray();

            await _contactsRepository.AddContactSkillsAsync(contactSkills);
        }

        private async Task<Contact> ValidateOperationAsync(int id, bool authorizationEnabled = true)
        {
            var contactEntity = authorizationEnabled ? await _contactsRepository.GetAsync(id) : await _contactsRepository.GetWithSkillsAsync(id);

            if (contactEntity == null)
            {
                throw new NoResourceFoundException("No contact has been found.");
            }

            if (authorizationEnabled)
            {
                var currentUserId = int.Parse(_authClaimsService.GetUserId());
                if (contactEntity.UserId != currentUserId)
                {
                    throw new UnauthorizedAccessException("This contact belongs to another user.");
                }
            }

            return contactEntity;
        }
    }
}
