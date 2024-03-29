﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CompleteServiceDto : IDto
    {
        public int ServiceInfoId { get; set; }
        public int ControllerUserId { get; set; }
        public string Note { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal Price { get; set; }
    }
}
