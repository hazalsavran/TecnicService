﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class VehicleGroups : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }
        public int? FleetId { get; set; }
    }
}
