using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Double Amount { get; set; }
        public float Tax { get; set; }
        public Double TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid CustomerId { get; set; }
        public bool IsDeleted { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
