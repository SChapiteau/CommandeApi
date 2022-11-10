using CommandApi.Process;
using CommandApi.DAL;
using CommandApi.Entity.Interface;
using Unity;

namespace CommandApi.Controllers
{
    public static class DependencyResolver
    {
        private static IUnityContainer container;

        static DependencyResolver()
        {
            container = new UnityContainer();

            container.RegisterType<IValidateCommandeProcess, ValidateCommandeProcess>();
        }


        public static T Resolve<T>()
        {
            return container.Resolve <T> ();
        }
    }
}
