using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IVehicleOwnerService
	{
		IDataResult<List<VehicleOwner>> GetAll();
		IDataResult<VehicleOwner> GetById(int id);
		IDataResult<VehicleOwner> GetByTc(string tc);
		IDataResult<List<VehicleOwner>> GetVehicleOwnersByPlate(string plate);
		IResult MatchWithPlate(int vehicleOwnerId, int vehicleId);
		IResult Add(VehicleOwner vehicleOwner);
		IResult Update(VehicleOwner vehicleOwner);
		IResult Remove(VehicleOwner vehicleOwner);
	}
}
