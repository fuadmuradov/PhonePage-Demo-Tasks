using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Models
{
    [NotMapped]
    public class UserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
     
    }
}
