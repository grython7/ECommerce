using Infrastructure.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;
using BusinessDomain.DTOs;

namespace BusinessDomain.Interfaces
{
    public interface IOrderService
    {
        public Task<List<OrderDTO>> GetOrdersByUserIdAsync(Guid id);
        public Task<OrderDTO> PlaceOrderAsync(OrderDTO orderIn);
        public Task SoftDeleteAsync(Guid id);

    }
}
