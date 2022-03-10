using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Models
{
    public class TeacherHobby
    {
        public int id { get; set; }
        public int Teacherid { get; set; }
        public int Hobbyid { get; set; }
        public Teacher Teacher { get; set; }
        public Hobby Hobby { get; set; }
    }
}
