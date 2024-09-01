using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        public Task<Product> GetByIdAsync(Guid id);
        public IQueryable<Product> GetAll();
        public Task AddAsync(Product entity);
        public void Update(Product product);
    }
}
