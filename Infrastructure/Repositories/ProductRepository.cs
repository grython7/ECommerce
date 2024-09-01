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
    public class ProductRepository : IProductRepository
    {
        private readonly MyContext _context;
        public ProductRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.OrderItems)
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public IQueryable<Product> GetAll()
        {
            return _context.Products.Include(p => p.OrderItems);
        }
        public async Task AddAsync(Product entity)
        {
            await _context.AddAsync(entity);
        }
        public void Update(Product product)
        {
            _context.Update(product);
        }
    }
}
