using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class VehicleMiniDto :IDto
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public int? TaxiType { get; set; }
    }
}
