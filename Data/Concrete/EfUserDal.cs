using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
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
    public class EfUserDal : EfEntityRepositoryBase<User, ITaksiContext>, IUserDal
    {
        public int GetHistoryCount(UserCountDto userCountDto)
        {
            using (var context = new ITaksiContext())
            {
                if (string.IsNullOrWhiteSpace(userCountDto.Id) == false )
                {
                    return context.Users.Count(x => x.Id == userCountDto.Id);
                }                                
                    return context.Users.Count();
                
            }
        }
    }
}
