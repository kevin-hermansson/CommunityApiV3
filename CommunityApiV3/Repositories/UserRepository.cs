using CommunityApiV3.Data;
using CommunityApiV3.Models;
using CommunityApiV3.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityApiV3.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CommunityDbContext _db;

        public UserRepository(CommunityDbContext db)
        {
            _db = db;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }
        public async Task<User?> GetByIdAsync(int id)

        {
            return await _db.Users.FindAsync(id);
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
           return await _db.Users.FirstOrDefaultAsync(u=> u.Username == username);
        }
        public async Task<int> AddAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return user.Id;
        }
        public async Task UpdateAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }




      
    }
}
