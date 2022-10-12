using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class VehicleAddDto
    {
        public int ProvinceId { get; set; }
        public int TaxiTypeId { get; set; }
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ModelYear { get; set; }
        public string EngineNo { get; set; }
        public string ChasisNo { get; set; }
        public string LicenseNo { get; set; }
        public DateTime LicenseDate { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public int TaximeterTypeId { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int FleetId { get; set; }
        public string Segment { get; set; }
    }

}
