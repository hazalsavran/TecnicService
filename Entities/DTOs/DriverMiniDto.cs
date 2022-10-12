using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class DriverMiniDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gsm { get; set; }
        public string TCNo { get; set; }
        public bool Statu { get; set; }
        public bool? Pet { get; set; }
        public PaginationDto Pagination { get; set; }

    }
}
