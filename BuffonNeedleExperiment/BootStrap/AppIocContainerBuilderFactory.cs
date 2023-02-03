using Autofac;
using BuffonNeedleExperiment.Models;
using BuffonNeedleExperiment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuffonNeedleExperiment.BootStrap
{
    public class AppIocContainerBuilderFactory : IAppIoCContainerBuilderFactory
    {
        public ContainerBuilder GetBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LineFactory>().AsSelf().SingleInstance();
            builder.RegisterType<PiCounter>().AsSelf();
            builder.RegisterType<MainWindowViewModel>().AsSelf();        
            builder.RegisterType<Settings>().AsSelf().SingleInstance();
            builder.RegisterType<NeedleFactory>().AsSelf().SingleInstance();
            return builder;
        }
    }
}
