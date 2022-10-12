using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IServiceProcessService
	{
		IDataResult<List<ServiceProcess>> GetAll();
		IDataResult<List<ServiceProcess>> GetAllByServiceInfoId(int serviceInfoId);
		IDataResult<ServiceProcess> GetById(int id);
		IResult Add(ServiceProcess serviceProcess);
		IResult Update(ServiceProcess serviceProcess);
		IResult Remove(ServiceProcess serviceProcess);
	}
}
