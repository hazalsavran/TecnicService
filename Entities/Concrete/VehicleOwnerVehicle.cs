using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class VehicleOwnerVehicle : IEntity
    {
        public int Id { get; set; }
        public int VehicleOwnerId { get; set; }
        public int VehicleId { get; set; }
    }
}
