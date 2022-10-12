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
    public interface IServiceMediaService
    {
        IDataResult<List<ServiceMedia>> GetAllByServiceMediaQuery(ServiceMediaQueryDto serviceMediaQueryDto);
        IDataResult<ServiceMedia> GetById(int id);
        IResult Add(ServiceMedia serviceMedia);
        IResult Update(ServiceMedia serviceMedia);
        IResult Remove(ServiceMedia serviceMedia);
    }
}
