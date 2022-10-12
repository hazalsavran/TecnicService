using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class VehicleOwnerManager : IVehicleOwnerService
    {
        IVehicleOwnerDal _ownerDal;
        IVehicleService _vehicleService;
        IVehicleOwnerVehicleDal _vehicleOwnerVehicleDal;

        public VehicleOwnerManager(IVehicleOwnerDal serviceDal, IVehicleService vehicleService, IVehicleOwnerVehicleDal vehicleOwnerVehicleDal)
        {
            _ownerDal = serviceDal;
            _vehicleService = vehicleService;
            _vehicleOwnerVehicleDal = vehicleOwnerVehicleDal;   
        }

        public IResult Add(VehicleOwner vehicleOwner)
        {
            _ownerDal.Add(vehicleOwner);
            return new SuccessResult();
        }

        public IDataResult<List<VehicleOwner>> GetAll()
        {
            var result = _ownerDal.GetAll();
            return new SuccessDataResult<List<VehicleOwner>>(result);
        }

        public IDataResult<VehicleOwner> GetById(int id)
        {
            var result = _ownerDal.Get(s => s.Id == id);
            return new SuccessDataResult<VehicleOwner>(result);
        }

        public IDataResult<VehicleOwner> GetByTc(string tc)
        {
            var result = _ownerDal.Get(s => s.TCNo == tc);
            if (result == null)
            {
                return new ErrorDataResult<VehicleOwner>("Bu T.C. kimlik numarasına ait araç sahibi bulunamadı!");
            }
            return new SuccessDataResult<VehicleOwner>(result);
        }

        public IDataResult<List<VehicleOwner>> GetVehicleOwnersByPlate(string plate)
        {
            plate = plate.Replace(" ", "");
            var vehicleResult = _vehicleService.GetByPlate(plate);
            if (!vehicleResult.Success)
                return new ErrorDataResult<List<VehicleOwner>>(vehicleResult.Message);
            var vehicleOwnerVehicles = _vehicleOwnerVehicleDal.GetAll(d => d.VehicleId == vehicleResult.Data.Id);
            var vehicleOwners = new List<VehicleOwner>();
            foreach (var vehicleOwnerVehicle in vehicleOwnerVehicles)
            {
                var vehicleOwner = _ownerDal.Get(d => d.Id == vehicleOwnerVehicle.VehicleOwnerId);
                if (vehicleOwner == null)
                    return new ErrorDataResult<List<VehicleOwner>>("Eksik araç sahibi bilgisi!");
                vehicleOwners.Add(vehicleOwner);
            }
            return new SuccessDataResult<List<VehicleOwner>>(vehicleOwners, vehicleOwners.Count == 0 ? "Hiçbir araç sahibi bulunamadı!" : "Araç sahipleri listelendi");
        }

        [SecuredOperation("nodejs,admin")]
        [TransactionScopeAspect]
        public IResult MatchWithPlate(int vehicleOwnerId, int vehicleId)
        {
            var driverResult = GetById(vehicleOwnerId);
            if (!driverResult.Success)
                return new ErrorResult(driverResult.Message);
            var vehicleResult = _vehicleService.GetById(vehicleId);
            if (!vehicleResult.Success)
                return new ErrorResult(vehicleResult.Message);
            var vovRes = _vehicleOwnerVehicleDal.Get(vov => vov.VehicleOwnerId == vehicleOwnerId && vov.VehicleId == vehicleId);
            if (vovRes == null)
            {
                var driverVehicle = new VehicleOwnerVehicle();
                driverVehicle.VehicleOwnerId = vehicleOwnerId;
                driverVehicle.VehicleId = vehicleId;
                _vehicleOwnerVehicleDal.Add(driverVehicle);
                return new SuccessResult("Plaka ve araç sahibi eşleştirildi!");
            }
            return new SuccessResult("Plaka ve araç sahibi zaten eşleşmiş!");

        }

        public IResult Remove(VehicleOwner vehicleOwner)
        {
            _ownerDal.Delete(vehicleOwner);
            return new SuccessResult();
        }

        public IResult Update(VehicleOwner vehicleOwner)
        {
            _ownerDal.Update(vehicleOwner);
            return new SuccessResult();
        }
    }
}
