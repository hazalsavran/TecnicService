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
    public class ServiceMediaManager : IServiceMediaService
    {
        IServiceMediaDal _serviceMediaDal;

        public ServiceMediaManager(IServiceMediaDal serviceMediaDal)
        {
            _serviceMediaDal = serviceMediaDal;
        }

        public IResult Add(ServiceMedia serviceMedia)
        {
           _serviceMediaDal.Add(serviceMedia);
            return new SuccessResult();
        }

        public IDataResult<List<ServiceMedia>> GetAllByServiceMediaQuery(ServiceMediaQueryDto serviceMediaQueryDto)
        {
            var serviceMedias = _serviceMediaDal.GetAllByServiceMediaQuery(serviceMediaQueryDto);
            return new SuccessDataResult<List<ServiceMedia>>(serviceMedias);
        }

        public IDataResult<ServiceMedia> GetById(int id)
        {
            var serviceMedia = _serviceMediaDal.Get(m => m.Id == id);
            return new SuccessDataResult<ServiceMedia>(serviceMedia);
        }

        public IResult Remove(ServiceMedia serviceMedia)
        {
            throw new NotImplementedException();
        }

        public IResult Update(ServiceMedia serviceMedia)
        {
            throw new NotImplementedException();
        }
    }
}
