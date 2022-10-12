using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IRegionService
	{
		IDataResult<List<Region>> GetAll();
		IDataResult<Region> GetById(int id);
		IResult Add(Region region);
		IResult Update(Region region);
		IResult Remove(Region region);
	}
}
