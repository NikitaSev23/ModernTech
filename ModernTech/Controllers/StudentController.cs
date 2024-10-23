using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModernTech.Models;
using ModernTech.Services;
using System.ComponentModel.DataAnnotations;

public class FilterContract
{
    public string? EnrollFrom {  get; set; }
    public string? EnrollTo {  get; set; }
    public int AgeFrom { get; set; }
    public int AgeTo { get; set; }
}

namespace ModernTech.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly DataContext _context;
        private readonly Class _service;
        public StudentController(DataContext context, Class service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        [Route("Students")]
        public IActionResult LoadStudents()
        {
            var Students = _context.Students;
            return Json(Students);
        }
        [HttpPost]
        [Route("Student")]
        public IActionResult AddStudent([FromBody] Student student)
        {
            try
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return Json(1);
            }
            catch (Exception ex) 
            {
                return Json(0);
            }
        }
        [HttpDelete]
        [Route("Student")]
        public IActionResult RemoveStudent([FromBody] List<int> Ids)
        {
            try
            {
               for(int i = 0; i < Ids.Count; i++)
               {
                    var DelStudent = _context.Students.FirstOrDefault(s => s.Id == Ids[i]);
                    _context.Students.Remove(DelStudent);
                    _context.SaveChanges();
               }
               return Json(1);
            }
            catch (Exception ex)
            {
                return Json(0);
            }
        }
        [HttpPost]
        [Route("FilterStudent")]
        public IActionResult LoadFilterStudents([FromBody] FilterContract filter)
        {
            var students = _context.Students.AsQueryable();

            if (DateOnly.TryParse(filter.EnrollFrom, out DateOnly DateFrom))
            {
                students = students.Where(s => s.EnrollementDate >= DateFrom);
            }

            if (DateOnly.TryParse(filter.EnrollTo, out DateOnly DateTo))
            {
                students = students.Where(s => s.EnrollementDate <= DateTo);
            }

            if (filter.AgeFrom != 0)
            {
                students = students.Where(s =>
                    (DateTime.Now.Year - s.BirthDate.ToDateTime(TimeOnly.MinValue).Year) -
                    (DateTime.Now.DayOfYear < s.BirthDate.ToDateTime(TimeOnly.MinValue).DayOfYear ? 1 : 0) >= filter.AgeFrom);
            }

            if (filter.AgeTo != 0)
            {
                students = students.Where(s =>
                    (DateTime.Now.Year - s.BirthDate.ToDateTime(TimeOnly.MinValue).Year) -
                    (DateTime.Now.DayOfYear < s.BirthDate.ToDateTime(TimeOnly.MinValue).DayOfYear ? 1 : 0) <= filter.AgeTo);
            }

            return Json(students.ToList());
        }

    }
}
