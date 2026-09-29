using System.ComponentModel.DataAnnotations;

namespace Zoo.ViewModels.Animal
{
    public class AnimalEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name must be filled")]
        [StringLength(50, ErrorMessage = "Speicies cant be longer than 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Species must be filled")]
        [StringLength(100, ErrorMessage = "Speicies cant be longer than 100")]
        public string Species { get; set; }

        [Required(ErrorMessage = "Age must be filled")]
        [Range(1, 150, ErrorMessage = "Age is out of range")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Weight must be filled")]
        [Range(0.5, 10000.0, ErrorMessage = "Weight is out of range")]
        public double WeightKg { get; set; }

        [Required(ErrorMessage = "Please, choose a zoo.")]
        public int ZooId { get; set; }
    }
}
