using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Double Amount { get; set; }
        public float Tax { get; set; }
        public Double TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public Guid CustomerId { get; set; }
        public User Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
