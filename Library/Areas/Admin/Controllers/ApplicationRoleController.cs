using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.Areas.Admin.Controllers
{
    [Area("Admin")]
//    [Authorize(Roles = "Admin")]
    public class ApplicationRoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationRoleController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ApplicationRoleViewModel> model = new List<ApplicationRoleViewModel>();
            model = _roleManager.Roles.Select(r => new ApplicationRoleViewModel()
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                NumberOfUsers = _userManager.Users.Count()
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddEditRole(string id)
        {
            ApplicationRoleViewModel model = new ApplicationRoleViewModel();
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    model.Id = applicationRole.Id;
                    model.Name = applicationRole.Name;
                    model.Description = applicationRole.Description;
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return PartialView("_AddEditApplicationRole", model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            string name = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole ar = await _roleManager.FindByIdAsync(id);
                if (ar != null)
                {
                    name = ar.Name;
                }
            }

            return PartialView("_deleteRole", name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id, IFormCollection form)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole ar = await _roleManager.FindByIdAsync(id);
                if (ar != null)
                {
                    IdentityResult roleResult = _roleManager.DeleteAsync(ar).Result;
                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEditRole(string id, ApplicationRoleViewModel model, string redirectURL)
        {
            if (ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(id))
                {
                    ApplicationRole applicationRole = new ApplicationRole();
                    applicationRole.Name = model.Name;
                    applicationRole.Description = model.Description;
                    IdentityResult roleResult = await _roleManager.CreateAsync(applicationRole);

                    if (roleResult.Succeeded)
                    {
                        return PartialView("_successfulResponse", redirectURL);
                    }
                }
                else
                {
                    ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
                    applicationRole.Name = model.Name;
                    applicationRole.Description = model.Description;
                    IdentityResult roleResult = await _roleManager.UpdateAsync(applicationRole);
                    if (roleResult.Succeeded)
                    {
                        return PartialView("_successfulResponse", redirectURL);
                    }
                }
            }

            return PartialView("_AddEditApplicationRole", model);
        }
    }
}