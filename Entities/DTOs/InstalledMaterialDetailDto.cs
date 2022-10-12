using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class InstalledMaterialDetailDto : IDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string SerialNo { get; set; }
        public string SerialNoOld { get; set; }
        public string ImeiNo { get; set; }
        public string ImeiNoOld { get; set; }
        public string Description { get; set; }
        public int ServiceInfoId { get; set; }
        public int OperatorId { get; set; }
        public DateTime Date { get; set; }
    }
}
