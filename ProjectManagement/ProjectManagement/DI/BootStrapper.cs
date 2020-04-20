using Autofac;
using BIM4PM.DataAccess;
using BIM4PM.DataAccess.Interfaces;
using BIM4PM.UI.Tools.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.DI
{
   public class BootStrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LoginView>().AsSelf();
            //builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<AuthenticationRepository>()
             .As<IAuthenticationRepository>();

            builder.RegisterType<ProjectRepository>()
              .As<IProjectRepository>();

            builder.RegisterType<UserRepository>()
              .As<IUserRepository>();
            builder.RegisterType<VersionRepository>()
             .As<IVersionRepository>();

            return builder.Build();
        }
    }
}
