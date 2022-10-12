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
    public class RegionManager : IRegionService
    {
        IRegionDal _regionDal;

        public RegionManager(IRegionDal serviceDal)
        {
            _regionDal = serviceDal;
        }

        public IResult Add(Region region)
        {
            _regionDal.Add(region);
            return new SuccessResult();
        }

        public IDataResult<List<Region>> GetAll()
        {
            var result = _regionDal.GetAll();
            return new SuccessDataResult<List<Region>>(result);
        }

        public IDataResult<Region> GetById(int id)
        {
            var result = _regionDal.Get(s => s.Id == id);
            return new SuccessDataResult<Region>(result);
        }

        public IResult Remove(Region region)
        {
            _regionDal.Delete(region);
            return new SuccessResult();
        }

        public IResult Update(Region region)
        {
            _regionDal.Update(region);
            return new SuccessResult();
        }
    }
}
