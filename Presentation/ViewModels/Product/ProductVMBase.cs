﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels
{
    public class ProductVMBase
    {
        // Input & Output
        [Required, MaxLength(50)]
        public String Name { get; set; }

        public String? Description { get; set; }

        [Required, Range(0.01, 99999999.99)]
        public double Amount { get; set; }

        public String Type { get; set; }
        public int? Quantity { get; set; }
        public String? Status { get; set; }
    }
}
