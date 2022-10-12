using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ServiceRecordPageDto : IDto
    {
        //public List<Driver> Drivers { get; set; }
        public List<Service> Services { get; set; }
        public List<Material> Materials { get; set; }

    }
}
