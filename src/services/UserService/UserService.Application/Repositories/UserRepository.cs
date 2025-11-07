using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Contexts;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            User userEntity = new
            (
                user.Id,
                user.Role,
                user.Username,
                user.PasswordHash,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Phone
            );
            
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsername(string userName)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == userName);
        }
    }
}
