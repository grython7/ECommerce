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
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly MyContext _context;
        public OrderItemRepository(MyContext context)
        {
            _context = context;
        }

        public OrderItem GetById(Guid id)
        {
            return _context.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .FirstOrDefault(oi => oi.Id == id);
        }
    }
}
