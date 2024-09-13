using AutoMapper;
using backend.models.database;
using backend.models.dto.create;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers {
    internal class AuthController : ControllerBase {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterDTO registerDTO) {
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
    }
}