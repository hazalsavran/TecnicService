using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class InstalledMaterialManager : IInstalledMaterialService
    {
        IInstalledMaterialDal _installedMaterialDal;

        public InstalledMaterialManager(IInstalledMaterialDal installedMaterialDal)
        {
            _installedMaterialDal = installedMaterialDal;
        }

        public IResult Add(InstalledMaterial installedMaterial)
        {
           _installedMaterialDal.Add(installedMaterial);
            return new SuccessResult();
        }

        public IDataResult<List<InstalledMaterial>> GetAll()
        {
            var materials = _installedMaterialDal.GetAll();
            return new SuccessDataResult<List<InstalledMaterial>>(materials);
        }

        public IDataResult<List<InstalledMaterial>> GetAllByVehicleId(int id)
        {
            var materials = _installedMaterialDal.GetAll(m => m.VehicleId == id);
            return new SuccessDataResult<List<InstalledMaterial>>(materials);
        }

        public IDataResult<List<InstalledMaterialDetailDto>> GetAllWithDetailByVehicleId(int vehicleId)
        {
            var materials = _installedMaterialDal.GetAllWithDetailByVehicleId(vehicleId);
            return new SuccessDataResult<List<InstalledMaterialDetailDto>>(materials);
        }

        public IDataResult<InstalledMaterial> GetById(int id)
        {
            var material = _installedMaterialDal.Get(i => i.Id == id);
            return new SuccessDataResult<InstalledMaterial>(material);
        }

        public IDataResult<InstalledMaterial> GetMaterialIfExistsInCar(int vehicleId, int materialId)
        {
            var material = _installedMaterialDal.Get(i => i.VehicleId == vehicleId && i.MaterialId == materialId);
            return new SuccessDataResult<InstalledMaterial>(material);
        }

        public IResult Remove(InstalledMaterial installedMaterial)
        {
            _installedMaterialDal.Delete(installedMaterial);
            return new SuccessResult();
        }

        public IResult Update(InstalledMaterial installedMaterial)
        {
            _installedMaterialDal.Update(installedMaterial);
            return new SuccessResult();
        }
    }
}
