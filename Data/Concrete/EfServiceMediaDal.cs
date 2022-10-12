using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfServiceMediaDal : EfEntityRepositoryBase<ServiceMedia, ITaksiContext>, IServiceMediaDal
    {
        public List<ServiceMedia> GetAllByServiceMediaQuery(ServiceMediaQueryDto serviceMediaQueryDto)
        {
            using (ITaksiContext context = new ITaksiContext())
            {
                Expression<Func<ServiceMedia, bool>> query = q => (serviceMediaQueryDto.ServiceStatus != null ? q.ServiceStatus == serviceMediaQueryDto.ServiceStatus : true)
                && (serviceMediaQueryDto.ServiceInfoId != null ? q.ServiceInfoId == serviceMediaQueryDto.ServiceInfoId : true);
                return context.Set<ServiceMedia>().Where(query).ToList();
            }
        }
    }
}
