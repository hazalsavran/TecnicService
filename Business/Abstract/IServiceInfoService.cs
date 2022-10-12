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
	public interface IServiceInfoService
	{
		IDataResult<List<ServiceInfo>> GetAllByServiceStatus(int serviceStatus);
		IDataResult<ServiceRecordPageDto> GetServiceRecordPageData();
		IDataResult<PendingAssemblyDto> GetByIdPendingAssemblyStatus(int serviceInfoId);
		IDataResult<CompleteServicePageData> GetByIdCompletedAssemblyStatus(int serviceInfoId);
		IDataResult<ServiceRegisterDto> CreateServiceRecord(ServiceRegisterDto serviceRegisterDto);
		IResult CompleteAssembly(CompleteAssemblyDto completeAssemblyDto);
		IResult CompleteService(CompleteServiceDto completeServiceDto);
		IDataResult<PagedModel<List<ServiceCompletedDto>>> GetAllServiceCompletedStatus(PaginationDto paginationDto);
		IDataResult<ServiceDetailPageData> GetServiceDetailByServiceInfoId(int serviceInfoId);
		IDataResult<PagedModel<List<PendingAssemblyDto>>> GetAllPendingAssemblyStatus(PaginationDto paginationDto);
		IDataResult<PagedModel<List<CompletedAssemblyDto>>> GetAllCompletedAssemblyStatus(PaginationDto paginationDto);
		IDataResult<ServiceInfo> GetById(int id);
		IDataResult<ServiceInfo> Add(AddServiceinfoDto serviceInfo);
		IResult Update(ServiceInfo serviceInfo);
		IResult Remove(int serviceInfoId);
	}
}
