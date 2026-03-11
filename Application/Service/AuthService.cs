using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<string> RegisterAsync(RegisterDto registerDto, string pictureUrl)
        {
            string hashedPassword = _authRepository.Hash(registerDto.Password);
            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                Password = hashedPassword,
                Address = registerDto.Address,
                ProfilePicture = pictureUrl
            };

            var registeredUser = await _authRepository.RegisterAsync(user);
            return registeredUser;
        }
        public async Task<string> LoginAsync(string email, string password)
        {
            string hashedPassword = _authRepository.Hash(password);
            var user = await _authRepository.LoginAsync(email, hashedPassword);
            if(user == null) return null;

            var jetToken = _authRepository.JwtTokenGenerate(user);

            return jetToken;
        }
        public Task<decimal> ForgotPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> ResetPasswordAsync(string email, decimal otp, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> VerifyOtpAsync(string email, decimal otp)
        {
            throw new NotImplementedException();
        }
    }
}
