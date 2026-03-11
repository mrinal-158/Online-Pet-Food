using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> RegisterAsync(User user);
        Task<User> LoginAsync(string email, string password);
        string JwtTokenGenerate(User user);
        string Hash(string password);
    }
}
