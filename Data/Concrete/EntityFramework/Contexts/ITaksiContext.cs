using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    
    public class ITaksiContext : DbContext
    {
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {          
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=tuhimteknikservis;Integrated Security=True;");
           
        }


        //public DbSet<OperationClaim> OperationClaims { get; set; }
        //public DbSet<PanelUserOperationClaim> PanelUserOperationClaims { get; set; }
        //public DbSet<PanelUser> PanelUsers { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceVersions> DeviceVersions { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<ServiceInfo> ServiceInfos { get; set; }
        public DbSet<ServiceMedia> ServiceMedias { get; set; }
        public DbSet<InstalledMaterial> InstalledMaterials { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleOwner> OwnerVehicles { get; set; }
        public DbSet<TaxiType> TaxiType { get; set; }
        public DbSet<ServiceProcess> ServiceProcesses { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Region> Regions { get; set; }    
        public DbSet<TaximeterTypes> TaximeterTypes { get; set; }

        //public DbSet<VehicleGroups> VehicleGroups { get; set; }
        //public DbSet<DriverVehicle> DriverVehicles { get; set; }
        //public DbSet<VehicleOwnerVehicle> VehicleOwnerVehicles { get; set; }
        //public DbSet<TaximeterHistory> TaximeterHistory { get; set; }
        //public DbSet<TaximeterLocation> TaximeterLocation { get; set; }
        //public DbSet<NdjsLog> NdjsLogs { get; set; }
        //public DbSet<FaultyDevice> FaultyDevices { get; set; }
    }
}
