using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class PendingAssemblyDto : IDto
    {
        public int Id { get; set; }
        public DriverMiniDto Driver { get; set; }
        public VehicleMiniDto Vehicle { get; set; }
        public ServiceMiniDto Service { get; set; }
        public List<ServiceProcessMiniDto> ServiceProcesses { get; set; }
        public Region Region { get; set; }
        public string Description { get; set; }
        public List<ServiceMediaDto> ServiceMedias { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
