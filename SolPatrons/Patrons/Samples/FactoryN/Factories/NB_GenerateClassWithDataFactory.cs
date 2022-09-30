namespace Patrons.Samples.FactoryN.Factories
{
    public static class NGenerateClassWithDataFactoryExtension
    {
        public static void NAddGenericClassWithDataFactory(this IServiceCollection services)
        {
            services.AddTransient<INUserData, NUserData>();
            services.AddSingleton<Func<INUserData>>(x => () => x.GetService<INUserData>()!);
            services.AddSingleton<INUserDataFactory, NUserDataFactory>(); //Registra el factory que llama al Func
        }
    }
    public interface INUserDataFactory
    {
        INUserData Create(string name);
    }

    public class NUserDataFactory : INUserDataFactory
    {
        private readonly Func<INUserData> _factory;

        public NUserDataFactory(Func<INUserData> factory)
        {
            _factory = factory;
        }

        public INUserData Create(string name)
        {
            var output = _factory();
            output.Name = name;
            return output;
        }
    }
}
