using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage="Please fill email this field")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }
        [Required(ErrorMessage ="fill Password Field")]
        [MinLength(6,ErrorMessage = "must be 8 symbol")]
        
        public string Password { get; set; }
    }
}
