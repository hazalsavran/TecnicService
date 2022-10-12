using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CompleteAssemblyDto : IDto
    {
        public int ServiceInfoId { get; set; }
        public string Note { get; set; }
        public int InstallerUserId { get; set; }
        public List<ServiceMediaDto> MediaLinks { get; set; }
        public List<ServiceProcessUpdateDto> ServiceProcessesUpdate { get; set; }
        public List<ServiceProcessNewDto> ServiceProcessesNew { get; set; }

    }
}
