using Core.Entities;

namespace Entities.DTOs
{
    public class DeviceInfoQueryDto : IDto
    {
        
        public int? TaxiType { get; set; }
        public string? Plate { get; set; }
        public string? Imei { get; set; }
        public PaginationDto? Pagination { get; set; }
    }
}
