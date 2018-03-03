using Autofac;
using eBarService.DatabaseOperations;
using eBarService.Interfaces;

namespace eBarService
{
    public class IocContainer
    {
        public IocContainer()
        {
            InitializeIOC();
        }

        private void InitializeIOC()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<IUserOperations>().As<UserOperations>();
            builder.Build();
        }
    }
}