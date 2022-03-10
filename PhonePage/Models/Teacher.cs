using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Models
{
    public class Teacher
    {
        public int id { get; set; }
        public string Name { get; set; }
        public List<TeacherHobby> TeacherHobbies { get; set; }

        [NotMapped]
        public List<int> HobbyIds { get; set; }
    }
}
