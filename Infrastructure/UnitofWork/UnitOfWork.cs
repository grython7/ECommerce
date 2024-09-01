using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyContext _context;
        
        public UnitOfWork(MyContext context)
        {
            _context = context;   
        }
        public async Task<int> SaveAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
