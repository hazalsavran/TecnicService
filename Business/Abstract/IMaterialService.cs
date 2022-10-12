using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IMaterialService
	{
		IDataResult<List<Material>> GetAll();
		IDataResult<Material> GetById(int materialId);
		IResult Add(Material material);
		IResult Update(Material material);
		IResult Remove(Material material);
	}
}
