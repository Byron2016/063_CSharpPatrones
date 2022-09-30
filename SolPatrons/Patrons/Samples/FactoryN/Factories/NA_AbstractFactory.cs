namespace Patrons.Samples.FactoryN.Factories
{
    public static class NAbstractFactoryExtension
    {
        public static void NAddAbstractFactory<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
        {
            services.AddTransient<TInterface, TImplementation>();
            services.AddSingleton<Func<TInterface>>(x => () => x.GetService<TInterface>()!);
            services.AddSingleton<INA_AbstractFactory<TInterface>, NA_AbstractFactory<TInterface>>(); //Registra el factory que llama al Func
        }
    }

    public class NA_AbstractFactory<T> : INA_AbstractFactory<T>
    {
        private readonly Func<T> _factory;

        public NA_AbstractFactory(Func<T> factory)
        {
            _factory = factory;
        }

        public T Create()
        {
            return _factory();
        }
    }

    public interface INA_AbstractFactory<T>
    {
        T Create();
    }
}
