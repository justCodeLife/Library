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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Areas.Admin.Controllers
{
    [Area("Admin")]
//    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            model = _userManager.Users.Select(u => new UserListViewModel()
            {
                id = u.Id,
                Name = u.FirstName + " " + u.Lastname,
                Email = u.Email
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            UserViewModel model = new UserViewModel();
            model.ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();
            return PartialView("_AddUser", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(UserViewModel model, string redirectURL)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    Lastname = model.FirstName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Username,
                    gender = model.gender
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ApplicationRole approle = await _roleManager.FindByIdAsync(model.ApplicationRoleID);
                    if (approle != null)
                    {
                        IdentityResult roleresult = await _userManager.AddToRoleAsync(user, approle.Name);
                        if (roleresult.Succeeded)
                        {
                            return PartialView("_successfulResponse", redirectURL);
                        }
                    }
                }
            }

            model.ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();
            return PartialView("_AddUser", model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            EditUserViewModel model = new EditUserViewModel();
            model.ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    model.FirstName = user.FirstName;
                    model.Lastname = user.Lastname;
                    model.Email = user.Email;
                    model.PhoneNumber = user.PhoneNumber;
                    model.gender = user.gender;
                    model.ApplicationRoleID = _roleManager.Roles
                        .Single(r => r.Name == _userManager.GetRolesAsync(user).Result.Single()).Id;
                }
            }

            return PartialView("_EditUser", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string id, EditUserViewModel model, string redirectURL)
        {
//            if (ModelState.IsValid)
//            {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.Lastname = model.Lastname;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.gender = model.gender;
                string existingRole = _userManager.GetRolesAsync(user).Result.Single();
                string existingRoleID = _roleManager.Roles.Single(r => r.Name == existingRole).Id;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (existingRoleID != model.ApplicationRoleID)
                    {
                        IdentityResult roleresult = await _userManager.RemoveFromRoleAsync(user, existingRole);
                        if (roleresult.Succeeded)
                        {
                            ApplicationRole approle = await _roleManager.FindByIdAsync(model.ApplicationRoleID);

                            if (approle != null)
                            {
                                IdentityResult newrole = await _userManager.AddToRoleAsync(user, approle.Name);
                                if (newrole.Succeeded)
                                {
                                    return PartialView("_successfulResponse", redirectURL);
                                }
                            }
                        }
                    }

                    return PartialView("_successfulResponse", redirectURL);
                }
            }
//            }

            model.ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();
            return PartialView("_EditUser", model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            string name = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser au = await _userManager.FindByIdAsync(id);
                if (au != null)
                {
                    name = au.FirstName + " " + au.Lastname;
                }
            }

            return PartialView("_deleteUser", name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id, IFormCollection form)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser au = await _userManager.FindByIdAsync(id);
                if (au != null)
                {
                    IdentityResult userResult = await _userManager.DeleteAsync(au);
                    if (userResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View("Index");
        }
    }
}