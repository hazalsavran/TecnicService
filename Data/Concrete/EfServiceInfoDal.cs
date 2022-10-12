using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfServiceInfoDal : EfEntityRepositoryBase<ServiceInfo, ITaksiContext>, IServiceInfoDal
    {
        public List<PendingAssemblyDto> GetAllPendingAssemblyStatus()
        {
			using (ITaksiContext context = new ITaksiContext())
			{
				var result = from si in context.ServiceInfos
							 where si.StatusId == 1
							 join v in context.Vehicles on si.VehicleId equals v.Id
							 join d in context.Driver on si.DriverId equals d.Id
							 join s in context.Services on si.InstallerServiceId equals s.Id
							 join r in context.Regions on s.RegionId equals r.Id
							 select new PendingAssemblyDto
							 {
								 Id = si.Id,
								 CreatedTime = si.CreatedTime,
								 CreatedUserId = si.CreatedUserId,
								 Description = si.CreatedNote,
								 Vehicle = new VehicleMiniDto
								 {
									 Id = v.Id,
									 Plate = v.Plate,
									 TaxiType = v.TaxiTypeId
								 },
								 Driver = new DriverMiniDto
								 {
									 Id = d.Id,
									 Gsm = d.Phone,
									 Name = d.Name + " " + d.Surname
								 },
								 Service = new ServiceMiniDto
								 {
									 Id = s.Id,
									 Name = s.Name
								 },
								 ServiceProcesses = (List<ServiceProcessMiniDto>)(from sp in context.ServiceProcesses
													 where sp.ServiceInfoId == si.Id
													 join m in context.Materials
													 on sp.MaterialId equals m.Id
													 select new ServiceProcessMiniDto
													 {
														 Id = sp.Id,
														 ProcessName = m.Name
													 }).ToList(),
								 ServiceMedias = (List<ServiceMediaDto>)(from sm in context.ServiceMedias
												  where sm.ServiceStatus == si.StatusId && sm.ServiceInfoId == si.Id
												  select new ServiceMediaDto
												  {
													  Link = sm.MediaLink,
													  Type = sm.MediaType
												  }).ToList(),
								 Region = new Region
								 {
									 Id = r.Id,
									 Name = r.Name,
									 ProvinceId = r.ProvinceId
								 }

							 };
				return result.ToList();
			}
		}

        public PendingAssemblyDto GetByIdPendingAssemblyStatus(int serviceInfoId)
        {
			using (ITaksiContext context = new ITaksiContext())
			{
				var result = from si in context.ServiceInfos
							 where si.Id == serviceInfoId && si.StatusId == 1
							 join v in context.Vehicles on si.VehicleId equals v.Id
							 join d in context.Driver on si.DriverId equals d.Id
							 join s in context.Services on si.InstallerServiceId equals s.Id
							 join r in context.Regions on s.RegionId equals r.Id
							 select new PendingAssemblyDto
							 {
								 Id = si.Id,
								 CreatedTime = si.CreatedTime,
								 CreatedUserId = si.CreatedUserId,
								 Description = si.CreatedNote,
								 Vehicle = new VehicleMiniDto
								 {
									 Id = v.Id,
									 Plate = v.Plate,
									 TaxiType = v.TaxiTypeId
								 },
								 Driver = new DriverMiniDto
								 {
									 Id = d.Id,
									 Gsm = d.Phone,
									 Name = d.Name + " " + d.Surname
								 },
								 Service = new ServiceMiniDto
								 {
									 Id = s.Id,
									 Name = s.Name
								 },
								 ServiceProcesses = (from sp in context.ServiceProcesses
													 where sp.ServiceInfoId == si.Id
													 join m in context.Materials
													 on sp.MaterialId equals m.Id
													 select new ServiceProcessMiniDto
													 {
														 Id = sp.Id,
														 ProcessName = m.Name
													 }).ToList(),
								 ServiceMedias = (from sm in context.ServiceMedias
												  where sm.ServiceStatus == si.StatusId && sm.ServiceInfoId == si.Id
												  select new ServiceMediaDto
												  {
													  Link = sm.MediaLink,
													  Type = sm.MediaType
												  }).ToList(),
								 Region = new Region
								 {
									 Id = r.Id,
									 Name = r.Name,
									 ProvinceId = r.ProvinceId
								 }

							 };
               
				return result.FirstOrDefault();
			}
		}
    }
}
