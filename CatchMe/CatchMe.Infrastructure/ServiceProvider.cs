using Ninject;
using Ninject.Parameters;

namespace CatchMe.Infrastructure
{
    public static class ServiceProvider
    {
        public static IKernel Kernel { private set; get; }

        public static void SetContainer(IKernel kernel)
        {
            Kernel = kernel;
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        public static T Get<T>(params IParameter[] constructorArguments)
        {
            return Kernel.Get<T>(constructorArguments);
        }
    }
}
