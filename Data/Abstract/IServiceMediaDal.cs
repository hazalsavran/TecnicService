﻿using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IServiceMediaDal : IEntityRepository<ServiceMedia>
    {
        public List<ServiceMedia> GetAllByServiceMediaQuery(ServiceMediaQueryDto serviceMediaQueryDto);
    }
}
