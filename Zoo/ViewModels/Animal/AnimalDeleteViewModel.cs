using System.ComponentModel.DataAnnotations;

namespace Zoo.ViewModels.Animal
{
    public class AnimalDeleteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
    }
}
