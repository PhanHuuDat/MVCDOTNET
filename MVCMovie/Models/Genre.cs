using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCMovie.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [StringLength(100, ErrorMessage ="Must from 1 to 100")]
        public string Name { get; set; }
        [Display(Name = "Created Date"), DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
    }
}
