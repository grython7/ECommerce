using BusinessDomain.DTOs;
using BusinessDomain.Exceptions;
using BusinessDomain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Presentation.Helpers;
using Presentation.Responses;
using Presentation.ViewModels;
using System.Data.Common;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        // Constructor injection of OrderService
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/orders/guid
        [HttpGet("user/{CustomerId}")]
        public async Task<IActionResult> GetAllOrders(Guid CustomerId)
        {
            BaseResponse response;
            try
            {
                List<OrderDTO> ordersDTO = await _orderService.GetOrdersByUserIdAsync(CustomerId);
                List<OrderViewModel> data = ordersDTO.Adapt<List<OrderViewModel>>();
                response = new SuccessResponse<List<OrderViewModel>>
                {
                    StatusCode = 200,
                    Message = "Orders Retrieved Successfully",
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
                    Message = "Can’t Retrieve Orders",
                };
                return BadRequest(response);
            }
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderViewModel OrderVM)
        {
            BaseResponse response;
            if (!ModelState.IsValid)
            {
                var errorsDict = ModelState.ToErrorDictionary();
                response = new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Data",
                    Errors = errorsDict
                };
                return BadRequest(response);
            }

            try
            {
                OrderDTO orderDTO = await _orderService.PlaceOrderAsync(OrderVM.Adapt<OrderDTO>());
                OrderViewModel data = orderDTO.Adapt<OrderViewModel>();
                response = new SuccessResponse<OrderViewModel>
                {
                    StatusCode = 200,
                    Message = "Order Added Successfully",
                    Data = data
                };
                return Ok(response);
            }
            catch (ProductNotFoundException)
            {
                // to: Verify this exception
                response = new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "InValid Data",
                    Errors = new Dictionary<string, string> { { "ProductId", "Product Not Found" } }
                };
                return BadRequest(response);
            }

            catch (InsufficientStockException)
            {
                response = new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "InValid Data",
                    Errors = new Dictionary<string, string> { { "Quantity", "Insufficient Stock" } }
                };
                return BadRequest(response);
            }

            catch (Exception e) when (e is NoSavedChangesException || e is DbException || e is DbUpdateException)
            {
                response = new BaseResponse
                {
                    StatusCode = 400,
                    Message = "Can’t Add Order",
                };
                return BadRequest(response);
            }
        }

        // PUT: api/orders/1
        [HttpPut("delete/{Id}")]
        public async Task<IActionResult> DeleteOrder(Guid Id)
        {
            BaseResponse response;
            try
            {
                await _orderService.SoftDeleteAsync(Id);
                response = new BaseResponse
                {
                    StatusCode = 200,
                    Message = "Order Deleted Successfully",
                };
                return Ok(response);
            }
            catch (OrderNotFoundException e)
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
                    Message = "Can’t Delete Order",
                };
                return BadRequest(response);
            }
        }
    }
}
