using Infrastructure.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;
using BusinessDomain.DTOs;
using BusinessDomain.Interfaces;
using Infrastructure.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using BusinessDomain.Exceptions;

namespace BusinessDomain.Services
{
    public class OrderService : IOrderService
    {
        private const int TAX = 14;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        private readonly IUnitOfWork _unitOfWork;
        // Since we are going to need the ProductService to update the quantity and status of the products
        private readonly IProductService _productService;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IProductService productService, IUserService userService)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;

            _unitOfWork = unitOfWork;

            _productService = productService;
        }

        public async Task<List<OrderDTO>> GetOrdersByUserIdAsync(Guid id)
        {
            // Check if user exists
            if (await _userRepository.GetByIdAsync(id) is null)
                throw new UserNotFoundException();

            List<Order> orders = await _orderRepository.GetAll()
                .Where(o => o.Customer.Id.Equals(id) & !o.IsDeleted)
                .ToListAsync();

            return orders.Adapt<List<OrderDTO>>();
        }

        public async Task<OrderDTO> PlaceOrderAsync(OrderDTO orderIn)
        {
            Order order = orderIn.Adapt<Order>();

            // Validate products and calculate item costs
            await ValidateAndCalculateOrderItemsAsync(order);

            // Calculate the total amount for the order
            order.Tax = TAX;
            order.TotalAmount = (order.Tax + 100) * order.Amount / 100;

            // Save the order
            await _orderRepository.AddAsync(order);
            if (await _unitOfWork.SaveAsync() == 0)
                throw new NoSavedChangesException();

            return order.Adapt<OrderDTO>();
        }

        private async Task ValidateAndCalculateOrderItemsAsync(Order order)
        {
            foreach (OrderItem oi in order.OrderItems)
            {
                Product product = await _productRepository.GetByIdAsync(oi.ProductId);
                if (product is null)
                    throw new ProductNotFoundException();
                oi.Cost = product.Amount * oi.Quantity;
                order.Amount += oi.Cost;
            }
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            Order order = await _orderRepository.GetByIdAsync(id);
            if (order is null)
                throw new OrderNotFoundException();
            order.IsDeleted = true;
            _orderRepository.Update(order);
            if (await _unitOfWork.SaveAsync() == 0)
                throw new NoSavedChangesException();
        }
    }
}
