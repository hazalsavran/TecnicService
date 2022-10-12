using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ServiceProcess : IEntity
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int ServiceInfoId { get; set; }
        public string Description { get; set; }
        public string SerialNo { get; set; }
        public int ProcessTypeId { get; set; }
        public bool Status { get; set; }
    }
}
