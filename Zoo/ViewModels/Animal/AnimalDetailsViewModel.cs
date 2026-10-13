using System.ComponentModel.DataAnnotations;

namespace Zoo.ViewModels.Animal
{
    public class AnimalDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Species { get; set; }

        public int Age { get; set; }

        public double WeightKg { get; set; }
        public string ZooName { get; set; }
        public List<string> Caretakers { get; set; } = new List<string>();
    }
}
