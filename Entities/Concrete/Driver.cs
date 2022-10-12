using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public  class Driver : IEntity
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Phone { get; set; }
        public String Mail { get; set; }
        public String TCNo { get; set; }
        //public double Rating { get; set; }
        public String Birthday { get; set; }
        //public String Latitude { get; set; }
        //public String Longitude { get; set; }
        //public bool Statu { get; set; }
        //public bool Pet { get; set; }
        public DateTime Created { get; set; }
    }
}
