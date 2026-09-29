using System.ComponentModel.DataAnnotations;

namespace Zoo.ViewModels.Zoo
{
    public class ZooEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name must be filled")]
        [StringLength(50, ErrorMessage = "Name cant be longer than 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location must be filled")]
        [StringLength(100, ErrorMessage = "Location cant be longer than 100")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Establishment year must be filled")]
        [Range(1752, 2026, ErrorMessage = "Establishment year is OutOfMemoryException of range")]
        public int EstablishedYear { get; set; }

        [Required(ErrorMessage = "Email must be filled")]
        [EmailAddress(ErrorMessage = "Email must be valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number must be filled")]
        [Phone(ErrorMessage = "Phone number must be valid")]
        [RegularExpression(@"^\+?[0-9\s()\-]{10}$", ErrorMessage = "Phone number must be valid")]
        public string PhoneNumber { get; set; }
    }
}
