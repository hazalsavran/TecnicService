using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfVehicleDal : EfEntityRepositoryBase<Vehicle, ITaksiContext>, IVehicleDal
    {
        public List<VehicleGroupDto> GetAllByInfoQuery()
        {
            using (ITaksiContext context = new ITaksiContext())
            {
                return (from vehicle in context.Vehicles
                        join groupVehicle in context.VehicleGroups
                        on vehicle.Id equals groupVehicle.VehicleId
                        where vehicle.Id == groupVehicle.VehicleId
                        select new VehicleGroupDto
                        {
                            Vehicle = vehicle,
                            VehicleGroups = groupVehicle
                        }).ToList();
            }
        }

        public List<VehicleCountDto> GetVehicleCount(List<Vehicle> vehicleList)
        {
            using (ITaksiContext context = new ITaksiContext())
            {
                return vehicleList.GroupBy(d => d.TaxiTypeId)
                        .Select(d => new VehicleCountDto
                        {
                            TaxiType = d.Key,
                            VehicleCount = d.Count(),
                        }).ToList();
            }
        }       

        public int GetVehicleCountTaxiType(VehicleTaxiTypeCountDto vehicleCountDto)
        {
            using (ITaksiContext context = new ITaksiContext())
            {
                if (vehicleCountDto != null)
                {
                    if (vehicleCountDto.TaxiType==1 || vehicleCountDto.TaxiType == 2 || vehicleCountDto.TaxiType == 3 || vehicleCountDto.TaxiType == 4)
                    {
                        return context.Vehicles.Count(x => x.TaxiTypeId == vehicleCountDto.TaxiType);
                    }
                    else if (vehicleCountDto.TaxiType==0)
                    {
                        return context.Vehicles.Count();
                    }
                    return 0;
                }               
                return 0;
            }
        }
    }
}
