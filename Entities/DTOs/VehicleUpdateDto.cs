using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class VehicleUpdateDto : IDto
    {
        public int Id { get; set; }
        public int TaxiTypeId { get; set; }
        public string Segment { get; set; }


    }
}
