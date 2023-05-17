using System.ComponentModel.DataAnnotations;

namespace UserApp_Backend.Models.Dtos
{
    public class UserInputDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
        public string? Address { get; set; }
        [Required]
        public IFormFile Picture { get; set; }
        public string? PicturePath { get; set; }
    }
}
