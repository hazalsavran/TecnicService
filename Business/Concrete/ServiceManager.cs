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
    public class ServiceManager : IServiceService
    {
        IServiceDal _serviceDal;

        public ServiceManager(IServiceDal serviceDal)
        {
            _serviceDal = serviceDal;
        }

        public IResult Add(Service service)
        {
            _serviceDal.Add(service);
            return new SuccessResult();
        }

        public IDataResult<List<Service>> GetAll()
        {
            var result = _serviceDal.GetAll();
            return new SuccessDataResult<List<Service>>(result);
        }

        public IDataResult<Service> GetById(int id)
        {
            var result = _serviceDal.Get(s => s.Id == id);
            return new SuccessDataResult<Service>(result);
        }

        public IResult Remove(Service service)
        {
            _serviceDal.Delete(service);
            return new SuccessResult();
        }

        public IResult Update(Service service)
        {
            _serviceDal.Update(service);
            return new SuccessResult();
        }
    }
}
