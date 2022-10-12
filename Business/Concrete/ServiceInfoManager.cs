using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Entities;
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
    public class ServiceInfoManager : IServiceInfoService
    {
        IServiceInfoDal _serviceInfoDal;
        IServiceMediaService _mediaService;
        IInstalledMaterialService _installedMaterialService;
        IDriverService _driverService;
        IServiceService _serviceService;
        IVehicleService _vehicleService;
        IServiceProcessService _serviceProcessService;
        IMaterialService _materialService;
        IRegionService _regionService;
        IVehicleOwnerService _vehicleOwnerService;

        public ServiceInfoManager(IServiceInfoDal serviceInfoDal,
            IServiceMediaService mediaService,
            IInstalledMaterialService installedMaterialService,
            IDriverService driverService,
            IServiceService serviceService,
            IVehicleService vehicleService,
            IServiceProcessService serviceProcessService,
            IMaterialService materialService,
            IRegionService regionService,
            IVehicleOwnerService vehicleOwnerService)
        {
            _serviceInfoDal = serviceInfoDal;
            _mediaService = mediaService;
            _installedMaterialService = installedMaterialService;
            _driverService = driverService;
            _serviceService = serviceService;
            _vehicleService = vehicleService;
            _serviceProcessService = serviceProcessService;
            _materialService = materialService;
            _regionService = regionService;
            _vehicleOwnerService = vehicleOwnerService;
        }

        [SecuredOperation("nodejs,admin")]
        [CacheRemoveAspect("IServiceInfoService.Get")]
        public IDataResult<ServiceInfo> Add(AddServiceinfoDto serviceInfo)
        {
            var servis = new ServiceInfo
            {
                VehicleId = serviceInfo.VehicleId,
                DriverId= serviceInfo.DriverId,
                StatusId = serviceInfo.StatusId,
                CreatedNote= serviceInfo.CreatedNote,
                CreatedUserId = serviceInfo.CreatedUserId,
                InstallerUserId = serviceInfo.InstallerUserId,
                InstallerTime = serviceInfo.InstallerTime,
                ControllerUserId = serviceInfo.CreatedUserId,
                FinishedNote = serviceInfo.FinishedNote,
                FinishTime= serviceInfo.FinishTime,
                PaymentTypeId= serviceInfo.PaymentTypeId,
                Price= serviceInfo.Price,
                InstallerServiceId= serviceInfo.InstallerServiceId,
                CameraSerialNo= serviceInfo.CameraSerialNo,
                Operator= serviceInfo.Operator,

            };
            _serviceInfoDal.Add(servis);
            return new SuccessDataResult<ServiceInfo>(servis,"Successfully Added");
        }

        //[SecuredOperation("nodejs,admin")]
        public IDataResult<List<ServiceInfo>> GetAllByServiceStatus(int serviceStatus)
        {
            var serviceInfos = _serviceInfoDal.GetAll(s => s.StatusId == serviceStatus);
            return new SuccessDataResult<List<ServiceInfo>>(serviceInfos,message:"Servis bilgileri başarıyla listelendi.");
        }

        //[SecuredOperation("nodejs,admin")]
        public IDataResult<ServiceInfo> GetById(int id)
        {
            var serviceInfo = _serviceInfoDal.Get(s => s.Id == id);
            return serviceInfo == null
                ? new ErrorDataResult<ServiceInfo>("Servis kaydı bulunamadı!")
                : new SuccessDataResult<ServiceInfo>(serviceInfo);
        }

        //[SecuredOperation("nodejs,admin")]
        //[CacheRemoveAspect("IServiceInfoService.Get")]
        [TransactionScopeAspect]
        public IResult Remove(int serviceInfoId)
        {
            var serviceInfo = _serviceInfoDal.Get(si => si.Id == serviceInfoId);
            if (serviceInfo == null)
                return new ErrorResult("Servis kaydı bulunamadı");
            _serviceInfoDal.Delete(serviceInfo);
            return new SuccessResult("Successfully Deleted");
        }

        //[SecuredOperation("nodejs,admin")]
        //[CacheRemoveAspect("IServiceInfoService.Get")]
        [TransactionScopeAspect]
        public IResult Update(ServiceInfo serviceInfo)
        {
            var controlData = _serviceInfoDal.Get(s => s.Id == serviceInfo.Id);
            if (controlData == null)
            {
                return new ErrorResult("Service info not found!");
            }
            _serviceInfoDal.Update(serviceInfo);
            return new SuccessResult("Successfully Updated");
        }

        //[SecuredOperation("nodejs,admin")]
        //[ValidationAspect(typeof(ProductValidator))]
        public IDataResult<ServiceRecordPageDto> GetServiceRecordPageData()
        {
            ServiceRecordPageDto serviceRecordPageDto = new ServiceRecordPageDto();
            //Swagger
            //var driversResult = _driverService.GetDriversByPlate(plate);
            //if (!driversResult.Success)
            //{
            //    return new ErrorDataResult<ServiceRecordPageDto>();
            //}
            //serviceRecordPageDto.Drivers = driversResult.Data;

            var servicesResult = _serviceService.GetAll();
            if (!servicesResult.Success)
            {
                return new ErrorDataResult<ServiceRecordPageDto>();
            }
            serviceRecordPageDto.Services = servicesResult.Data;


            var materialsResult = _materialService.GetAll();
            if (!materialsResult.Success)
            {
                return new ErrorDataResult<ServiceRecordPageDto>();
            }
            serviceRecordPageDto.Materials = materialsResult.Data;

            return new SuccessDataResult<ServiceRecordPageDto>(serviceRecordPageDto);
        }

        //[SecuredOperation("nodejs,admin")]
        //[CacheRemoveAspect("IServiceInfoService.Get")]
        public IDataResult<ServiceRegisterDto> CreateServiceRecord(ServiceRegisterDto serviceRegisterDto)
        {
            var serviceInfo = new ServiceInfo
            {
                StatusId = 1,
                CreatedNote = serviceRegisterDto.Note,
                CreatedUserId = serviceRegisterDto.CreatedUserId,
                InstallerServiceId = serviceRegisterDto.ServiceId,
                VehicleId = serviceRegisterDto.VehicleId,
                CreatedTime = DateTime.Now,
                DriverId = serviceRegisterDto.DriverId
            };
            try
            {
                var result = _serviceInfoDal.Add(serviceInfo);
            }
            catch (Exception)
            {

                return new ErrorDataResult<ServiceRegisterDto>(message:"Hata");
            }
           
         
            var serviceInfoId = serviceInfo.Id;
            var processesList = new List<ServiceProcess>();
            var process = new ServiceProcess();

            foreach (var processItem in serviceRegisterDto.ServiceProcesses)
            {
                process.Id = 0;
                process.ServiceInfoId = serviceInfoId;
                process.Status = false;
                process.Description = processItem.Description;
                process.MaterialId = processItem.MaterialId;
                try
                {
                    _serviceProcessService.Add(process);
                    processesList.Add(process);
                }
                catch (Exception)
                {
                    foreach (var item in processesList)
                    {
                        _serviceProcessService.Remove(item);
                    }
                    Remove(serviceInfo.Id);
                    throw;
                }

            }

            var mediasList = new List<ServiceMedia>();
            var serviceMedia = new ServiceMedia();
            foreach (var item in serviceRegisterDto.MediaLinks)
            {
                serviceMedia.Id = 0;
                serviceMedia.ServiceInfoId = serviceInfoId;
                serviceMedia.ServiceStatus = 1;
                serviceMedia.MediaLink = item.Link;
                serviceMedia.MediaType = 1;
                try
                {
                    _mediaService.Add(serviceMedia);
                    mediasList.Add(serviceMedia);
                }
                catch (Exception)
                {
                    foreach (var processItem in processesList)
                    {
                        _serviceProcessService.Remove(processItem);
                    }
                    foreach (var media in mediasList)
                    {
                        _mediaService.Remove(media);
                    }
                    Remove(serviceInfo.Id);
                    throw;
                }
            }
            return new SuccessDataResult<ServiceRegisterDto>(serviceRegisterDto,"Kayıt başarıyla oluşturuldu");
        }

        //[SecuredOperation("nodejs,admin")]
        //[CacheAspect]
        public IDataResult<PagedModel<List<PendingAssemblyDto>>> GetAllPendingAssemblyStatus(PaginationDto paginationDto)
        {
            var pendingAssemblies = _serviceInfoDal.GetAllPendingAssemblyStatus();

            int pageNumber = (int)(paginationDto.PageNumber != null ? paginationDto.PageNumber : 1);
            int pageSize = (int)(paginationDto.PageSize != null ? paginationDto.PageSize : 10);
            pageNumber = pageNumber > 0 ? pageNumber : 1;
            pageSize = pageSize > 0 ? pageSize : 10;
            int start = (int)((pageNumber - 1) * pageSize);

            int totalRecords = pendingAssemblies.Count;
            pendingAssemblies = pendingAssemblies.Skip(start).Take(pageSize).ToList();

            var pagedModel = new PagedModel<List<PendingAssemblyDto>>(pendingAssemblies.OrderBy(p => p.Id).ToList(), totalRecords, pageNumber, pageSize);
            return new SuccessDataResult<PagedModel<List<PendingAssemblyDto>>>(pagedModel);
        
            #region Old
            //var result = GetAllByServiceStatus(1);
            //if (!result.Success)
            //{
            //    return new ErrorDataResult<List<PendingAssemblyDto>>("Bekleyen servis kaydına ulaşılamadı! (Hata)");

            //}
            //var serviceInfos = result.Data;
            //var pendingAssemblies = new List<PendingAssemblyDto>();

            //foreach (var serviceInfo in serviceInfos)
            //{
            //    var pendingAssemblyResult = GetByIdPendingAssemblyStatus(serviceInfo.Id);
            //    if(pendingAssemblyResult.Success)
            //        pendingAssemblies.Add(pendingAssemblyResult.Data);
            //    else
            //    {
            //        return new ErrorDataResult<List<PendingAssemblyDto>>(pendingAssemblyResult.Message);
            //    }
            //}
            //pendingAssemblies = pendingAssemblies.OrderBy(p => p.Id).ToList();
            //return new SuccessDataResult<List<PendingAssemblyDto>>(pendingAssemblies);
            #endregion
        }

        //[SecuredOperation("nodejs,admin")]
        //[CacheAspect]
        public IDataResult<PendingAssemblyDto> GetByIdPendingAssemblyStatus(int serviceInfoId)
        {
            var pendingassembly = _serviceInfoDal.GetByIdPendingAssemblyStatus(serviceInfoId);
            if (pendingassembly == null)
            {
                return new ErrorDataResult<PendingAssemblyDto>(message:"Girilen Id'ye ait sorgu bulunamadı!");
            }
            return new SuccessDataResult<PendingAssemblyDto>(pendingassembly);
            #region Old
            //var result = GetById(serviceInfoId);
            //if (!result.Success)
            //{
            //    return new ErrorDataResult<PendingAssemblyDto>("Bekleyen servis kaydına ulaşılamadı! (Hata)");
            //}
            //var serviceInfo = result.Data;

            //var pendingAssembly = new PendingAssemblyDto();
            //pendingAssembly.Id = serviceInfo.Id;
            //pendingAssembly.Description = serviceInfo.CreatedNote;
            //pendingAssembly.CreatedTime = serviceInfo.CreatedTime;
            //pendingAssembly.CreatedUserId = serviceInfo.CreatedUserId;

            //var resulasd = _vehicleService.GetById(serviceInfo.VehicleId);
            //var vehicle = resulasd.Data;
            //pendingAssembly.Vehicle = new VehicleMiniDto
            //{
            //    Id = vehicle.Id,
            //    Plate = vehicle.Plate,
            //    TaxiType = vehicle.TaxiTypeId
            //};

            //var driver = _driverService.GetById(serviceInfo.DriverId).Data;
            //pendingAssembly.Driver = new DriverMiniDto
            //{
            //    Id = driver.Id,
            //    Gsm = driver.Phone,
            //    Name = driver.Name + " " + driver.Surname
            //};


            //var service = _serviceService.GetById(serviceInfo.InstallerServiceId).Data;
            //pendingAssembly.Service = new ServiceMiniDto
            //{
            //    Id = service.Id,
            //    Name = service.Name
            //};

            //var processes = _serviceProcessService.GetAllByServiceInfoId(serviceInfo.Id).Data;
            //var processesMini = new List<ServiceProcessMiniDto>();
            //foreach (var p in processes)
            //{
            //    processesMini.Add(new ServiceProcessMiniDto
            //    {
            //        Id = p.Id,
            //        ProcessName = _materialService.GetById(p.MaterialId).Data.Name,
            //        //ProcessTypeId = p.ProcessTypeId
            //    });
            //}
            //pendingAssembly.ServiceProcesses = processesMini;

            //var medias = _mediaService.GetAllByServiceMediaQuery(new ServiceMediaQueryDto
            //{
            //    ServiceInfoId = serviceInfo.Id,
            //    ServiceStatus = serviceInfo.StatusId
            //}).Data;
            //var miniMedias = new List<ServiceMediaDto>();
            //foreach (var m in medias)
            //{
            //    miniMedias.Add(new ServiceMediaDto
            //    {
            //        Link = m.MediaLink,
            //        Type = m.MediaType
            //    });
            //}

            //pendingAssembly.ServiceMedias = miniMedias;

            //var regionsResult = _regionService.GetById(service.RegionId);
            //if (!regionsResult.Success)
            //{
            //    return new ErrorDataResult<PendingAssemblyDto>("Bölgelere ulaşılamadı! (Hata)");
            //}
            //pendingAssembly.Region = regionsResult.Data;

            //return new SuccessDataResult<PendingAssemblyDto>(pendingAssembly);
            #endregion
        }

        //[SecuredOperation("nodejs,admin")]
        //[CacheAspect]
        public IDataResult<CompleteServicePageData> GetByIdCompletedAssemblyStatus(int serviceInfoId)
        {
            var result = GetById(serviceInfoId);
            if (!result.Success)
            {
                return new ErrorDataResult<CompleteServicePageData>("Tamamlanan servis kaydına ulaşılamadı! (Hata)");

            }
            var serviceInfo = result.Data;

            var pageData = new CompleteServicePageData();
            pageData.Id = serviceInfo.Id;
            pageData.CreatedDescription = serviceInfo.CreatedNote;
            pageData.CreatedTime = serviceInfo.CreatedTime;
            pageData.CreatedUserId = serviceInfo.CreatedUserId;
            pageData.InstallerDescription = serviceInfo.InstallerNote;
            pageData.InstallerUserId = serviceInfo.InstallerUserId;
            pageData.InstallerTime = serviceInfo.InstallerTime;

            var resulasd = _vehicleService.GetById(serviceInfo.VehicleId);
            var vehicle = resulasd.Data;
            pageData.Vehicle = new VehicleMiniDto
            {
                Id = vehicle.Id,
                Plate = vehicle.Plate,
                TaxiType = vehicle.TaxiTypeId
            };

            var driver = _driverService.GetById(serviceInfo.DriverId).Data;
            pageData.Driver = new DriverMiniDto
            {
                Id = driver.Id,
                Gsm = driver.Phone,
                Name = driver.Name + " " + driver.Surname
            };


            var service = _serviceService.GetById(serviceInfo.InstallerServiceId).Data;
            pageData.Service = new ServiceMiniDto
            {
                Id = service.Id,
                Name = service.Name
            };

            var processes = _serviceProcessService.GetAllByServiceInfoId(serviceInfo.Id).Data;
            var processesDetail = new List<ServiceProcessDetailDto>();
            foreach (var p in processes)
            {
                processesDetail.Add(new ServiceProcessDetailDto
                {
                    Id = p.Id,
                    ProcessName = _materialService.GetById(p.MaterialId).Data.Name,
                    Status = p.Status,
                    Description = p.Description,
                    SerialNo = p.SerialNo

                });
            }
            pageData.ServiceProcesses = processesDetail;

            var createdMedias = _mediaService.GetAllByServiceMediaQuery(new ServiceMediaQueryDto
            {
                ServiceInfoId = serviceInfo.Id,
                ServiceStatus = 1
            }).Data;
            var createdMiniMedias = new List<ServiceMediaDto>();
            foreach (var m in createdMedias)
            {
                createdMiniMedias.Add(new ServiceMediaDto
                {
                    Link = m.MediaLink,
                    Type = m.MediaType
                });
            }
            pageData.CreatedServiceMedias = createdMiniMedias;

            var installerMedias = _mediaService.GetAllByServiceMediaQuery(new ServiceMediaQueryDto
            {
                ServiceInfoId = serviceInfo.Id,
                ServiceStatus = 2
            }).Data;
            var installerMiniMedias = new List<ServiceMediaDto>();
            foreach (var m in installerMedias)
            {
                installerMiniMedias.Add(new ServiceMediaDto
                {
                    Link = m.MediaLink,
                    Type = m.MediaType
                });
            }
            pageData.InstallerServiceMedias = installerMiniMedias;

            var regionsResult = _regionService.GetAll();
            if (!regionsResult.Success)
            {
                return new ErrorDataResult<CompleteServicePageData>("Bölge kaydına ulaşılamadı! (Hata)");
            }
            pageData.Regions = regionsResult.Data;

            return new SuccessDataResult<CompleteServicePageData>(pageData,"Kayıt başarıyla getirildi.");
        }

        //[SecuredOperation("nodejs,admin")]
        //[CacheAspect]
        public IDataResult<ServiceDetailPageData> GetServiceDetailByServiceInfoId(int serviceInfoId)
        {
            var result = GetById(serviceInfoId);
            if (!result.Success)
            {
                return new ErrorDataResult<ServiceDetailPageData>("Servis kaydına ulaşılamadı! (Hata)");

            }
            var serviceInfo = result.Data;

            var pageData = new ServiceDetailPageData();
            pageData.Id = serviceInfo.Id;
            pageData.ServiceStatusId = serviceInfo.StatusId;
            pageData.CreatedDescription = serviceInfo.CreatedNote;
            pageData.CreatedTime = serviceInfo.CreatedTime.ToString("dd/MM/yyyy H:mm");
            pageData.CreatedUserId = serviceInfo.CreatedUserId;
            pageData.InstallerDescription = serviceInfo.InstallerNote;
            pageData.InstallerUserId = serviceInfo.InstallerUserId;
            pageData.InstallerTime = serviceInfo.InstallerTime.ToString("dd/MM/yyyy H:mm");
            pageData.ControllerUserId = serviceInfo.ControllerUserId;
            pageData.ControllerTime = serviceInfo.FinishTime.ToString("dd/MM/yyyy H:mm");
            pageData.ControllerDescription = serviceInfo.FinishedNote;
            var serviceasd = _serviceService.GetById(serviceInfo.InstallerServiceId).Data;
            pageData.ServiceArea = serviceasd.Name + _regionService.GetById(serviceasd.RegionId).Data.Name.ToString();

            var resulasd = _vehicleService.GetById(serviceInfo.VehicleId);
            var vehicle = resulasd.Data;
            pageData.Vehicle = vehicle;

            var driver = _driverService.GetById(serviceInfo.DriverId).Data;
            pageData.Driver = driver;


            var service = _serviceService.GetById(serviceInfo.InstallerServiceId).Data;
            pageData.ServiceArea = service.Name;

            var processes = _serviceProcessService.GetAllByServiceInfoId(serviceInfo.Id).Data;
            var processesDetail = new List<ServiceProcessDetailDto>();
            foreach (var p in processes)
            {
                processesDetail.Add(new ServiceProcessDetailDto
                {
                    Id = p.Id,
                    ProcessName = _materialService.GetById(p.MaterialId).Data.Name,
                    Status = p.Status,
                    Description = p.Description,
                    SerialNo = p.SerialNo

                });
            }
            pageData.ServiceProcesses = processesDetail;

            var installedMaterials = _installedMaterialService.GetAllWithDetailByVehicleId(serviceInfo.VehicleId).Data;

            pageData.InstalledMaterials = installedMaterials;

            var createdMedias = _mediaService.GetAllByServiceMediaQuery(new ServiceMediaQueryDto
            {
                ServiceInfoId = serviceInfo.Id,
                ServiceStatus = 1
            }).Data;
            var createdMiniMedias = new List<ServiceMediaDto>();
            foreach (var m in createdMedias)
            {
                createdMiniMedias.Add(new ServiceMediaDto
                {
                    Link = m.MediaLink,
                    Type = m.MediaType
                });
            }
            pageData.CreatedServiceMedias = createdMiniMedias;

            var installerMedias = _mediaService.GetAllByServiceMediaQuery(new ServiceMediaQueryDto
            {
                ServiceInfoId = serviceInfo.Id,
                ServiceStatus = 2
            }).Data;
            var installerMiniMedias = new List<ServiceMediaDto>();
            foreach (var m in installerMedias)
            {
                installerMiniMedias.Add(new ServiceMediaDto
                {
                    Link = m.MediaLink,
                    Type = m.MediaType
                });
            }
            pageData.InstallerServiceMedias = installerMiniMedias;

            return new SuccessDataResult<ServiceDetailPageData>(pageData);
        }

        //[SecuredOperation("nodejs,admin")]
        //[CacheAspect]

       
        public IDataResult<PagedModel<List<CompletedAssemblyDto>>> GetAllCompletedAssemblyStatus(PaginationDto paginationDto)
        {
            var result = GetAllByServiceStatus(2);

            if (!result.Success)
            {
                return new ErrorDataResult<PagedModel<List<CompletedAssemblyDto>>>("Tamamlanan servis kaydına ulaşılamadı! (Hata)");
            }
            var serviceInfos = result.Data;
            var completedAssemblies = new  List<CompletedAssemblyDto>();
            var completedAssembly11 = new CompletedAssemblyDto();
            foreach (var serviceInfo in serviceInfos)
            {
                var completedAssembly = new CompletedAssemblyDto();
                completedAssembly.Id = serviceInfo.Id;
                completedAssembly.Description = serviceInfo.InstallerNote;
                completedAssembly.InstallerTime = serviceInfo.InstallerTime;
                completedAssembly.InstallerUserId = serviceInfo.InstallerUserId;

                var resulasd = _vehicleService.GetById(serviceInfo.VehicleId);
                var vehicle = resulasd.Data;
                completedAssembly.Vehicle = new VehicleMiniDto
                {
                    Id = vehicle.Id,
                    Plate = vehicle.Plate,
                    TaxiType = vehicle.TaxiTypeId
                };

                var driver = _driverService.GetById(serviceInfo.DriverId).Data;
                completedAssembly.Driver = new DriverMiniDto
                {
                    Id = driver.Id,
                    Gsm = driver.Phone,
                    Name = driver.Name + " " + driver.Surname
                };


                var service = _serviceService.GetById(serviceInfo.InstallerServiceId).Data;
                completedAssembly.Service = new ServiceMiniDto
                {
                    Id = service.Id,
                    Name = service.Name
                };

                var processes = _serviceProcessService.GetAllByServiceInfoId(serviceInfo.Id).Data;
                var processesUpdated = new List<ServiceProcessUpdatedDto>();
                foreach (var p in processes)
                {
                    processesUpdated.Add(new ServiceProcessUpdatedDto
                    {
                        Id = p.Id,
                        ProcessName = _materialService.GetById(p.MaterialId).Data.Name,
                        //ProcessTypeId = p.ProcessTypeId,
                        Status = p.Status
                    });
                }
                completedAssembly.ServiceProcesses = processesUpdated;

                var medias = _mediaService.GetAllByServiceMediaQuery(new ServiceMediaQueryDto
                {
                    ServiceInfoId = serviceInfo.Id,
                    ServiceStatus = serviceInfo.StatusId
                }).Data;
                var miniMedias = new List<ServiceMediaDto>();
                foreach (var m in medias)
                {
                    miniMedias.Add(new ServiceMediaDto
                    {
                        Link = m.MediaLink,
                        Type = m.MediaType
                    });
                }
                completedAssembly.ServiceMedias = miniMedias;

                var regionsResult = _regionService.GetAll();
                if (!regionsResult.Success)
                {
                    return  new ErrorDataResult<PagedModel<List<CompletedAssemblyDto>>>("Tamamlanan servis kaydına ulaşılamadı! (Hata)");
                }
                completedAssembly.Regions = regionsResult.Data;

                completedAssemblies.Add(completedAssembly);

               
            }
            int pageNumber = (int)(paginationDto.PageNumber != null ? paginationDto.PageNumber : 1);
            int pageSize = (int)(paginationDto.PageSize != null ? paginationDto.PageSize : 10);
            pageNumber = pageNumber > 0 ? pageNumber : 1;
            pageSize = pageSize > 0 ? pageSize : 10;
            int start = (int)((pageNumber - 1) * pageSize);

            int totalRecords = completedAssemblies.Count;
            completedAssemblies = completedAssemblies.Skip(start).Take(pageSize).ToList();

            var pagedModel = new PagedModel<List<CompletedAssemblyDto>>(completedAssemblies.OrderBy(p => p.Id).ToList(), totalRecords, pageNumber, pageSize);
            return new SuccessDataResult<PagedModel<List<CompletedAssemblyDto>>>(pagedModel);
            //completedAssemblies = completedAssemblies.OrderBy(p => p.Id).ToList();
            //var count = completedAssemblies.Count();
            //return new SuccessDataResult<List<CompletedAssemblyDto>>(completedAssemblies);

        }

        //[SecuredOperation("nodejs,admin")]
        //[CacheRemoveAspect("IServiceInfoService.Get")]
        public IResult CompleteAssembly(CompleteAssemblyDto completeAssemblyDto)
        {
            var serviceInfoResult = GetById(completeAssemblyDto.ServiceInfoId);
            if (!serviceInfoResult.Success)
            {
                return new ErrorResult(serviceInfoResult.Message);
            }
            var serviceInfo = serviceInfoResult.Data;
            serviceInfo.StatusId = 2;
            serviceInfo.InstallerNote = completeAssemblyDto.Note;
            serviceInfo.InstallerUserId = completeAssemblyDto.InstallerUserId;
            serviceInfo.InstallerTime = DateTime.Now;

            var serviceInfoId = serviceInfo.Id;
            Update(serviceInfo);
            foreach (var processItem in completeAssemblyDto.ServiceProcessesUpdate)
            {
                var processResult = _serviceProcessService.GetById(processItem.Id);
                if (!processResult.Success)
                {
                    return new ErrorResult(processResult.Message);
                }
                var process = processResult.Data;
                process.Status = processItem.Status;
                process.Description = processItem.Description;
                process.SerialNo = processItem.SerialNo;

                var resultUpdate = _serviceProcessService.Update(process);
                if (!resultUpdate.Success)
                {
                    return new ErrorResult(processResult.Message);
                }
                var installedIsExists = _installedMaterialService.GetMaterialIfExistsInCar(serviceInfo.VehicleId, process.MaterialId);
                if (installedIsExists.Data == null)
                {
                    var resultInstall = _installedMaterialService.Add(
                       new InstalledMaterial
                       {
                           Description = processItem.Description,
                           MaterialId = process.MaterialId,
                           SerialNo = processItem.SerialNo,
                           SerialNoOld = processItem.SerialNoOld,
                           ImeiNo = processItem.ImeiNo,
                           ImeiNoOld = processItem.ImeiNoOld,
                           VehicleId = serviceInfo.VehicleId,
                           ServiceInfoId = serviceInfo.Id,
                           CreatedDate = DateTime.Now
                       }
                    );
                    if (!resultInstall.Success)
                    {
                        return new ErrorResult(processResult.Message);
                    }
                }
                else
                {
                    var materialForUpdate = installedIsExists.Data;
                    materialForUpdate.CreatedDate = DateTime.Now;
                    materialForUpdate.SerialNo = processItem.SerialNo;
                    materialForUpdate.SerialNoOld = processItem.SerialNoOld;
                    materialForUpdate.ImeiNo = processItem.ImeiNo;
                    materialForUpdate.ImeiNoOld = processItem.ImeiNoOld;
                    materialForUpdate.Description = processItem.Description;
                    materialForUpdate.ServiceInfoId = serviceInfo.Id;
                    var resultInstall = _installedMaterialService.Update(materialForUpdate);

                    if (!resultInstall.Success)
                    {
                        return new ErrorResult(processResult.Message);
                    }
                }
               
            }


            //Servis sırasında tespitedilenler
            foreach (var processItem in completeAssemblyDto.ServiceProcessesNew)
            {

                var process = new ServiceProcess()
                {
                    Description = processItem.Description,
                    MaterialId = processItem.MaterialId,
                    SerialNo = processItem.SerialNo,
                    ServiceInfoId = serviceInfo.Id,
                    Status = true
                };

                var resultUpdate = _serviceProcessService.Add(process);
                if (!resultUpdate.Success)
                {
                    return new ErrorResult(resultUpdate.Message);
                }

                var installedIsExists = _installedMaterialService.GetMaterialIfExistsInCar(serviceInfo.VehicleId, process.MaterialId);
                if (installedIsExists.Data == null)
                {
                    var resultInstall = _installedMaterialService.Add(
                       new InstalledMaterial
                       {
                           Description = processItem.Description,
                           MaterialId = process.MaterialId,
                           SerialNo = processItem.SerialNo,
                           SerialNoOld = processItem.SerialNoOld,
                           ImeiNo = processItem.ImeiNo,
                           ImeiNoOld = processItem.ImeiNoOld,
                           VehicleId = serviceInfo.VehicleId,
                           ServiceInfoId = serviceInfo.Id,
                           CreatedDate = DateTime.Now
                       }
                    );
                    if (!resultInstall.Success)
                    {
                        return new ErrorResult(resultInstall.Message);
                    }
                }
                else
                {
                    var materialForUpdate = installedIsExists.Data;
                    materialForUpdate.CreatedDate = DateTime.Now;
                    materialForUpdate.SerialNo = processItem.SerialNo;
                    materialForUpdate.SerialNoOld = processItem.SerialNoOld;
                    materialForUpdate.ImeiNo = processItem.ImeiNo;
                    materialForUpdate.ImeiNoOld = processItem.ImeiNoOld;
                    materialForUpdate.Description = processItem.Description;
                    materialForUpdate.ServiceInfoId = serviceInfo.Id;
                    var resultInstall = _installedMaterialService.Update(materialForUpdate);

                    if (!resultInstall.Success)
                    {
                        return new ErrorResult(resultInstall.Message);
                    }
                }

            }

            foreach (var item in completeAssemblyDto.MediaLinks)
            {
                var serviceMedia = new ServiceMedia();
                serviceMedia.Id = 0;
                serviceMedia.ServiceInfoId = serviceInfoId;
                serviceMedia.ServiceStatus = 2;
                serviceMedia.MediaLink = item.Link;
                serviceMedia.MediaType = 1;
                _mediaService.Add(serviceMedia);
            }


            return new SuccessResult("Servis montaj tamamlandı");
        }

        //[SecuredOperation("nodejs,admin")]
        //[CacheAspect]
        public IDataResult<PagedModel<List<ServiceCompletedDto>>> GetAllServiceCompletedStatus(PaginationDto paginationDto)
        {
            var result = GetAllByServiceStatus(3);
            if (!result.Success)
            {
                return new ErrorDataResult<PagedModel<List<ServiceCompletedDto>>>(result.Message);
            }
            var serviceInfos = result.Data;
            var serviceCompleteds = new List<ServiceCompletedDto>();

            foreach (var serviceInfo in serviceInfos)
            {
                var serviceCompleted = new ServiceCompletedDto();
                serviceCompleted.Id = serviceInfo.Id;
                serviceCompleted.Description = serviceInfo.FinishedNote;
                serviceCompleted.FinishTime = serviceInfo.FinishTime;
                serviceCompleted.ControllerUserId = serviceInfo.ControllerUserId;
                serviceCompleted.PaymentTypeId = serviceInfo.PaymentTypeId;
                serviceCompleted.Price = serviceInfo.Price;

                var resulasd = _vehicleService.GetById(serviceInfo.VehicleId);
                var vehicle = resulasd.Data;
                serviceCompleted.Vehicle = new VehicleMiniDto
                {
                    Id = vehicle.Id,
                    Plate = vehicle.Plate,
                    TaxiType = vehicle.TaxiTypeId
                };

                var driver = _driverService.GetById(serviceInfo.DriverId).Data;
                serviceCompleted.Driver = new DriverMiniDto
                {
                    Id = driver.Id,
                    Gsm = driver.Phone,
                    Name = driver.Name + " " + driver.Surname
                };


                var service = _serviceService.GetById(serviceInfo.InstallerServiceId).Data;
                serviceCompleted.Service = new ServiceMiniDto
                {
                    Id = service.Id,
                    Name = service.Name
                };

                var processes = _serviceProcessService.GetAllByServiceInfoId(serviceInfo.Id).Data;
                var processesUpdated = new List<ServiceProcessUpdatedDto>();
                foreach (var p in processes)
                {
                    processesUpdated.Add(new ServiceProcessUpdatedDto
                    {
                        Id = p.Id,
                        ProcessName = _materialService.GetById(p.MaterialId).Data.Name,
                        //ProcessTypeId = p.ProcessTypeId,
                        Status = p.Status
                    });
                }
                serviceCompleted.ServiceProcesses = processesUpdated;

                var medias = _mediaService.GetAllByServiceMediaQuery(new ServiceMediaQueryDto
                {
                    ServiceInfoId = serviceInfo.Id,
                    ServiceStatus = serviceInfo.StatusId
                }).Data;
                var miniMedias = new List<ServiceMediaDto>();
                foreach (var m in medias)
                {
                    miniMedias.Add(new ServiceMediaDto
                    {
                        Link = m.MediaLink,
                        Type = m.MediaType
                    });
                }
                serviceCompleted.ServiceMedias = miniMedias;

                var regionsResult = _regionService.GetAll();
                if (!regionsResult.Success)
                {
                    return new ErrorDataResult<PagedModel<List<ServiceCompletedDto>>>(regionsResult.Message);
                }
                serviceCompleted.Regions = regionsResult.Data;

                serviceCompleteds.Add(serviceCompleted);

            }

            int pageNumber = (int)(paginationDto.PageNumber != null ? paginationDto.PageNumber : 1);
            int pageSize = (int)(paginationDto.PageSize != null ? paginationDto.PageSize : 10);
            pageNumber = pageNumber > 0 ? pageNumber : 1;
            pageSize = pageSize > 0 ? pageSize : 10;
            int start = (int)((pageNumber - 1) * pageSize);

            int totalRecords = serviceCompleteds.Count;
            serviceCompleteds = serviceCompleteds.Skip(start).Take(pageSize).ToList();

            var pagedModel = new PagedModel<List<ServiceCompletedDto>>(serviceCompleteds.OrderBy(p => p.Id).ToList(), totalRecords, pageNumber, pageSize);
            return new SuccessDataResult<PagedModel<List<ServiceCompletedDto>>>(pagedModel);

            //serviceCompleteds = serviceCompleteds.OrderBy(p => p.Id).ToList();
            //return new SuccessDataResult<PagedModel<List<ServiceCompletedDto>>>(serviceCompleteds);
        }

        //[SecuredOperation("nodejs,admin")]
        //[CacheRemoveAspect("IServiceInfoService.Get")]
        public IResult CompleteService(CompleteServiceDto completeServiceDto)
        {
            var serviceInfoResult = GetById(completeServiceDto.ServiceInfoId);
            if (!serviceInfoResult.Success)
            {
                return new ErrorResult(serviceInfoResult.Message);
            }
            var serviceInfo = serviceInfoResult.Data;
            serviceInfo.StatusId = 3;
            serviceInfo.FinishedNote = completeServiceDto.Note;
            serviceInfo.ControllerUserId = completeServiceDto.ControllerUserId;
            serviceInfo.FinishTime = DateTime.Now;
            serviceInfo.Price = completeServiceDto.Price;
            serviceInfo.PaymentTypeId = completeServiceDto.PaymentTypeId;
            var serviceInfoId = serviceInfo.Id;
            var result = Update(serviceInfo);
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            return new SuccessResult("Servis çıkış tamamlandı");
        }
    }
}