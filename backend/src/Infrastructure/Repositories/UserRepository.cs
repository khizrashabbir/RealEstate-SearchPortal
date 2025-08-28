using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) => _db = db;

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<long> CreateAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user.Id;
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        return await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }
}
