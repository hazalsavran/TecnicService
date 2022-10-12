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
    public class EfDeviceDal : EfEntityRepositoryBase<Device, ITaksiContext>, IDeviceDal
    {
        public List<Device> GetAllByInfoQuery(DeviceInfoQueryDto deviceInfoQueryDto)
        {
            using (ITaksiContext context = new ITaksiContext())
            {
                Expression<Func<Device, bool>> query = q => 
                   (deviceInfoQueryDto.Plate != null ? q.Plate.Contains(deviceInfoQueryDto.Plate) : true)
                && (deviceInfoQueryDto.TaxiType >0  ? q.TaxiType == deviceInfoQueryDto.TaxiType : true)
                && (deviceInfoQueryDto.Imei != null ? q.Imei.Contains( deviceInfoQueryDto.Imei ): true);
                return context.Set<Device>().Where(query).ToList();
            }
        }

        //select COUNT(Id) as DeviceCount, TaxiType from Device group by TaxiType;
        public List<DeviceCountDto> GetDevicesCount()
        {
            using (ITaksiContext context = new ITaksiContext())
            {
                return context.Devices.GroupBy(d => d.TaxiType)
                        .Select(d => new DeviceCountDto{
                            TaxiType = d.Key,
                            DeviceCount = d.Count()
                        }).ToList();
            }
        }
    }
}
