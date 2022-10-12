using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ServiceProcessMiniDto : IDto
    {
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public int ProcessTypeId { get; set; }
    }
}
