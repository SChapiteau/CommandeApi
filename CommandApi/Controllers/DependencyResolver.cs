using CommandApi.Process;
using System.ComponentModel;
using Unity;

namespace CommandApi.Controllers
{
    public static class DependencyResolver
    {
        private static IUnityContainer container;

        static DependencyResolver()
        {
            container = new UnityContainer();

            container.RegisterType<IValidateComemandProcess, ValidateComemandProcess>();
        }


        public static T Ressolve<T>()
        {
            return container.Resolve <T> ();
        }
    }
}
