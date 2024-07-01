using GhostQA_API.DTO_s;
using GhostQA_API.Helper;
using GhostQA_API.Models;
using GhostQA_API.TenantInfra.EntityMigration;
using GhostQA_API.TenantInfra.EntityModal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GhostQA_API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DBHelper _helper;
        private readonly Migration_Helper _migrationHelper;

        public LoginController(UserManager<ApplicationUser> userManager, DBHelper helper, Migration_Helper migrationHelper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _helper = helper ?? throw new ArgumentNullException(nameof(helper));
            _migrationHelper = migrationHelper ?? throw new ArgumentNullException(nameof(migrationHelper));
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(Dto_Login loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
                {
                    return BadRequest(new Dto_Response { status = "false", message = "User Name or Password must not be blank" });
                }

                var user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (user == null)
                {
                    return BadRequest(new Dto_Response { status = "false", message = "User not found" });
                }

                if ((bool)user.IsDisabled)
                {
                    return StatusCode(403, new { status = "error", message = "User account is disabled. Please contact the administrator" });
                }

                if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                {
                    return BadRequest(new Dto_Response { status = "false", message = "Password is incorrect" });
                }

                string result = await _helper.VerifyUser(user.Email, user.PasswordHash);
                if (!result.Contains("Success"))
                {
                    return Ok(new Dto_Response { status = "false", message = result });
                }

                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, loginDTO.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _helper.GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    result = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Dto_Response { status = "false", message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPost("UserSignUpTenantCreation")]
        public async Task<IActionResult> UserSignUpTenantCreation(Dto_Tenant _tenant)
        {
            try
            {
                await _migrationHelper.RunMigrations(_tenant);
                return Ok(new Dto_Response { status = "true", message = "Tenant created successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new Dto_Response { status = "false", message = $"Error creating tenant: {ex.Message}" });
            }
        }
    }
}
