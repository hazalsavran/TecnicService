using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Service : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegionId { get; set; }
    }
}
