using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CompleteServicePageData : IDto
    {
        public int Id { get; set; }
        public DriverMiniDto Driver { get; set; }
        public VehicleMiniDto Vehicle { get; set; }
        public ServiceMiniDto Service { get; set; }
        public List<ServiceProcessDetailDto> ServiceProcesses { get; set; }
        public List<Region> Regions { get; set; }
        public string CreatedDescription { get; set; }
        public string InstallerDescription { get; set; }
        public List<ServiceMediaDto> CreatedServiceMedias { get; set; }
        public List<ServiceMediaDto> InstallerServiceMedias { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public int InstallerUserId { get; set; }
        public DateTime? InstallerTime { get; set; }
    }
}
