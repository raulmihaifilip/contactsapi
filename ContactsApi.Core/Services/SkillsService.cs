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
    public class SkillsService : ISkillsService
    {
        private readonly ISkillsRepository _skillsRepository;
        private readonly IAuthClaimsService _authClaimsService;

        public SkillsService(IAuthClaimsService authClaimsService, ISkillsRepository skillsRepository)
        {
            _authClaimsService = authClaimsService;
            _skillsRepository = skillsRepository;
        }

        public async Task<SkillGetViewModel> GetAsync(int id)
        {
            var skillEntity = await ValidateOperationAsync(id, false);

            var skillViewModel = new SkillGetViewModel()
            {
                Id = skillEntity.Id,
                Name = skillEntity.Name,
                LevelId = skillEntity.LevelId,
                LevelName = skillEntity.Level.Name
            };

            return skillViewModel;
        }

        public async Task<IList<SkillGetViewModel>> GetAllAsync()
        {
            var skillsEntities = await _skillsRepository.GetAllWithLevelAsync();

            //use here Automapper
            var skillViewModels = skillsEntities.Select(skillEntity => new SkillGetViewModel()
            {
                Id = skillEntity.Id,
                Name = skillEntity.Name,
                LevelId = skillEntity.LevelId,
                LevelName = skillEntity.Level.Name
            }).ToList();

            return skillViewModels;
        }

        public async Task<int> AddAsync(SkillSaveViewModel skillViewModel)
        {
            var skillEntity = new Skill()
            {
                Name = skillViewModel.Name,
                LevelId = skillViewModel.LevelId
            };

            return await _skillsRepository.AddAsync(skillEntity);
        }

        public async Task UpdateAsync(int id, SkillSaveViewModel skillViewModel)
        {
            var skillEntity = await ValidateOperationAsync(id);

            skillEntity.Name = skillViewModel.Name;
            skillEntity.LevelId = skillViewModel.LevelId;

            await _skillsRepository.UpdateAsync(skillEntity);
        }

        public async Task DeleteAsync(int id)
        {
            var skillEntity = await ValidateOperationAsync(id);
            await _skillsRepository.DeleteAsync(skillEntity);
        }

        private async Task<Skill> ValidateOperationAsync(int id, bool authorizationEnabbled = true)
        {
            var skillEntity = authorizationEnabbled ? await _skillsRepository.GetWithContactsAsync(id) : await _skillsRepository.GetWithLevelAsync(id);

            if (skillEntity == null)
            {
                throw new NoResourceFoundException("No skill has been found.");
            }

            if (authorizationEnabbled)
            {
                var currentUserId = int.Parse(_authClaimsService.GetUserId());
                var isShared = skillEntity.ContactSkills.Any(cs => cs.Contact.UserId != currentUserId);
                if (isShared)
                {
                    throw new UnauthorizedAccessException("This skill is shared with another user(s).");
                }
            }

            return skillEntity;
        }
    }
}
