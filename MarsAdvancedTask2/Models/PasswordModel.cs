using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTask2.Models
{
    public class PasswordModel
    {
            public int Id { get; set; }
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
            public string ConfirmPassword { get; set; }
            public string PopUpMessage { get; set; }  // For success/error message verification
        }
}
