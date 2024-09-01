using BusinessDomain.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels
{
    public class OrderViewModel
    {
        // Output only
        public Guid Id { get; set; }
        public Double Amount { get; set; }
        public float Tax { get; set; }
        public Double TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        // Input & Output
        [Required]
        public Guid CustomerId { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
