using BusinessDomain.DTOs;
using Presentation.ViewModels.OrderItem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels.Order
{
    public class OrderVMResponse : OrderVMBase
    {
        // Output only
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public float Tax { get; set; }
        public double TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public List<OrderItemVMResponse> OrderItems { get; set; }
    }
}
