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
	public interface IDriverService
	{
		IDataResult<PagedModel<List<Driver>>> GetAllByQuery(DriverMiniDto driverMiniDto);
		IDataResult<List<Driver>> GetDriversByPlate(string plate);
		IDataResult<Driver> GetById(int driverId);
		IDataResult<Driver> GetByTCNo(string tcNo);
		IDataResult<Driver> GetByPhoneNumber(string phoneNumber);
		IResult MatchWithPlate(int driverId, int vehicleId);
		IResult Add(Driver driver);
		IResult Update(Driver driver);
		IResult Remove(Driver driver);
	}
}
