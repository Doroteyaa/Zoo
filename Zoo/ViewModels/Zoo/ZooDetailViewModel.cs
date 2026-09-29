using System.ComponentModel.DataAnnotations;

namespace Zoo.ViewModels.Zoo
{
    public class ZooDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int EstablishedYear { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Display(Name = "Established on")]
        public DateTime CreatedOn { get; set; }
    }
}
