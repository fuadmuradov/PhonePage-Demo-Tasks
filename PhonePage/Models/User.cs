using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }


        [NotMapped]
        public List<string> RoleIds { get; set; }
        [NotMapped]
        public string RoleId { get; set; }
        [NotMapped]
        public string newRoleId { get; set; }
    }
}
