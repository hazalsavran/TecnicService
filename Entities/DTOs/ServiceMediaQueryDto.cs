using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ServiceMediaQueryDto : IDto
    {
        public int ServiceStatus { get; set; }
        public int ServiceInfoId { get; set; }
    }
}
