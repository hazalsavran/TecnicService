using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
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
    public class DriverManager : IDriverService
    {
        IDriverDal _driverDal;
        IDriverVehicleDal _driverVehicleDal;
        IVehicleService _vehicleService;

        public DriverManager(IDriverDal driverDal, IDriverVehicleDal driverVehicleDal, IVehicleService vehicleService)
        {
            _driverDal = driverDal;
            _driverVehicleDal = driverVehicleDal;
            _vehicleService = vehicleService;
        }

        [SecuredOperation("nodejs,admin")]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IDriverService.Get")]
        public IResult Add(Driver driver)
        {
            var driverExists = _driverDal.Get(d=> d.TCNo == driver.TCNo);
            if (driverExists != null)
            {
                return new ErrorDataResult<Driver>(driverExists, "Şoför zaten mevcut!");
            }
            driver.Created = DateTime.Now;
            _driverDal.Add(driver);
            return new SuccessResult();
        }

        [SecuredOperation("nodejs,admin")]
        [CacheAspect]
        public IDataResult<List<Driver>> GetAll()
        {
            var drivers = _driverDal.GetAll();
            return new SuccessDataResult<List<Driver>>(drivers);
        }

        public IDataResult<PagedModel<List<Driver>>> GetAllByQuery(DriverMiniDto driverMiniDto)
        {
            int pageNumber = (int)(driverMiniDto.Pagination.PageNumber != null ? driverMiniDto.Pagination.PageNumber : 1);
            int pageSize = (int)(driverMiniDto.Pagination.PageSize != null ? driverMiniDto.Pagination.PageSize : 10);
            pageNumber = pageNumber > 0 ? pageNumber : 1;
            pageSize = pageSize > 0 ? pageSize : 10;
            int start = (int)((pageNumber - 1) * pageSize);

            var driverList = _driverDal.GetAll();

            if (string.IsNullOrWhiteSpace(driverMiniDto.TCNo)==false)
            {
                driverList = driverList.Where(x => x.TCNo == driverMiniDto.TCNo).ToList();
            }
            if (string.IsNullOrWhiteSpace(driverMiniDto.Gsm)==false)
            {
                driverList = driverList.Where(x => x.Phone == driverMiniDto.Gsm).ToList();
            }
            if (string.IsNullOrWhiteSpace(driverMiniDto.Name)==false)
            {
                driverList = driverList.Where(x => x.Name == driverMiniDto.Name).ToList();
            }
            if (string.IsNullOrWhiteSpace(driverMiniDto.Surname)==false)
            {
                driverList = driverList.Where(x => x.Surname == driverMiniDto.Surname).ToList();
            }
                      
            int totalRecords = driverList.Count;
            driverList = driverList.Skip(start).Take(pageSize).ToList();

            if (totalRecords == 0)
            {
                return new SuccessDataResult<PagedModel<List<Driver>>>(Messages.EmptyList);
            }
            else if (driverList.Count == 0)
            {
                return new ErrorDataResult<PagedModel<List<Driver>>>(Messages.WrongPageSettings);
            }
            else
            {
                var pagedModel = new PagedModel<List<Driver>>(driverList, totalRecords, pageNumber, pageSize);
                return new SuccessDataResult<PagedModel<List<Driver>>>(pagedModel, Messages.Success);
            }
        }

        [SecuredOperation("nodejs,admin")]
        public IDataResult<Driver> GetById(int driverId)
        {
            var driver = _driverDal.Get(d => d.Id == driverId);
            if (driver == null)
            {
                return new ErrorDataResult<Driver>("Şoför bulunamadı!");
            }
            return new SuccessDataResult<Driver>(driver);
        }

        [SecuredOperation("nodejs,admin")]
        public IDataResult<Driver> GetByPhoneNumber(string phoneNumber)
        {
            var driver = _driverDal.Get(d => d.Phone == phoneNumber);
            if (driver == null)
            {
                return new ErrorDataResult<Driver>("Bu telefon numarasına ait şoför bulunamadı!");
            }
            return new SuccessDataResult<Driver>(driver);
        }

        [SecuredOperation("nodejs,admin")]
        public IDataResult<Driver> GetByTCNo(string tcNo)
        {
            var driver = _driverDal.Get(d => d.TCNo == tcNo);
            if (driver == null)
            {
                return new ErrorDataResult<Driver>("Bu T.C. kimlik numarasına ait şoför bulunamadı!");
            }
            return new SuccessDataResult<Driver>(driver);
        }

        [SecuredOperation("nodejs,admin")]
        public IDataResult<List<Driver>> GetDriversByPlate(string plate)
        {
            plate = plate.Replace(" ", "");
            var vehicleResult = _vehicleService.GetByPlate(plate);
            if (!vehicleResult.Success)
                return new ErrorDataResult<List<Driver>>(vehicleResult.Message);
            var drivervehicles = _driverVehicleDal.GetAll(d => d.VehicleId == vehicleResult.Data.Id);
            var drivers = new List<Driver>();
            foreach (var driverVehicle in drivervehicles)
            {
                var driver = _driverDal.Get(d => d.Id == driverVehicle.DriverId);
                if (driver == null)
                    return new ErrorDataResult<List<Driver>>("Eksik şoför bilgisi!");
                drivers.Add(driver);
            }
            return new SuccessDataResult<List<Driver>>(drivers, drivers.Count == 0 ? "Hiçbir şoför bulunamadı!": "Şoförler listelendi");
        }

        [SecuredOperation("nodejs,admin")]
        [TransactionScopeAspect]
        public IResult MatchWithPlate(int driverId, int vehicleId)
        {
            var driverResult = GetById(driverId);
            if (!driverResult.Success)
                return new ErrorResult(driverResult.Message);
            var vehicleResult = _vehicleService.GetById(vehicleId);
            if (!vehicleResult.Success)
                return new ErrorResult(vehicleResult.Message);
            var dvRes = _driverVehicleDal.Get(dv => dv.DriverId == driverId && dv.VehicleId == vehicleId);

            if (dvRes == null)
            {
                var driverVehicle = new DriverVehicle();
                driverVehicle.DriverId = driverId;
                driverVehicle.VehicleId = vehicleId;
                _driverVehicleDal.Add(driverVehicle);
                return new SuccessResult("Plaka ve şoför eşleştirildi!");
            }
            return new SuccessResult("Plaka ve şoför zaten eşleşmiş!");
        }

        [SecuredOperation("nodejs,admin")]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IDriverService.Get")]
        public IResult Remove(Driver driver)
        {
            _driverDal.Delete(driver);
            return new SuccessResult();
        }

        [SecuredOperation("nodejs,admin")]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IDriverService.Get")]
        public IResult Update(Driver driver)
        {
            var driverExists = _driverDal.Get(d => d.Id == driver.Id);
            if (driverExists != null)
            {
                _driverDal.Update(driver);
                return new SuccessResult();
            }
            return new ErrorDataResult<Driver>(driverExists, "Bilgileri kontrol ediniz");
        }
    }
}
