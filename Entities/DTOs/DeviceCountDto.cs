using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class DeviceCountDto : IDto
    {
        public int TaxiType { get; set; }
        public int DeviceCount { get; set; }
    }
}
