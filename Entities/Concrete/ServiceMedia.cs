using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ServiceMedia : IEntity
    {
        public int Id { get; set; }
        public int ServiceInfoId { get; set; }
        public string MediaLink { get; set; }
        public int MediaType { get; set; }
        public int ServiceStatus { get; set; }

    }
}
