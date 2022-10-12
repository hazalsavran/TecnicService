using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Device : IEntity
    {
        public int Id { get; set; }
        public String Imei { get; set; }
        public String Plate { get; set; }
        public String Brand { get; set; }
        public String Model { get; set; }
        public int TaxiType { get; set; }
        public bool Active { get; set; }
        public bool Statu { get; set; }
        public DateTime Created { get; set; }
       
    }
}
