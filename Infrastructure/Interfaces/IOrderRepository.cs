using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IOrderRepository
    {
        public Task<Order> GetByIdAsync(Guid id);
        public IQueryable<Order> GetAll();
        public Task AddAsync(Order order);
        public void Update(Order order);
    }
}
