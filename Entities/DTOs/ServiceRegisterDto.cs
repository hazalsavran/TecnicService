using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ServiceRegisterDto : IDto
    {
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int ServiceId { get; set; }
        public string Note { get; set; }
        public int CreatedUserId { get; set; }
        public List<ServiceMediaDto> MediaLinks { get; set; }
        public List<ServiceProcessDto> ServiceProcesses { get; set; }

    }
}
