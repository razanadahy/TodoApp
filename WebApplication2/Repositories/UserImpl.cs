using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Models.DTO;
using WebApplication2.Repositories.Interfaces;

namespace WebApplication2.Repositories;

public class UserImpl : IUserRepository
{
    private readonly AppDBContext _context;

    public UserImpl(AppDBContext context)
    {
        _context = context;
    }

    public async Task<List<Users>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<Users> CreatUserAsync(Users user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<Users?> getByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}