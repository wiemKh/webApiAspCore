
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using firstExp.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace firstExp.Controllers
{
   [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private StudentContext _studentContext;

        public StudentController(StudentContext context)
        {
            _studentContext = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            return _studentContext.Students.OrderBy(c => c.FirstName).Include(c => c.Notes).ToList();
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound("Student id must be higher than zero");
            }
           Student student = _studentContext.Students.Include(c => c.Notes)
                    .Single(c => c.StudentId == id);
       //     Student student = _studentContext.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Student student)
        {
            if (student == null)
            {
                return NotFound("Student data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _studentContext.Students.AddAsync(student);
            await _studentContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),new { id = student.StudentId },student);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id,[FromBody]Student student)
        {
            if (student == null)
            {
                return NotFound("Stduent data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Student existingStudent = _studentContext.Students.FirstOrDefault(s => s.StudentId == id);
            if (existingStudent == null)
            {
                return NotFound("Student does not exist in the database");
            }
            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.State = student.State;
            existingStudent.City = student.City;
            _studentContext.Attach(existingStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _studentContext.SaveChangesAsync();
            return Ok(existingStudent);
        }



 





        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("Id is not supplied");
            }
            Student student = _studentContext.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                return NotFound("No student found with particular id supplied");
            }
            _studentContext.Students.Remove(student);
            await _studentContext.SaveChangesAsync();
            return Ok("Student is deleted sucessfully.");
        }

        ~StudentController()
        {
            _studentContext.Dispose();
        }
    }
}