using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhonePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly MyContext db;

        public HomeController(MyContext db)
        {
            this.db = db;
        }

        public IActionResult Dashboard()
        {
           List<Teacher> teacher = db.Teachers.Include(f=>f.TeacherHobbies).ThenInclude(h=>h.Hobby).ToList();
            return View(teacher);
        }

        public IActionResult Create()
        {
            ViewBag.Hobbies = db.Hobbies.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher)
        {
            ViewBag.Hobbies = db.Hobbies.ToList();
            if (!ModelState.IsValid) return View();


            teacher.TeacherHobbies = new List<TeacherHobby>();
            foreach (var id in teacher.HobbyIds)
            {
                TeacherHobby thobby = new TeacherHobby
                {
                    Teacher = teacher,
                    Hobbyid = id
                };
                teacher.TeacherHobbies.Add(thobby);
            }

            db.Teachers.Add(teacher);
            db.SaveChanges();


            return View();
        }





        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Hobbies = db.Hobbies.ToList();
            ViewBag.TeacherHobbies = db.TeacherHobbies.Where(th => th.Teacherid == id).ToList();

            Teacher teacher = db.Teachers.Include(f=>f.TeacherHobbies).ThenInclude(h=>h.Hobby).FirstOrDefault(f=>f.id==id);
            return View(teacher);
        }

        [HttpPost]
        public IActionResult Edit(Teacher teacher)
        {

            ViewBag.Hobbies = db.Hobbies.ToList();
            if (!ModelState.IsValid) return View();
            List<TeacherHobby> teacherHobbies = db.TeacherHobbies.Where(t => t.Teacherid == teacher.id).ToList();
            db.TeacherHobbies.RemoveRange(teacherHobbies);
            teacher.TeacherHobbies = new List<TeacherHobby>();
            foreach (var id in teacher.HobbyIds)
            {
                TeacherHobby thobby = new TeacherHobby
                {
                    Teacherid = teacher.id,
                    Hobbyid = id
                };
                db.TeacherHobbies.Add(thobby);
            }
            db.SaveChanges();

            //Teacher teacher1 = db.Teachers.


            return Redirect("/Admin/Home/Dashboard");
        }

    }
}
