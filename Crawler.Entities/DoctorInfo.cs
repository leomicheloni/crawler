using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Entities
{
    public class DoctorInfo
    {
        public string Name { get; set; }
        public string PracticeName { get; set; }
        public IEnumerable<string> Specialities { get; set; }
        public string Address { get; set; }
        public string Rate { get; set; }
        public string Image { get; set; }

        public override string ToString()
        {
            var specialities = String.Join(", ", this.Specialities);
            return this.Name + " " + this.PracticeName + " " + this.Address + " " + this.Image + " " + specialities;
        }
   }
}
