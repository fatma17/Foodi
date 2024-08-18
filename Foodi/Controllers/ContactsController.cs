using Foodi.Core.Consts;
using Foodi.Core.Models;
using Foodi.Core.Services;
using Foodi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foodi.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }


        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> Index()
        {
            return View(await _contactsService.GetAll());
        }


        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {

            if(!ModelState.IsValid)
            { 
                return PartialView("_ContactForm", contact);
            }

            await _contactsService.Create(contact);
            return Json(new { success = true });


           // if (ModelState.IsValid)
           // {
           //     await _contactsService.Create(contact);
           //     return Json(new { success = true, message = "Thank you for contacting us!" });
           // }

           // /*Collect error messages*/
           //var errors = ModelState.ToDictionary(
           //    kvp => kvp.Key,
           //    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
           //);
           // return Json(new { success = false, errors });
           // var errorList = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList(); // Collect error messages
           // return Json(new { success = false, message = string.Join(" ", errorList) });
        }
    }
}
