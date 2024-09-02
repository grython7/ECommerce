using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels.OrderItem
{
    public class OrderItemVMResponse
    {
        // Output only
        public Guid Id { get; set; }
        public double Cost { get; set; }
    }
}
