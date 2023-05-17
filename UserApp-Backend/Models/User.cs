using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserApp_Backend.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        public string? Phone { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string? Address { get; set; }
        [Required]
        public string PicturePath { get; set; }
    }
}
