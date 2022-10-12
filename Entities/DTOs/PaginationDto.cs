using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class PaginationDto : IDto
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
