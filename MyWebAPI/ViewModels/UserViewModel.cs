using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class UserViewModel
    {
        // Output only
        public Guid Id { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        // Input & Output
        [Required, MaxLength(50)]
        public String Name { get; set; }
        [Required]
        public String Address { get; set; }
        [Required, MaxLength(14)]
        public String Phone { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
        // status is either active or inactive
        [Required]
        public String Status { get; set; }
    }
}
