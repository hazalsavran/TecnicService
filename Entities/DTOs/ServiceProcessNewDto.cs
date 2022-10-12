using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ServiceProcessNewDto : IDto
    {
        public int MaterialId { get; set; }
        public string Description { get; set; }
        public string SerialNo { get; set; }
        public string SerialNoOld { get; set; }
        public string ImeiNo { get; set; }
        public string ImeiNoOld { get; set; }
        public bool Status { get; set; }
    }
}
