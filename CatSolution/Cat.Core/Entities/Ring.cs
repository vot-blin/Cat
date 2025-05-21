using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat.Core.Entities
{
    public class Ring
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Location { get; set; }
        public string Timetable { get; set; }
        public ICollection<Cat> Cats { get; set; }
        public ICollection<RingBreed> RingBreeds { get; set; }
        public ICollection<ExpertRing> ExpertRings { get; set; }
    }
}
