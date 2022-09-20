using System.Runtime.CompilerServices;

namespace Patrons.Samples.Factory.Factories
{
    public static class AbstractFactoryExtension
    {
        public static void AddAbstractFactory<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
        {
            services.AddTransient<TInterface, TImplementation>();
            services.AddSingleton<Func<TInterface>>(x => () => x.GetService<TInterface>()!);
            services.AddSingleton<IA_AbstractFactory<TInterface>, A_AbstractFactory<TInterface>>(); //Registra el factory que llama al Func
        }
    }
    public class A_AbstractFactory<T> : IA_AbstractFactory<T>
    {
        private readonly Func<T> _factory;

        public A_AbstractFactory(Func<T> factory)
        {
            _factory = factory;
        }

        public T Create()
        {
            return _factory();
        }
    }

    public interface IA_AbstractFactory<T>
    {
        T Create();
    }
}
