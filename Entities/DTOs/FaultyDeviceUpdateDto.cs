using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class FaultyDeviceUpdateDto : IDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int MaterialId { get; set; }
        public string SeriNo { get; set; }
        public string Imei { get; set; }
        public bool FaultyStatus { get; set; }
    }
}
