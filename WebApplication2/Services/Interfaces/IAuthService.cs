using WebApplication2.Models.DTO;

namespace WebApplication2.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO);
    Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);
    Task<List<UserDTO>> getAllAsync();
}