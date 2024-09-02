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
    public class OrderVMBase
    {
        // Input & Output
        [Required]
        public Guid CustomerId { get; set; }
    }
}
