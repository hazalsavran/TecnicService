using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ServiceInfooo : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceRecordId { get; set; }
        public int ServiceStatusId { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}