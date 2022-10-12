using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ServiceDetailPageData : IDto
    {
        public int Id { get; set; }
        public int ServiceStatusId { get; set; }
        public Driver Driver { get; set; }
        public Vehicle Vehicle { get; set; }
        public VehicleOwner VehicleOwner { get; set; }
        public string ServiceArea { get; set; }
        public List<ServiceProcessDetailDto> ServiceProcesses { get; set; }
        public List<InstalledMaterialDetailDto> InstalledMaterials { get; set; }
        public List<ServiceMediaDto> CreatedServiceMedias { get; set; }
        public List<ServiceMediaDto> InstallerServiceMedias { get; set; }
        public int CreatedUserId { get; set; }
        public string CreatedDescription { get; set; }
        public string CreatedTime { get; set; }
        public int InstallerUserId { get; set; }
        public string InstallerDescription { get; set; }
        public string InstallerTime { get; set; }
        public int ControllerUserId { get; set; }
        public string ControllerDescription { get; set; }
        public string ControllerTime { get; set; }
    }
}
