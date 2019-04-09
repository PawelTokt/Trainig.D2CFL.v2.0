using System;
using Autofac;
using D2CFL.Database.Interfaces;
using D2CFL.Database.Models;
using D2CFL.Database.Repository;

namespace D2CFL.Database
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repositories
            builder.RegisterType<Repository<PlayerEntity, Guid>>().As<IRepository<PlayerEntity, Guid>>().InstancePerLifetimeScope();

            // UnitOfWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}
