using Autofac;
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
            //builder.RegisterType<MainWindow>().AsSelf();
            //builder.RegisterType<MainViewModel>().AsSelf();

            //builder.RegisterType<NavigationViewModel>()
            //  .As<INavigationViewModel>();

            //builder.RegisterType<NavigationDataProvider>()
            //  .As<INavigationDataProvider>();

            //builder.RegisterType<FileDataService>()
            //  .As<IDataService>();

            return builder.Build();
        }
    }
}
