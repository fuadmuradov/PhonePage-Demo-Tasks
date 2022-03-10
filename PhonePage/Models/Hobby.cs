using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Models
{
    public class Hobby
    {
        public int id { get; set; }
        public string Name { get; set; }
        public List<TeacherHobby> TeacherHobbies { get; set; }

    }
}
