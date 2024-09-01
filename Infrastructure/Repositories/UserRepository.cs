using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyContext _context;
        public UserRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Orders)
                .ThenInclude(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByEmailAsync(String email)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.Email.ToLower()
                .Equals(email.ToLower()))
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.AddAsync(user);
        }

        public void Update(User user)
        {
            _context.Update(user);
        }
    }
}
