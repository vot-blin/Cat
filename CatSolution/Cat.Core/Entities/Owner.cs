using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat.Core.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public string PassportIssuedBy { get; set; }
        public DateTime PassportIssueDate { get; set; }
        public ICollection<Cat> Cats { get; set; }
    }
}
