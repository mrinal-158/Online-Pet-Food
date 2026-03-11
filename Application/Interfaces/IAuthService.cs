using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto registerDto, string pictureUrl);
        Task<string> LoginAsync(string email, string password);
        Task<decimal> VerifyOtpAsync(string email, decimal otp);
        Task<decimal> ForgotPasswordAsync(string email);
        Task<decimal> ResetPasswordAsync(string email, decimal otp, string newPassword);
    }
}
