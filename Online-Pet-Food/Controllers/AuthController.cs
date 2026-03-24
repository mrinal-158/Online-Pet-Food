using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Online_Pet_Food.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IWebHostEnvironment _env;
        public AuthController(IAuthService authService, IWebHostEnvironment env)
        {
            _authService = authService;
            _env = env;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            string? pictureUrl = null;

            if (registerDto.ProfilePicture != null && registerDto.ProfilePicture.Length > 0)
            {
                var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
                var imagesDir = Path.Combine(webRoot, "images");
                Directory.CreateDirectory(imagesDir);

                var ext = Path.GetExtension(registerDto.ProfilePicture.FileName);
                var fileName = $"{Guid.NewGuid():N}{ext}";
                var filePath = Path.Combine(imagesDir, fileName);

                await using (var stream = System.IO.File.Create(filePath))
                {
                    await registerDto.ProfilePicture.CopyToAsync(stream);
                }

                pictureUrl = "/images/" + fileName;
            }
            var result = await _authService.RegisterAsync(registerDto, pictureUrl);
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
        {
            var jwtToken = await _authService.LoginAsync(email, password);
            if(jwtToken == null) return Unauthorized(new { Message = "Invalid email or password" });

            return Ok(new { Message = "Login successful", Token = jwtToken });
        }
    }
}
