using WebApplication2.Models;
using WebApplication2.Models.DTO;

namespace WebApplication2.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<Users>>GetAllAsync();
    Task<Users> CreatUserAsync(Users user);
    Task<Users?> getByEmailAsync(string email);
}