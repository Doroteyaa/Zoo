using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ZooCon.Entities
{
    public class AnimalCaretaker
    {
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int CaretakerId { get; set; }
        public Caretaker Caretaker { get; set; }
    }
}
