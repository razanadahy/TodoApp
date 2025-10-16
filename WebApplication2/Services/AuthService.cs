using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.Helpers;
using WebApplication2.Models;
using WebApplication2.Models.DTO;
using WebApplication2.Repositories;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class AuthService: IAuthService
{
    private readonly UserImpl _userImpl;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(UserImpl userImpl, IConfiguration configuration, ILogger<AuthService> logger)
    {
        _userImpl = userImpl;
        _configuration = configuration;
        _logger = logger;
    }

    private string GenerateToken(Users user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);
       return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO)
    {
        _logger.LogInformation("Registering user with email {email}", registerDTO.Email);
        if (await _userImpl.getByEmailAsync(registerDTO.Email) == null)
        {
            var user = new Users
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                Password = PasswordHasher.HashPassword(registerDTO.Password),
                Role = registerDTO.Role,
                Created = DateTime.Now,
            };
            user = await _userImpl.CreatUserAsync(user);
            _logger.LogInformation("User with email {email} created", user.Email);
            string token = GenerateToken(user);
            return new AuthResponseDTO
            {
                Token = token,
                Email = user.Email,
                Name = user.UserName,
                UserId = user.UserId,
                Role = user.Role
            };
        }
        _logger.LogWarning(" email {email} déjà existe", registerDTO.Email);
        throw new Exception("Email already exists");
    }

    public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO)
    {
        _logger.LogInformation("Logining user with email {email}", loginDTO.Email);
        
        var user = await _userImpl.getByEmailAsync(loginDTO.Email);

        if (user==null || !PasswordHasher.VerifyHashedPassword(loginDTO.Password, user.Password))
        {
            _logger.LogError("Failed to login with email {email}", loginDTO.Email);
            throw new UnauthorizedAccessException("Invalid credentials");
        }
        _logger.LogInformation("User with email {email} logged in", loginDTO.Email);
        string token = GenerateToken(user);
        return new AuthResponseDTO
        {
            Token = token,
            Email = user.Email,
            Name = user.UserName,
            UserId = user.UserId,
            Role = user.Role
        };
    }

    public async Task<List<UserDTO>> getAllAsync()
    {
        _logger.LogInformation("Fetch all User");
        var user = await _userImpl.GetAllAsync();
        return user.Select(u => new UserDTO 
        { 
            UserId = u.UserId,
            Email = u.Email,
            Name = u.UserName,
            created_at = u.Created
        }).ToList();
    }
}