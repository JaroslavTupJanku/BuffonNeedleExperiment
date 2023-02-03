using Autofac;

namespace BuffonNeedleExperiment.BootStrap
{
    public class IoC
    {
        private static IContainer Container { get; set; }
        public static void Configure(IAppIoCContainerBuilderFactory builderFactory)
        {
            Container = builderFactory.GetBuilder().Build();
        }
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
