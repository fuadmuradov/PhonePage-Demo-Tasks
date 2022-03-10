using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Models
{
    public class Specification
    {
        public int id { get; set; }
        [Required]
        public string PhoneMarka { get; set; }
        [Required]
        public string PhoneModel { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
        [Required]
        public string Spesific1 { get; set; }
        [Required]
        public string Spesific2 { get; set; }
    }
}
