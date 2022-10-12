using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class NdjsLogDto :IDto
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string AppId { get; set; }
        public string AppType { get; set; }
        public DateTime Created { get; set; }
        public string Messages { get; set; }
        public PaginationDto? Pagination { get; set; }

    }
}
