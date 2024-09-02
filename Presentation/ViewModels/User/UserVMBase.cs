using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.User
{
    public class UserVmBase
    {
        // Input & Output
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required, MaxLength(14)]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        // status is either active or inactive
        [Required]
        public string Status { get; set; }
    }
}
