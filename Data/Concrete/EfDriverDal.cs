using Core.DataAccess.EntityFramework;
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
    public class EfDriverDal : EfEntityRepositoryBase<Driver, ITaksiContext>, IDriverDal
    {
        public List<Driver> GetAllByInfoQuery(DriverMiniDto driverMiniDto)
        {
            using (ITaksiContext context =new ITaksiContext())
            {
                Expression<Func<Driver, bool>> query = q =>
                   (driverMiniDto.Id > 0 ? q.Id == driverMiniDto.Id : true)
                && (driverMiniDto.Name != null ? q.Name.Contains(driverMiniDto.Name) : true)
                && (driverMiniDto.Surname != null ? q.Surname.Contains(driverMiniDto.Surname) : true)
                && (driverMiniDto.Gsm != null ? q.Phone.Contains(driverMiniDto.Gsm) : true)
                && (driverMiniDto.TCNo != null ? q.TCNo.Contains(driverMiniDto.TCNo) : true);
                //&& (driverMiniDto.Pet != true ? q.Pet.Equals(driverMiniDto.Pet) : true);
                return context.Set<Driver>().Where(query).ToList();
            }
        }

    }
}
