using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ServiceCompletedDto : IDto
    {
        public int Id { get; set; }
        public DriverMiniDto Driver { get; set; }
        public VehicleMiniDto Vehicle { get; set; }
        public ServiceMiniDto Service { get; set; }
        public List<ServiceProcessUpdatedDto> ServiceProcesses { get; set; }
        public string Description { get; set; }
        public List<ServiceMediaDto> ServiceMedias { get; set; }
        public List<Region> Regions { get; set; }
        public int ControllerUserId { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal Price { get; set; }
        public DateTime? FinishTime { get; set; }

    }
}
