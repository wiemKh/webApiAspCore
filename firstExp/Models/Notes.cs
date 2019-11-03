using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace firstExp.Models {
    public class Notes {
        public int NotesId { get; set; }
         [Required]
        public string NoteValue { get; set; }
         [Required]

        public string Subject { get; set; }

        [ForeignKey ("StudentId")]
        public Student Student { get; set; }
        public int StudentId { get; set; }
    }
}