using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuffonNeedleExperiment.BootStrap
{
    public interface IAppIoCContainerBuilderFactory
    {
        ContainerBuilder GetBuilder();
    }
}
