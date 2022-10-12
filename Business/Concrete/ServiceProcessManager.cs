using Business.Abstract;
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
    public class ServiceProcessManager : IServiceProcessService
    {
        IServiceProcessDal _serviceProcessDal;

        public ServiceProcessManager(IServiceProcessDal serviceProcessDal)
        {
            _serviceProcessDal = serviceProcessDal;
        }

        public IResult Add(ServiceProcess serviceProcess)
        {
            _serviceProcessDal.Add(serviceProcess);
            return new SuccessResult();
        }

        public IDataResult<List<ServiceProcess>> GetAll()
        {
            var processes = _serviceProcessDal.GetAll();
            return new SuccessDataResult<List<ServiceProcess>>(processes);
        }

        public IDataResult<List<ServiceProcess>> GetAllByServiceInfoId(int serviceInfoId)
        {
            var processes = _serviceProcessDal.GetAll(p => p.ServiceInfoId == serviceInfoId);
            return new SuccessDataResult<List<ServiceProcess>>(processes);
        }

        public IDataResult<ServiceProcess> GetById(int id)
        {
            var process = _serviceProcessDal.Get(p => p.Id == id);
            return new SuccessDataResult<ServiceProcess>(process);
        }

        public IResult Remove(ServiceProcess serviceProcess)
        {
            _serviceProcessDal.Delete(serviceProcess);
            return new SuccessResult();
        }

        public IResult Update(ServiceProcess serviceProcess)
        {
            _serviceProcessDal.Update(serviceProcess);
            return new SuccessResult();
        }
    }
}
