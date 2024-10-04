using System.Security.Claims;
using AutoMapper;
using backend.models.database;
using backend.models.dto.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(CreateAccountDTO registerDTO) {
            var userAlreadyExists = await _userManager.Users.AnyAsync(u => u.Email == registerDTO.Email);

            if(userAlreadyExists)
                return Conflict(new { message = "A book with this WorkId already exists in users library" });

            User newUser = _mapper.Map<User>(registerDTO);

            IdentityResult result = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if(!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "Failed to register user" });

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUser(string userEmail) {
            var foundUser = await _userManager.FindByEmailAsync(userEmail);

            if(foundUser == null)
                return BadRequest("User with this email doesn't exist");

            var result = await _userManager.DeleteAsync(foundUser);

            if(!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "Failed to remove user" });

            return Ok("User succesfully removed");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleAlreadyExists = await _roleManager.Roles.AnyAsync(r => r.Name == r.Name);

            if(roleAlreadyExists)
                return Conflict(new { message = "A role with this name already exists" });

            IdentityRole<int> role = new IdentityRole<int>() {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };

            try {
                await _roleManager.CreateAsync(role);

                return Ok("Role created succesfully");

            } catch {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(string userEmail, string roleName)
        {
            var roleExists = await _roleManager.Roles.AnyAsync(r => r.Name == r.Name);

            if(!roleExists)
                return BadRequest("Role with this name doesn't exist");

            var foundUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if(foundUser == null)
                return BadRequest("User with this email doesn't exist");

            try {
                var result = await _userManager.AddToRoleAsync(foundUser, roleName);

                if(!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "Failed to add user to role" });

                return Ok("User succesfully added to role");

            } catch {
                throw;
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserEmail()
        {
            // Find the user ID from the authenticated user's claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Or another claim type

            if (userId == null)
            {
                return BadRequest("User ID not found in token.");
            }

            // Retrieve the user from the Identity system (or your custom user store)
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Return the user's email
            return Ok(new { Email = user.Email });
        }
    }
}