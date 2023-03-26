using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Student_storproc.Data;
using Student_storproc.Models;
using System.Reflection.Metadata;

namespace Student_storproc.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var data = _db.Students.FromSqlRaw("exec spGetAllStudents").ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@Name", student.Name));
                parameter.Add(new SqlParameter("@Address", student.Address));
                parameter.Add(new SqlParameter("@RollNo", student.RollNo));
                parameter.Add(new SqlParameter("@Subject", student.Subject));

                _db.Database.ExecuteSqlRaw(@"exec spInsertStudent @Name, @Address, @RollNo, @Subject", parameter.ToArray());

                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Edit(int id)
        {
            var records = _db.Students.Find(id);
            return View(records);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@Id", student.Id));
                parameter.Add(new SqlParameter("@Name", student.Name));
                parameter.Add(new SqlParameter("@Address", student.Address));
                parameter.Add(new SqlParameter("@RollNo", student.RollNo));
                parameter.Add(new SqlParameter("@Subject", student.Subject));

                _db.Database.ExecuteSqlRaw(@"exec spupdateStudent @Id, @Name, @Address, @RollNo, @Subject", parameter.ToArray());
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            if(id== 0)
            {
                return NotFound();
            }
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Id", id));

            _db.Database.ExecuteSqlRaw(@"exec spdeleteStudent @Id",parameter.ToArray());
            return RedirectToAction("Index");
        }
    }
}
