using BusinessDomain.DTOs;
using BusinessDomain.Exceptions;
using BusinessDomain.Interfaces;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.UnitofWork;
using Mapster;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;

            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync(Guid userId)
        {
            User user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
                throw new UserNotFoundException();
            if (user.IsAdmin)
                return await GetAllProductsForAdminAsync();
            return await GetAllProductsForCustomersAsync();
        }

        public async Task<List<ProductDTO>> GetAllProductsForAdminAsync()
        {
            List<Product> products = await _productRepository.GetAll().ToListAsync();
            return products.Adapt<List<ProductDTO>>();
        }

        public async Task<List<ProductDTO>> GetAllProductsForCustomersAsync()
        {
            List<Product> products = await _productRepository.GetAll().Where(p => !p.IsDeleted).ToListAsync();
            return products.Adapt<List<ProductDTO>>();
        }


        public async Task<ProductDTO> GetProductByIdAsync(Guid id)
        {
            Product product = await _productRepository.GetByIdAsync(id);
            return product.Adapt<ProductDTO>();
        }

        public async Task<ProductDTO> AddProductAsync(ProductDTO productIn)
        {
            Product product = productIn.Adapt<Product>();

            if (!(product.Quantity is null))
                UpdateProductStatus(product);
            
            await _productRepository.AddAsync(product);
            if (await _unitOfWork.SaveAsync() == 0)
                throw new NoSavedChangesException();
            return product.Adapt<ProductDTO>();
        }

        public async Task<ProductDTO> UpdateProductAsync(Guid id, ProductDTO productIn)
        {
            Product thisProduct = await _productRepository.GetByIdAsync(id);
            if (thisProduct == null)
            {
                throw new ProductNotFoundException();
            }

            //thisProduct.Name = productIn.Name;
            //thisProduct.Description = productIn.Description;
            //thisProduct.Amount = productIn.Amount;
            //thisProduct.Type = productIn.Type;
            //thisProduct.Status = productIn.Status;

            TypeAdapterConfig<ProductDTO, Product>
                .NewConfig()
                .IgnoreNullValues(true)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedOn)
                .Ignore(dest => dest.UpdatedOn)
                .Ignore(dest => dest.IsDeleted);

            productIn.Adapt(thisProduct);


            thisProduct.UpdatedOn = DateTime.Now;

            if (!(thisProduct.Quantity is null))
                UpdateProductStatus(thisProduct);

            _productRepository.Update(thisProduct);
            if (await _unitOfWork.SaveAsync() == 0)
                throw new NoSavedChangesException();

            return thisProduct.Adapt<ProductDTO>();
        }

        public async Task UpdateProductStockAsync(Product product, int quantity)
        {
            if (product.Quantity < quantity)
                throw new InsufficientStockException($"Insufficient stock for {product.Name}");

            product.Quantity -= quantity;
            UpdateProductStatus(product);

            _productRepository.Update(product);
            if (await _unitOfWork.SaveAsync() == 0)
                throw new NoSavedChangesException();
        }

        private void UpdateProductStatus(Product product)
        {
            string status;
            switch (product.Quantity)
            {
                case 0:
                    status = "Out of stock";
                    break;
                case <= 3:
                    status = "Limited stock";
                    break;
                default:
                    status = "In stock";
                    break;
            }
            product.Status = status;
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            Product product = await _productRepository.GetByIdAsync(id);
            if (product is null)
                throw new ProductNotFoundException();
            product.IsDeleted = true;
            _productRepository.Update(product);
            if (await _unitOfWork.SaveAsync() == 0)
                throw new NoSavedChangesException();
        }
    }
}
