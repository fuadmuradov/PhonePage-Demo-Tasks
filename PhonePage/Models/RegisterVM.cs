using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Models
{
    public class RegisterVM
    {
        [Required]
        [StringLength(maximumLength:30)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(maximumLength:35)]
        public string LastName { get; set; }
        [Required]
        [StringLength(maximumLength:30)]
        public string UserName{ get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
