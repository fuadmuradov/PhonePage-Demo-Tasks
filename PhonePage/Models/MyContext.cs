using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Models
{
    public class MyContext : IdentityDbContext<User>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<TeacherHobby> TeacherHobbies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserToProduct> UserToProducts { get; set; }
    }
}
