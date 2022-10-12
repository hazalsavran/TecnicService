using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class VehicleGroupCountDto
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int FleetId { get; set; }
    }
}
