using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class VehicleGroupDto
    {
        public Vehicle Vehicle { get; set; }
        public VehicleGroups VehicleGroups { get; set; }
    }
}
