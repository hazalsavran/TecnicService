using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public  class User : IEntity
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Phone { get; set; }
        public String Mail { get; set; }
        public String IdNo { get; set; }
        public String Birthday { get; set; }
        public bool Pet { get; set; }
        public bool Travel { get; set; }
        public DateTime Created { get; set; }
    }
}
