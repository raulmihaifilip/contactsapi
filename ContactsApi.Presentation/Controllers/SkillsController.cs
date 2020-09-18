using System.Threading.Tasks;
using ContactsApi.Core.Interfaces;
using ContactsApi.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.Presentation.Controllers
{
    public class SkillsController : BaseController
    {
        private readonly ISkillsService _skillsService;

        public SkillsController(ISkillsService skillsService)
        {
            _skillsService = skillsService;
        }

        /// <summary>
        /// Get skill
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _skillsService.GetAsync(id));
        }

        /// <summary>
        /// Create skill and return the id
        /// </summary>
        /// <param name="skillViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SkillSaveViewModel skillViewModel)
        {
            return Ok(await _skillsService.AddAsync(skillViewModel));
        }

        /// <summary>
        /// Update skill
        /// </summary>
        /// <param name="id"></param>
        /// <param name="skillViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SkillSaveViewModel skillViewModel)
        {
            await _skillsService.UpdateAsync(id, skillViewModel);
            return Ok();
        }

        /// <summary>
        /// Delete skill
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _skillsService.DeleteAsync(id);
            return Ok();
        }
    }
}
