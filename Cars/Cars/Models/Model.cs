using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cars.Models
{
    public class Model
    {
        public virtual int ModelID { get; set; }
        public virtual int BrandID { get; set; }
        public virtual string ModelName { get; set; }
        public virtual int Price { get; set; }
        public virtual int Year { get; set; }
        public virtual string TypeEngine { get; set; }
        public virtual int Stock { get; set; }
        public virtual Brand Brand { get; set; }
    }
}