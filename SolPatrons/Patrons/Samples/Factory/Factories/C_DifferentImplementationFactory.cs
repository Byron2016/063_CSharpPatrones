namespace Patrons.Samples.Factory.Factories
{
    public static class C_DifferentImplementationsFactoryExtension
    {
        public static void AddVehiculeFactory(this IServiceCollection services)
        {
            services.AddTransient<IVehicule, Car>();
            services.AddTransient<IVehicule, Truck>();
            services.AddTransient<IVehicule, Van>();

            services.AddSingleton<Func<IEnumerable<IVehicule>>>
                (x => () => x.GetService<IEnumerable<IVehicule>>()!);

            services.AddSingleton<IVehicleFactory, VehicleFactory>(); //Registra el factory que llama al Func
        }
    }

    public interface IVehicleFactory
    {
        IVehicule Create(string name);
    }

    public class VehicleFactory : IVehicleFactory
    {
        private readonly Func<IEnumerable<IVehicule>> _factory;

        public VehicleFactory(Func<IEnumerable<IVehicule>> factory)
        {
            _factory = factory;
        }

        public IVehicule Create(string name)
        {
            var set = _factory();
            IVehicule output = set.Where(x => x.VehicleType == name).First();

            return output;
        }
    }
}
