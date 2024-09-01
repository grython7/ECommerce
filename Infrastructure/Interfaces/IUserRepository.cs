using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetByIdAsync(Guid id);
        public Task<User> GetByEmailAsync(String email);
        public Task AddAsync(User user);
        public void Update(User user);
    }
}
