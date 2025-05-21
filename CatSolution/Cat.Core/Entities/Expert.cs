using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat.Core.Entities
{
    public class Expert
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specializations { get; set; }
        public bool IsActive { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
        public ICollection<ExpertRing> ExpertRings { get; set; }
    }
}
