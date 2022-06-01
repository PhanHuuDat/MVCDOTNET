using System.ComponentModel.DataAnnotations;

namespace EducationalManagement.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }
        [DisplayFormat(NullDisplayText = "No grade")]
        public Course Course { get; set; }
        public Student Student { get; set; }
        
    }
}
