using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cars.Models
{
    public class Brand
    {
        public virtual int BrandID { get; set; }
        public virtual int AgencyID { get; set; }
        public virtual string BrandName { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual ICollection<Model> Models { get; set; }
    }
}