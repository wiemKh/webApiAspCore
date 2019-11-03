using Microsoft.EntityFrameworkCore;
namespace firstExp.Models {
    public class StudentContext : DbContext {
        public StudentContext (DbContextOptions options) : base (options) { Database.Migrate (); }

        public DbSet<Student> Students { get; set; }
        public DbSet<Notes> Notes { get; set; }

        protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);

// builder.Entity<Student> ().ToTable ("Students");
            // builder.Entity<Student> ().HasKey (p => p.StudentId);
            // builder.Entity<Student> ().Property (p => p.StudentId).IsRequired ().ValueGeneratedOnAdd ();
            // builder.Entity<Student> ().Property (p => p.FirstName).IsRequired ().HasMaxLength (30);
            // builder.Entity<Student> ().HasMany (p => p.Notes).WithOne (p => p.Student).HasForeignKey (p => p.StudentId);

            // builder.Entity<Notes> ().ToTable ("Notes");
            // builder.Entity<Notes> ().HasKey (p => p.NotesId);
            // builder.Entity<Notes>().Property(p => p.NotesId).IsRequired().ValueGeneratedOnAdd();
            // builder.Entity<Notes>().Property(p => p.NoteValue).IsRequired().HasMaxLength(50);
            // builder.Entity<Notes>().Property(p => p.Subject).IsRequired();            
        }
    }

}