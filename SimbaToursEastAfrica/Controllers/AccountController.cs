﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SimbaToursEastAfrica.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> identityUserManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = identityUserManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            CreateRoles();
        }

        private void CreateRoles()
        {
            var user = new IdentityUser();
            user.UserName = "martinokello@martinlayooinc.com";
            user.Email = "martinokello@martinlayooinc.com";

            string userPWD = "deltaX!505";

            var userChecked = _userManager.FindByEmailAsync(user.Email).ConfigureAwait(true).GetAwaiter().GetResult();
            if (userChecked == null)
            {
                IdentityResult chkUser = _userManager.CreateAsync(user, userPWD).ConfigureAwait(true).GetAwaiter().GetResult();
            }
            if (_roleManager.FindByNameAsync("Administrator").ConfigureAwait(true).GetAwaiter().GetResult() == null)
            {
                var adminRole = new IdentityRole();
                adminRole.Name = "Administrator";
                var result = _roleManager.CreateAsync(adminRole).ConfigureAwait(true).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, "Administrator").ConfigureAwait(true).GetAwaiter().GetResult();

            }

            if (_roleManager.FindByNameAsync("StandardUser").ConfigureAwait(true).GetAwaiter().GetResult() == null)
            {
                var roleStandard = new IdentityRole();
                roleStandard.Name = "StandardUser";
                var result2 = _roleManager.CreateAsync(roleStandard).ConfigureAwait(true).GetAwaiter().GetResult();
            }
            if (_roleManager.FindByNameAsync("Guest").ConfigureAwait(true).GetAwaiter().GetResult() == null)
            {
                var roleGuest = new IdentityRole();
                roleGuest.Name = "Guest";
                var result2 = _roleManager.CreateAsync(roleGuest).ConfigureAwait(true).GetAwaiter().GetResult();
            }
        }
        [HttpGet]
        public JsonResult VerifyLoggedInUser()
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).ConfigureAwait(true).GetAwaiter().GetResult();

                return Json(new {name= user.Email, IsLoggedIn = true, IsAdministrator = _userManager.IsInRoleAsync(user, "Administrator").ConfigureAwait(true).GetAwaiter().GetResult() });
            }
            return Json(new { IsLoggedIn = false });
        }
        [HttpPost]
        public JsonResult Register([FromBody] UserDetails userDetails)
        {
            if (userDetails.password != userDetails.repassword)
            {
                ModelState.AddModelError(string.Empty, "Password don't match");
                return Json(new{ Error= "Passwords don't match", IsRegistered = false });
            }

            var newUser = new IdentityUser
            {
                UserName = userDetails.emailAddress,
                Email = userDetails.emailAddress
            };

            var userCreationResult = _userManager.CreateAsync(newUser, userDetails.password).ConfigureAwait(true).GetAwaiter().GetResult();
            if (!userCreationResult.Succeeded)
            {
                foreach (var error in userCreationResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return Json(new { IsRegistered = false});
            }

            var emailConfirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(newUser).ConfigureAwait(true).GetAwaiter().GetResult();
            var tokenVerificationUrl = Url.Action("VerifyEmail", "Account", new { id = newUser.Id, token = emailConfirmationToken }, Request.Scheme);

            //await _messageService.Send(email, "Verify your email", $"Click <a href=\"{tokenVerificationUrl}\">here</a> to verify your email");
            AddUserAsClaim(newUser);
            return Json(new { IsRegistered = true, IsAdministrator = false});
        }

        public IdentityResult AddUserAsClaim(IdentityUser identityUser)
        {
            //Here’s an example of how to add the “Administrator” role to a user:

            var result = _userManager.AddClaimAsync(identityUser, new Claim(ClaimTypes.Role, "StandardUser")).ConfigureAwait(true).GetAwaiter().GetResult();
            return result;
        }

        [HttpPost]
        public JsonResult Login([FromBody] UserDetails userDetails)
        {
            var user = _userManager.FindByEmailAsync(userDetails.emailAddress).ConfigureAwait(true).GetAwaiter().GetResult();
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");

                return Json(new { IsLoggedIn = false });
            }

            var passwordSignInResult = _signInManager.PasswordSignInAsync(user, userDetails.password, isPersistent: userDetails.keepLoggedIn, lockoutOnFailure: false).ConfigureAwait(true).GetAwaiter().GetResult();
            if (!passwordSignInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");
                return Json(new { IsLoggedIn = false, error= "Invalid Login"});
            }
            
            return Json(new { IsLoggedIn = true, IsAdministrator = _userManager.IsInRoleAsync(user, "Administrator").ConfigureAwait(true).GetAwaiter().GetResult() });
        }

        [HttpPost]
        public JsonResult ForgotPassword(string emailAddress)
        {
            var user = _userManager.FindByEmailAsync(emailAddress).ConfigureAwait(true).GetAwaiter().GetResult();
            if (user == null)
                return Json(new { message = "Reset your password through email link" });

            var passwordResetToken = _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(true).GetAwaiter().GetResult();
            var passwordResetUrl = Url.Action("ResetPassword", "Account", new ResetPassword { Id = user.Id, Token = passwordResetToken }, Request.Scheme);

            //await _messageService.Send(email, "Password reset", $"Click <a href=\"" + passwordResetUrl + "\">here</a> to reset your password");
            return Json(new { message = "Reset your password through email link" });
        }

        [HttpPost]
        public IActionResult ResetPassword([FromBody] ResetPassword resetPassword)
        {
            var user = _userManager.FindByIdAsync(resetPassword.Id).ConfigureAwait(true).GetAwaiter().GetResult();
            if (user == null)
                throw new InvalidOperationException();

            if (resetPassword.Password != resetPassword.Repassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match");

                return Json(new { message = "Password and retyped Passwords don't match" });
            }

            var resetPasswordResult = _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password).ConfigureAwait(true).GetAwaiter().GetResult();
            if (!resetPasswordResult.Succeeded)
            {
                foreach (var error in resetPasswordResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return Json(new { message = "Failed to reset Password" });
            }
            return Json(new { message = "Password reset successfully" });
        }

        [HttpGet]
        public IActionResult Logout()
        {
             _signInManager.SignOutAsync().ConfigureAwait(true).GetAwaiter().GetResult();
            return Json(new { isLoggedIn = false, Message="Logged Out" });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public bool AddUserToRole([FromBody] UserRole userRole)
        {
            var user = _userManager.FindByEmailAsync(userRole.Email).ConfigureAwait(true).GetAwaiter().GetResult();
            var checkedRole = _roleManager.FindByNameAsync(userRole.Role).ConfigureAwait(true).GetAwaiter().GetResult();

            if (!_userManager.IsInRoleAsync(user, checkedRole.Name).ConfigureAwait(true).GetAwaiter().GetResult())
            {
                _userManager.AddToRoleAsync(user, checkedRole.Name).ConfigureAwait(true).GetAwaiter().GetResult();
                return true;
            }
            return false;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public bool RemoveUserFromRole([FromBody] UserRole userRole)
        {
            var user = _userManager.FindByEmailAsync(userRole.Email).ConfigureAwait(true).GetAwaiter().GetResult();
            var checkedRole = _roleManager.FindByNameAsync(userRole.Role).ConfigureAwait(true).GetAwaiter().GetResult();

            if (_userManager.IsInRoleAsync(user, checkedRole.Name).ConfigureAwait(true).GetAwaiter().GetResult())
            {
                _userManager.RemoveFromRolesAsync(user,new string[] { checkedRole.Name }).ConfigureAwait(true).GetAwaiter().GetResult();
                return true;
            }
            return false;
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public Role[] GetAllRoles()
        {
            try
            {
                var roles = _roleManager.Roles.Select(p => new Role { Name = p.Name });
                return roles.ToArray();
            }
            catch (Exception ex)
            {
                return new Role[] { new Role { Name = ex.Message } };
            }
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateRole(string role)
        {
            try
            {
                var newRole = _roleManager.Roles.FirstOrDefault(p => p.Name.ToLower().Equals(role.ToLower().Trim()));

                if(newRole is null)
                {
                    var result =_roleManager.CreateAsync(new IdentityRole(role)).ConfigureAwait(true).GetAwaiter().GetResult();
                    if (result.Succeeded)
                    {
                        return Ok(true.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return BadRequest(false.ToString()); 
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteRole(string role)
        {
            try
            {
                var newRole = _roleManager.Roles.FirstOrDefault(p => p.Name.ToLower().Equals(role.ToLower().Trim()));

                if (!(newRole is null))
                {
                    var result = _roleManager.DeleteAsync(new IdentityRole(role)).ConfigureAwait(true).GetAwaiter().GetResult();
                    if (result.Succeeded)
                    {
                        return Ok(true.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return BadRequest(false.ToString());
        }
    }

    public class Role { public string Name { get; set; } }
}