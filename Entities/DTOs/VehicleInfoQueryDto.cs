using Core.Entities;

namespace Entities.DTOs
{
    public class VehicleInfoQueryDto : IDto
    {
        public int? TaxiType { get; set; }
        public string? Plate { get; set; }
        public int GroupId { get; set; }
        public int FleetId { get; set; }
        public int UserId { get; set; }
        public string DeviceVersion { get; set; }
        public PaginationDto? Pagination { get; set; }
        
    }
}
