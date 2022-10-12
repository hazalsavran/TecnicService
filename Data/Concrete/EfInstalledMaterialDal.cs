using Core.DataAccess.EntityFramework;
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
    public class EfInstalledMaterialDal : EfEntityRepositoryBase<InstalledMaterial, ITaksiContext>, IInstalledMaterialDal
    {
        public List<InstalledMaterialDetailDto> GetAllWithDetailByVehicleId(int vehicleId)
        {
				using (ITaksiContext context = new ITaksiContext())
				{
					var result = from im in context.InstalledMaterials
								 join m in context.Materials
								 on im.MaterialId equals m.Id
								 where im.VehicleId == vehicleId
								 select new InstalledMaterialDetailDto
								 {
									 Id = im.Id,
									 MaterialId = im.MaterialId,
									 VehicleId = im.VehicleId,
									 Description = im.Description,
									 OperatorId = im.OperatorId,
									 MaterialName = m.Name,
									 SerialNo = im.SerialNo,
									 SerialNoOld = im.SerialNoOld,
									 ImeiNo = im.ImeiNo,
									 ImeiNoOld = im.ImeiNoOld,
									 Date = im.CreatedDate,
									 ServiceInfoId = im.ServiceInfoId,
								 };
					return result.ToList();
				}
		}
    }
}
