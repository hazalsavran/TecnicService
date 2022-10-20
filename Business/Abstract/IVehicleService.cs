using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IVehicleService
    {
        //IDataResult<PagedModel<List<VehicleGroupDto>>> GetAllByInfoQuery(VehicleInfoQueryDto vehicleInfoQuery);
        //IDataResult<List<VehicleCountDto>> GetVehiclesCount(VehicleGroupCountDto vehicleGroupCountDto);
        IDataResult<int> GetVehiclesCountTaxiType(VehicleTaxiTypeCountDto vehicleCountDto);
        IDataResult<List<Vehicle>> GetAll();
        IDataResult<Vehicle> GetById(int id);
        IDataResult<Vehicle> GetByPlate(string plate);
        IDataResult<Vehicle> Add(Vehicle vehicle);
        IResult Update(Vehicle vehicle);

    }
}
