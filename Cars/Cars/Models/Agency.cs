using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cars.Models
{
    public class Agency
    {
        public virtual int ID { get; set; }
        public virtual string NameAgency { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
    }
}