using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Domain
{
    class Agency
    {
        public virtual int ID { get; set; }
        public virtual string NameAgency { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
    }
}
