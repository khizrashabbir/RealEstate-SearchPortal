using Application.DTOs;
using Application.Interfaces;
using BCrypt.Net;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest req);
    Task<AuthResponse> LoginAsync(LoginRequest req);
}

public class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository users, IConfiguration config)
    {
        _users = users;
        _config = config;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest req)
    {
        var existing = await _users.GetByEmailAsync(req.Email);
        if (existing != null) throw new InvalidOperationException("Email already in use");

        var user = new User
        {
            Email = req.Email.Trim().ToLowerInvariant(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password)
        };

        var id = await _users.CreateAsync(user);
        var token = GenerateJwt(id, user.Email);
        return new AuthResponse(id, user.Email, token);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest req)
    {
        var user = await _users.GetByEmailAsync(req.Email.Trim().ToLowerInvariant());
        if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        var token = GenerateJwt(user.Id, user.Email);
        return new AuthResponse(user.Id, user.Email, token);
    }

    private string GenerateJwt(long userId, string email)
    {
        var key = _config["Jwt:Key"] ?? throw new InvalidOperationException("Missing Jwt:Key");
        var issuer = _config["Jwt:Issuer"] ?? "RealEstateApi";
        var audience = _config["Jwt:Audience"] ?? "RealEstateClient";

        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email)
        };

        var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
