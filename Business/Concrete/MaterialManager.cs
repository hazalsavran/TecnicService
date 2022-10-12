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
    public class MaterialManager : IMaterialService
    {
        IMaterialDal _materialDal;

        public MaterialManager(IMaterialDal materialDal)
        {
            _materialDal = materialDal;
        }

        public IResult Add(Material material)
        {
            _materialDal.Add(material);
            return new SuccessResult();
        }

        public IDataResult<List<Material>> GetAll()
        {           
            var materials = _materialDal.GetAll();
            return new SuccessDataResult<List<Material>>(materials);
        }

        public IDataResult<Material> GetById(int materialId)
        {
            var material = _materialDal.Get(m => m.Id == materialId);
            return new SuccessDataResult<Material>(material);
        }

        public IResult Remove(Material material)
        {
            _materialDal.Delete(material);
            return new SuccessResult();
        }

        public IResult Update(Material material)
        {
            var result = _materialDal.Get(x=>x.Id==material.Id);
            if (string.IsNullOrWhiteSpace(material.Name)==false)
                result.Name = material.Name;
            if (string.IsNullOrWhiteSpace(material.CategoryId.ToString())==false)
                result.CategoryId= material.CategoryId;
            if (string.IsNullOrWhiteSpace(material.NumberOfUnits.ToString())==false)
                result.NumberOfUnits= material.NumberOfUnits;                            
                
            _materialDal.Update(result);
            return new SuccessResult();
        }
    }
}
