using BusinessDomain.DTOs;
using BusinessDomain.Exceptions;
using BusinessDomain.Interfaces;
using BusinessDomain.Services;
using Infrastructure.Entities;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Presentation.Helpers;
using Presentation.Responses;
using Presentation.ViewModels;
using Presentation.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        // Constructor injection of ProductService
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products/all/guid
        [HttpGet("all/{CustomerId}")]
        public async Task<IActionResult> GetAllProducts(Guid CustomerId)
        {
            BaseResponse response;
            try
            {
                List<ProductDTO> productsDTO = await _productService.GetAllProductsAsync(CustomerId);
                List<ProductVMResponse> data = productsDTO.Adapt<List<ProductVMResponse>>();
                response = new SuccessResponse<List<ProductVMResponse>>
                {
                    StatusCode = 200,
                    Message = "Products Retrieved Successfully",
                    Data = data
                };
                return Ok(response);
            }
            catch (UserNotFoundException e)
            {
                response = new BaseResponse
                {
                    StatusCode = 404,
                    Message = e.Message,
                };
                return NotFound(response);
            }
            catch (Exception e) when (e is DbException || e is DbUpdateException)
            {
                response = new BaseResponse
                {
                    StatusCode = 400,
                    Message = "Error retrieving products",
                };
                return BadRequest(response);
            }
        }

        // GET: api/products/guid
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductById(Guid Id)
        {
            BaseResponse response;
            try
            {
                ProductDTO productDTO = await _productService.GetProductByIdAsync(Id);
                if (productDTO == null)
                {
                    response = new BaseResponse
                    {
                        StatusCode = 404,
                        Message = "InValid Product Id",
                    };
                    return NotFound(response);
                }
                else
                {
                    ProductVMResponse data = productDTO.Adapt<ProductVMResponse>();
                    response = new SuccessResponse<ProductVMResponse>
                    {
                        StatusCode = 200,
                        Message = "Product Retrieved Successfully",
                        Data = data
                    };
                    return Ok(response);
                }
            }
            catch (Exception e) when (e is DbException || e is DbUpdateException)
            {
                response = new BaseResponse
                {
                    StatusCode = 400,
                    Message = "Can’t Retrieve Product",
                };
                return BadRequest(response);
            }
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductVMRequest ProductVm)
        {
            BaseResponse response;
            if (!ModelState.IsValid)
            {
                var errorsDict = ModelState.ToErrorDictionary();
                response = new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "InValid Data",
                    Errors = errorsDict
                };
                return BadRequest(response);
            }
            try
            {
                ProductDTO productDTO = await _productService.AddProductAsync(ProductVm.Adapt<ProductDTO>());
                ProductVMResponse data = productDTO.Adapt<ProductVMResponse>();
                response = new SuccessResponse<ProductVMResponse>
                {
                    StatusCode = 200,
                    Message = "Product Added Successfully",
                    Data = data
                };
                return Ok(response);
            }
            catch (Exception e) when (e is NoSavedChangesException || e is DbException || e is DbUpdateException)
            {
                response = new BaseResponse
                {
                    StatusCode = 400,
                    Message = "Can’t Add Product",
                };
                return BadRequest(response);
            }
        }

        // PUT: api/products/update/1
        [HttpPut("update/{Id}")]
        public async Task<IActionResult> UpdateProduct(Guid Id, [FromBody] ProductVMRequest ProductVm)
        {
            BaseResponse response;
            if (!ModelState.IsValid)
            {
                var errorsDict = ModelState.ToErrorDictionary();
                response = new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "InValid Data",
                    Errors = errorsDict
                };
                return BadRequest(response);
            }
            try
            {
                ProductDTO productDTO = await _productService.UpdateProductAsync(Id, ProductVm.Adapt<ProductDTO>());
                ProductVMResponse data = productDTO.Adapt<ProductVMResponse>();
                response = new SuccessResponse<ProductVMResponse>
                {
                    StatusCode = 200,
                    Message = "Product Updated Successfully",
                    Data = data
                };
                return Ok(response);
            }
            catch (ProductNotFoundException e)
            {
                response = new BaseResponse
                {
                    StatusCode = 404,
                    Message = e.Message,
                };
                return NotFound(response);
            }
            catch (Exception e) when (e is NoSavedChangesException || e is DbException || e is DbUpdateException)
            {
                response = new BaseResponse
                {
                    StatusCode = 400,
                    Message = "Can’t Update Product",
                };
                return BadRequest(response);
            }
        }

        // PUT: api/products/1
        [HttpPut("delete/{Id}")]
        public async Task<IActionResult> DeleteProduct(Guid Id)
        {
            BaseResponse response;
            try
            {
                await _productService.SoftDeleteAsync(Id);
                response = new BaseResponse
                {
                    StatusCode = 200,
                    Message = "Product Deleted Successfully",
                };
                return Ok(response);
            }
            catch (ProductNotFoundException e)
            {
                response = new BaseResponse
                {
                    StatusCode = 404,
                    Message = e.Message,
                };
                return NotFound(response);
            }
            catch (Exception e) when (e is NoSavedChangesException || e is DbException || e is DbUpdateException)
            {
                response = new BaseResponse
                {
                    StatusCode = 400,
                    Message = "Can’t Delete Product",
                };
                return BadRequest(response);
            }
        }
    }
}
