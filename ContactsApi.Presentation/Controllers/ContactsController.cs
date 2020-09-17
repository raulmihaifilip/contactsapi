using ContactsApi.Core.Interfaces;
using ContactsApi.Core.ViewModels;
using ContactsApi.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsApi.Web.Controllers
{
    public class ContactsController : BaseController
    {
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        /// <summary>
        /// Get contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _contactsService.GetAsync(id));
        }

        /// <summary>
        /// Create contact
        /// </summary>
        /// <param name="contactViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactSaveViewModel contactViewModel)
        {
            await _contactsService.AddAsync(contactViewModel);
            return Ok();
        }

        /// <summary>
        /// Update contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contactViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContactSaveViewModel contactViewModel)
        {
            await _contactsService.UpdateAsync(id, contactViewModel);
            return Ok();
        }

        /// <summary>
        /// Delete contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactsService.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Assign skills to contacts
        /// </summary>
        /// <param name="skillsToContacts"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> AssignSkills([FromBody] IEnumerable<SkillsToContactViewModel> skillsToContacts)
        {
            await _contactsService.AssignSkills(skillsToContacts);
            return Ok();
        }
    }
}
