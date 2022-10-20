using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class VehicleManager : IVehicleService
    {
        IVehicleDal _vehicleDal;
       // IDeviceDal _deviceDal;

        public VehicleManager(IVehicleDal vehicleDal/*,IDeviceDal deviceDal*/)
        {
            _vehicleDal = vehicleDal;
           // _deviceDal = deviceDal;
        }

        [SecuredOperation("nodejs,admin")]
        public IDataResult<Vehicle> Add(Vehicle vehicle)
        {
            vehicle.Plate = vehicle.Plate.ToUpper().Trim().Replace(" ", "");
            var plateExists = _vehicleDal.Get(v => v.Plate == vehicle.Plate);
            if (plateExists == null)
            {
                var vehicleResult = _vehicleDal.Add(vehicle);

                return new SuccessDataResult<Vehicle>(vehicle, Messages.ProductAdded);
            }
            return new ErrorDataResult<Vehicle>(vehicle, "Plaka kaydı zaten mevcut!");
        }

        //[SecuredOperation("nodejs,admin")]
        //public IDataResult<PagedModel<List<VehicleGroupDto>>> GetAllByInfoQuery(VehicleInfoQueryDto vehicleInfoQuery)
        //{
        //    int pageNumber = (int)(vehicleInfoQuery?.Pagination?.PageNumber != null ? vehicleInfoQuery.Pagination.PageNumber : 1);
        //    int pageSize = (int)(vehicleInfoQuery?.Pagination?.PageSize != null ? vehicleInfoQuery.Pagination.PageSize : 10);
        //    pageNumber = pageNumber > 0 ? pageNumber : 1;
        //    pageSize = pageSize > 0 ? pageSize : 10;
        //    int start = (int)((pageNumber - 1) * pageSize);

        //    var vehicleGroupList = _vehicleDal.GetAllByInfoQuery();

        //    if (vehicleInfoQuery.TaxiType > 0)
        //    {
        //        vehicleGroupList = vehicleGroupList.Where(x => x.Vehicle.TaxiTypeId == vehicleInfoQuery.TaxiType).ToList();
        //    }

        //    if (string.IsNullOrWhiteSpace(vehicleInfoQuery.Plate) == false)
        //    {
        //        vehicleGroupList = vehicleGroupList.Where(x => x.Vehicle.Plate == vehicleInfoQuery.Plate).ToList();
        //    }

        //    if (vehicleInfoQuery.GroupId > 0)
        //    {
        //        if (vehicleInfoQuery.UserId == 0 && vehicleInfoQuery.FleetId == 0)
        //        {
        //            vehicleGroupList = vehicleGroupList.Where(x => x.VehicleGroups.GroupId == vehicleInfoQuery.GroupId).ToList();
        //        }
        //        else if (vehicleInfoQuery.UserId > 0 && vehicleInfoQuery.FleetId > 0)
        //        {
        //            vehicleGroupList = vehicleGroupList.Where(x => x.VehicleGroups.GroupId == vehicleInfoQuery.GroupId && x.VehicleGroups.UserId == vehicleInfoQuery.UserId && x.VehicleGroups.FleetId == vehicleInfoQuery.FleetId).ToList();
        //        }
        //        else if (vehicleInfoQuery.UserId > 0 && vehicleInfoQuery.FleetId == 0)
        //        {
        //            vehicleGroupList = vehicleGroupList.Where(x => x.VehicleGroups.GroupId == vehicleInfoQuery.GroupId && x.VehicleGroups.UserId == vehicleInfoQuery.UserId).ToList();
        //        }
        //        else if (vehicleInfoQuery.UserId == 0 && vehicleInfoQuery.FleetId > 0)
        //        {
        //            vehicleGroupList = vehicleGroupList.Where(x => x.VehicleGroups.GroupId == vehicleInfoQuery.GroupId && x.VehicleGroups.FleetId == vehicleInfoQuery.FleetId).ToList();
        //        }
        //    }
        //    else
        //    {
        //        if (vehicleInfoQuery.UserId > 0 || vehicleInfoQuery.FleetId > 0)
        //        {
        //            if (vehicleInfoQuery.UserId > 0 && vehicleInfoQuery.FleetId > 0)
        //            {
        //                vehicleGroupList = vehicleGroupList.Where(x => x.VehicleGroups.UserId == vehicleInfoQuery.UserId && x.VehicleGroups.FleetId == vehicleInfoQuery.FleetId).ToList();
        //            }
        //            else if (vehicleInfoQuery.UserId > 0 && vehicleInfoQuery.FleetId == 0)
        //            {
        //                vehicleGroupList = vehicleGroupList.Where(x => x.VehicleGroups.UserId == vehicleInfoQuery.UserId).ToList();
        //            }
        //            else if (vehicleInfoQuery.UserId == 0 && vehicleInfoQuery.FleetId > 0)
        //            {
        //                vehicleGroupList = vehicleGroupList.Where(x => x.VehicleGroups.FleetId == vehicleInfoQuery.FleetId).ToList();
        //            }
        //        }
        //    }

        //    int totalRecords = vehicleGroupList.Count;
        //    vehicleGroupList = vehicleGroupList.Skip(start).Take(pageSize).ToList();

        //    if (totalRecords == 0)
        //    {
        //        return new SuccessDataResult<PagedModel<List<VehicleGroupDto>>>("Empty List");
        //    }
        //    else if (vehicleGroupList.Count == 0)
        //    {
        //        return new ErrorDataResult<PagedModel<List<VehicleGroupDto>>>("Wrong Page Settings");
        //    }
        //    else
        //    {
        //        var pagedModel = new PagedModel<List<VehicleGroupDto>>(vehicleGroupList, totalRecords, pageNumber, pageSize);
        //        return new SuccessDataResult<PagedModel<List<VehicleGroupDto>>>(pagedModel, Messages.Success);
        //    }
        //}

        //[SecuredOperation("nodejs,admin")]
        //public IDataResult<List<VehicleCountDto>> GetVehiclesCount(VehicleGroupCountDto vehicleGroupCountDto)
        //{
        //    var vehicleGroupList = _vehicleDal.GetAllByInfoQuery();
        //    var vehicleList = new List<Vehicle>();

        //    if (vehicleGroupCountDto.GroupId > 0)
        //    {
        //        if (vehicleGroupCountDto.UserId == 0 && vehicleGroupCountDto.FleetId == 0)
        //        {
        //            vehicleList = vehicleGroupList.Where(x => x.VehicleGroups.GroupId == vehicleGroupCountDto.GroupId).Select(x => x.Vehicle).ToList();
        //        }
        //        else if (vehicleGroupCountDto.UserId > 0 && vehicleGroupCountDto.FleetId > 0)
        //        {
        //            vehicleList = vehicleGroupList.Where(x => x.VehicleGroups.GroupId == vehicleGroupCountDto.GroupId && x.VehicleGroups.UserId == vehicleGroupCountDto.UserId && x.VehicleGroups.FleetId == vehicleGroupCountDto.FleetId).Select(x => x.Vehicle).ToList();
        //        }
        //        else if (vehicleGroupCountDto.UserId > 0 && vehicleGroupCountDto.FleetId == 0)
        //        {
        //            vehicleList = vehicleGroupList.Where(x => x.VehicleGroups.GroupId == vehicleGroupCountDto.GroupId && x.VehicleGroups.UserId == vehicleGroupCountDto.UserId).Select(x => x.Vehicle).ToList();
        //        }
        //        else if (vehicleGroupCountDto.UserId == 0 && vehicleGroupCountDto.FleetId > 0)
        //        {
        //            vehicleList = vehicleGroupList.Where(x => x.VehicleGroups.GroupId == vehicleGroupCountDto.GroupId && x.VehicleGroups.FleetId == vehicleGroupCountDto.FleetId).Select(x => x.Vehicle).ToList();
        //        }
        //    }
        //    else
        //    {
        //        if (vehicleGroupCountDto.UserId > 0 || vehicleGroupCountDto.FleetId > 0)
        //        {
        //            if (vehicleGroupCountDto.UserId > 0 && vehicleGroupCountDto.FleetId > 0)
        //            {
        //                vehicleList = vehicleGroupList.Where(x => x.VehicleGroups.UserId == vehicleGroupCountDto.UserId && x.VehicleGroups.FleetId == vehicleGroupCountDto.FleetId).Select(x => x.Vehicle).ToList();
        //            }
        //            else if (vehicleGroupCountDto.UserId > 0 && vehicleGroupCountDto.FleetId == 0)
        //            {
        //                vehicleList = vehicleGroupList.Where(x => x.VehicleGroups.UserId == vehicleGroupCountDto.UserId).Select(x => x.Vehicle).ToList();
        //            }
        //            else if (vehicleGroupCountDto.UserId == 0 && vehicleGroupCountDto.FleetId > 0)
        //            {
        //                vehicleList = vehicleGroupList.Where(x => x.VehicleGroups.FleetId == vehicleGroupCountDto.FleetId).Select(x => x.Vehicle).ToList();
        //            }
        //        }
        //        else
        //        {
        //            vehicleList = vehicleGroupList.Select(x => x.Vehicle).ToList();
        //        }
        //    }

        //    var result = _vehicleDal.GetVehicleCount(vehicleList);

        //    int totalCount = 0;
        //    foreach (var item in result)
        //    {
        //        totalCount += item.VehicleCount;
        //    }

        //    var totalDto = new VehicleCountDto()
        //    {
        //        TaxiType = 0,
        //        VehicleCount = totalCount
        //    };

        //    result.Add(totalDto);
        //    result = result.OrderBy(x => x.TaxiType).ToList();

        //    return new SuccessDataResult<List<VehicleCountDto>>(result);
        //}

        [SecuredOperation("nodejs,admin")]
        public IDataResult<Vehicle> GetById(int id)
        {
            var vehicle = _vehicleDal.Get(p => p.Id == id);
            if (vehicle == null)
            {
                return new ErrorDataResult<Vehicle>("Girilen plakaya ait araç bulunamadı!");
            }
            return new SuccessDataResult<Vehicle>(vehicle);
        }

        [SecuredOperation("nodejs,admin")]
        public IResult Update(Vehicle vehicle)
        {
            var result = _vehicleDal.Get(x=>x.Id==vehicle.Id);
            var plates= _vehicleDal.Get(x=>x.Plate==vehicle.Plate);
            if (result != null)
            {
                if (plates==null)
                {
                    //if (string.IsNullOrEmpty(vehicle.ProvinceId.ToString()) == false)
                    //    result.ProvinceId = vehicle.ProvinceId;
                    //if (string.IsNullOrEmpty(vehicle.VehicleOwnerId.ToString()) == false)
                    //    result.VehicleOwnerId = vehicle.VehicleOwnerId;

                    if (string.IsNullOrEmpty(vehicle.TaxiTypeId.ToString()) == false)
                        result.TaxiTypeId = vehicle.TaxiTypeId;

                    if (string.IsNullOrWhiteSpace(vehicle.Plate) == false)
                        result.Plate = vehicle.Plate.Replace(" ", "");

                    if (string.IsNullOrWhiteSpace(vehicle.Brand) == false)
                        result.Brand = vehicle.Brand;

                    if (string.IsNullOrWhiteSpace(vehicle.Model) == false)
                        result.Model = vehicle.Model;

                    if (string.IsNullOrWhiteSpace(vehicle.ModelYear.ToString()) == false)
                        result.ModelYear = vehicle.ModelYear;

                    if (string.IsNullOrWhiteSpace(vehicle.EngineNo) == false)
                        result.EngineNo = vehicle.EngineNo;

                    if (string.IsNullOrWhiteSpace(vehicle.ChasisNo) == false)
                        result.ChasisNo = vehicle.ChasisNo;

                    //if (string.IsNullOrWhiteSpace(vehicle.LicenseNo) == false)
                    //    result.LicenseNo = vehicle.LicenseNo;

                    //if (string.IsNullOrWhiteSpace(vehicle.LicenseDate.ToString()) == false)
                    //    result.LicenseDate = vehicle.LicenseDate;

                    //if (string.IsNullOrWhiteSpace(vehicle.LicenseExpiryDate.ToString()) == false)
                    //    result.LicenseExpiryDate = vehicle.LicenseExpiryDate;

                    if (string.IsNullOrWhiteSpace(vehicle.TaximeterTypeId.ToString()) == false)
                        result.TaximeterTypeId = vehicle.TaximeterTypeId;

                    //if (string.IsNullOrWhiteSpace(vehicle.Segment) == false)
                    //    result.Segment = vehicle.Segment;

                    if (string.IsNullOrWhiteSpace(vehicle.AssemblerStatus.ToString()) == false)
                        result.AssemblerStatus = vehicle.AssemblerStatus;

                    //if (string.IsNullOrWhiteSpace(vehicle.DeviceVersion.ToString()) == false)
                    //    result.DeviceVersion = vehicle.DeviceVersion;

                    if (string.IsNullOrWhiteSpace(vehicle.DeviceVersionId.ToString()) == false)
                        result.DeviceVersionId = vehicle.DeviceVersionId;

                    _vehicleDal.Update(result);
                }
            }
            return new SuccessResult();
        }
        
        [SecuredOperation("nodejs,admin")]
        public IDataResult<List<Vehicle>> GetAll()
        {
            var vehicles = _vehicleDal.GetAll();
            return new SuccessDataResult<List<Vehicle>>(vehicles);
        }

        [SecuredOperation("nodejs,admin")]
        public IDataResult<Vehicle> GetByPlate(string plate)
        {
            plate = plate.Replace(" ", "");
            var vehicle = _vehicleDal.Get(v => v.Plate == plate);
            if (vehicle == null)
            {
                return new ErrorDataResult<Vehicle>("Girilen plakaya ait araç bulunamadı!");
            }
            return new SuccessDataResult<Vehicle>(vehicle);
        }

        [SecuredOperation("nodejs,admin")]
        public IDataResult<int> GetVehiclesCountTaxiType(VehicleTaxiTypeCountDto vehicleCountDto)
        {
            return new SuccessDataResult<int>(_vehicleDal.GetVehicleCountTaxiType(vehicleCountDto));
        }      
    }
}
