using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double Amount { get; set; }
        public String Type { get; set; }
        public int? Quantity { get; set; }
        public String? Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
