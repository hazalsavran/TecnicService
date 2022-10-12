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
	public interface IInstalledMaterialService
	{
		IDataResult<List<InstalledMaterial>> GetAll();
		IDataResult<List<InstalledMaterial>> GetAllByVehicleId(int id);
		IDataResult<List<InstalledMaterialDetailDto>> GetAllWithDetailByVehicleId(int vehicleId);
		IDataResult<InstalledMaterial> GetById(int id);
		IDataResult<InstalledMaterial> GetMaterialIfExistsInCar(int vehicleId, int materialId);
		IResult Add(InstalledMaterial installedMaterial);
		IResult Update(InstalledMaterial installedMaterial);
		IResult Remove(InstalledMaterial installedMaterial);
	}
}
