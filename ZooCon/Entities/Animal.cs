using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCon.Entities
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Species { get; set; }
        [Required]
        [Range(1, 150)]
        public int Age { get; set; }
        [Required]
        [Range(0.5, 10000.0)]
        public double WeightKg { get; set; }
        public bool IsDeleted { get; set; }

        public int ZooId { get; set; }
        [ForeignKey(nameof(ZooId))]
        public Zoo Zoo { get; set; }
        public ICollection<AnimalCaretaker> AnimalCaretakers { get; set; } = new List<AnimalCaretaker>();

    }
}
