using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
	public class AutofacBusinessModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
            // Bu projeye ozel resolverlar burada olusturulur
            //builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            //builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            //builder.RegisterType<PanelUserManager>().As<IPanelUserService>().SingleInstance();
            //builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            //builder.RegisterType<DeviceManager>().As<IDeviceService>().SingleInstance();
            builder.RegisterType<DriverManager>().As<IDriverService>().SingleInstance();
            builder.RegisterType<ServiceInfoManager>().As<IServiceInfoService>().SingleInstance();
			builder.RegisterType<ServiceMediaManager>().As<IServiceMediaService>().SingleInstance();
			builder.RegisterType<ServiceManager>().As<IServiceService>().SingleInstance();

			builder.RegisterType<InstalledMaterialManager>().As<IInstalledMaterialService>().SingleInstance();
			builder.RegisterType<VehicleManager>().As<IVehicleService>().SingleInstance();
			builder.RegisterType<ServiceProcessManager>().As<IServiceProcessService>().SingleInstance();
            builder.RegisterType<MaterialManager>().As<IMaterialService>().SingleInstance();
            builder.RegisterType<RegionManager>().As<IRegionService>().SingleInstance();
            builder.RegisterType<VehicleOwnerManager>().As<IVehicleOwnerService>().SingleInstance();
            //builder.RegisterType<VehicleGroupManager>().As<IVehicleGroupService>().SingleInstance();
            //builder.RegisterType<TaximeterHistoryManager>().As<ITaximeterHistoryService>().SingleInstance();
            //builder.RegisterType<TaximeterLocationManager>().As<ITaximeterLocationService>().SingleInstance();
            //builder.RegisterType<NdjsLogManager>().As<INdjsLogService>().SingleInstance();
            //builder.RegisterType<FaultyDeviceManager>().As<IFaultyDeviceService>().SingleInstance();

            //builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
            //builder.RegisterType<EfPanelUserDal>().As<IPanelUserDal>().SingleInstance();
            //builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            //builder.RegisterType<EfDeviceDal>().As<IDeviceDal>().SingleInstance();
            builder.RegisterType<EfDriverDal>().As<IDriverDal>().SingleInstance();
            builder.RegisterType<EfServiceInfoDal>().As<IServiceInfoDal>().SingleInstance();
			builder.RegisterType<EfServiceMediaDal>().As<IServiceMediaDal>().SingleInstance();
			builder.RegisterType<EfServiceDal>().As<IServiceDal>().SingleInstance();
            builder.RegisterType<EfInstalledMaterialDal>().As<IInstalledMaterialDal>().SingleInstance();
            builder.RegisterType<EfVehicleDal>().As<IVehicleDal>().SingleInstance();
            builder.RegisterType<EfServiceProcessDal>().As<IServiceProcessDal>().SingleInstance();
            builder.RegisterType<EfMaterialDal>().As<IMaterialDal>().SingleInstance();
            builder.RegisterType<EfRegionDal>().As<IRegionDal>().SingleInstance();
            builder.RegisterType<EfVehicleOwnerDal>().As<IVehicleOwnerDal>().SingleInstance();
            builder.RegisterType<EfDriverVehicleDal>().As<IDriverVehicleDal>().SingleInstance();
            builder.RegisterType<EfVehicleOwnerDal>().As<IVehicleOwnerDal>().SingleInstance();
            builder.RegisterType<EfVehicleOwnerVehicleDal>().As<IVehicleOwnerVehicleDal>().SingleInstance();
            //builder.RegisterType<EfVehicleGroupDal>().As<IVehicleGroupDal>().SingleInstance();
            //builder.RegisterType<EfTaximeterHistoryDal>().As<ITaximeterHistoryDal>().SingleInstance();
            //builder.RegisterType<EfTaximeterLocationDal>().As<ITaximeterLocationDal>().SingleInstance();
            //builder.RegisterType<EfNdjsLogDal>().As<INdjsLogDal>();
            //builder.RegisterType<EfFaultyDeviceDal>().As<IFaultyDeviceDal>().SingleInstance();

            //IVehicleService _vehicleService;
            //IServiceProcessService _serviceProcessService;

            //builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            // For our interceptors to work
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
			builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
				.EnableInterfaceInterceptors(new ProxyGenerationOptions()
			{
				Selector = new AspectInterceptorSelector()
			}).SingleInstance();


		}
	}
}
