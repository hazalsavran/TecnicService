using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IVehicleDal : IEntityRepository<Vehicle>
    {
        List<VehicleGroupDto> GetAllByInfoQuery();
        public List<VehicleCountDto> GetVehicleCount(List<Vehicle> vehicleList);
        public int  GetVehicleCountTaxiType(VehicleTaxiTypeCountDto vehicleCountDto);

    }
}
