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
        public string Imei { get; set; }
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int TaxiType { get; set; }
        public bool Active { get; set; }
        public bool Statu { get; set; }
        public DateTime Created { get; set; }
       
    }
}
