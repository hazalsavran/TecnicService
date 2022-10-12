using Core.Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public  class ServiceInfo : IEntity
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int StatusId { get; set; }
        public int CreatedUserId { get; set; }
        public string CreatedNote { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public int InstallerUserId { get; set; }
        public string InstallerNote { get; set; }
        public DateTime InstallerTime { get; set; }
        public int ControllerUserId { get; set; }
        public string FinishedNote { get; set; }
        public DateTime FinishTime { get; set; }
        public int PaymentTypeId { get; set; }
        public double Price { get; set; }
        public int InstallerServiceId { get; set; }
        public string CameraSerialNo { get; set; }
        public string Operator { get; set; }
        //public PaginationDto Pagination { get; set; }
    }
}
