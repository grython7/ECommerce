using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.User
{
    public class UserVMRequest : UserVmBase
    {
        [Required]
        public string Password { get; set; }
    }
}
