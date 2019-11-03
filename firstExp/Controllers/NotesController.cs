using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using firstExp.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace firstExp.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class NotesController : Controller {
        private StudentContext _studentContext;
        public NotesController (StudentContext context) {
            _studentContext = context;
        }

        [HttpGet ("{studentId}/notes")]
        public IActionResult GetNotes (int studentId) {
            List<Notes> Notes = _studentContext.Notes.Where (p => p.StudentId == studentId).ToList ();
            if (Notes == null) {
                return NotFound ("Student not found");
            }
            return Ok (Notes);

        }

        [HttpPost ("{studentId}/note")]
        public async Task<ActionResult> CreateNote (int studentId, [FromBody] Notes note) {
            if (note == null) {
                return NotFound ("Student data is not supplied");
            }
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            note.StudentId = studentId;

            await _studentContext.Notes.AddAsync (note);
            await _studentContext.SaveChangesAsync ();
            return Ok (note);

        }

        //  [HttpPost ("{studentId}/notes")]
        // public async Task<ActionResult> CreateListNote (int studentId, [FromBody] List<Notes> notes) {
        //     if (notes == null) {
        //         return NotFound ("Student data is not supplied");
        //     }
        //     if (!ModelState.IsValid) {
        //         return BadRequest (ModelState);
        //     }
        //     await _studentContext.Notes.AddRangeAsync (notes);
        //     await _studentContext.SaveChangesAsync ();
        //     return Ok (notes);

        // }

        [HttpPut ("{studentId}/note/{id}")]
        public async Task<ActionResult> UpdateNote (int studentId, int id, [FromBody] Notes note) {

            if (note == null) {
                return NotFound ("Stduent data is not supplied");
            }
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            Notes existingNote = _studentContext.Notes.FirstOrDefault (p => p.NotesId == id && p.StudentId == studentId);
            if (existingNote == null) {
                return NotFound ("Student does not exist in the database");
            }
            existingNote.NoteValue = note.NoteValue;
            existingNote.Subject = note.Subject;
                        existingNote.StudentId = studentId;

           
            _studentContext.Attach (existingNote).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _studentContext.SaveChangesAsync ();
            return Ok (existingNote);

        }

        [HttpDelete ("{studentId}/notes/{id}")]

        public async Task<ActionResult> Delete (int studentId, int? id) {
            if (id == null) {
                return NotFound ("Id is not supplied");
            }
            Notes notes = _studentContext.Notes.FirstOrDefault (p => p.NotesId == id && p.StudentId == studentId);
            if (notes == null) {
                return NotFound ("No student found with particular id supplied");
            }
            _studentContext.Notes.Remove (notes);
            await _studentContext.SaveChangesAsync ();
            return Ok ("Student is deleted sucessfully.");
        }

    }

}