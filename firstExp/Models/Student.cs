using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace firstExp.Models
{
    public class Student
    {
        public Student()
        {
            Notes = new List<Notes>();
        }

         [Key]
         public int StudentId { get; set; }
         [Required]
        public string FirstName { get; set; }
         [Required]
        public string LastName { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public ICollection<Notes> Notes { get; set; }
              
    }
    
}