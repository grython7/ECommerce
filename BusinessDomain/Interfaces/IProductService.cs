using BusinessDomain.DTOs;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.UnitofWork;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Interfaces
{
    public interface IProductService
    {
        public Task<List<ProductDTO>> GetAllProductsAsync(Guid id);
        public Task<ProductDTO> GetProductByIdAsync(Guid id);
        public Task<ProductDTO> AddProductAsync(ProductDTO productIn);
        public Task<ProductDTO> UpdateProductAsync(Guid id, ProductDTO productIn);
        public Task SoftDeleteAsync(Guid id);
    }
}
