
using System.Collections.Specialized;

namespace Patrons.Samples.FactoryN.Factories
{
    public static class NDifferentImplementationsFactoryExtension
    {
        public static void NAddVehiculeFactory(this IServiceCollection services)
        {
            services.AddTransient<INVehicule, Car>();
            services.AddTransient<INVehicule, Truck>();
            services.AddTransient<INVehicule, Van>();

            services.AddSingleton<Func<IEnumerable<INVehicule>>>
                (x => () => x.GetService<IEnumerable<INVehicule>>()!);

            services.AddSingleton<INVehicleFactory, NVehicleFactory>(); //Registra el factory que llama al Func
        }
    }

    public interface INVehicleFactory
    {
        INVehicule Create(string name);
        INVehicule Create(string name, string owner);
    }

    public class NVehicleFactory : INVehicleFactory
    {
        private readonly Func<IEnumerable<INVehicule>> _factory;
        private readonly string _owner;

        public NVehicleFactory(Func<IEnumerable<INVehicule>> factory)
        {
            _factory = factory;
        }

        public INVehicule Create(string name)
        {
            var set = _factory();
            INVehicule output = set.Where(x => x.VehicleType == name).First();

            return output;
        }

        public INVehicule Create(string name, string owner)
        {
            var set = _factory();
            INVehicule output = set.Where(x => x.VehicleType == name).First();
            output.Owner = owner;

            return output;
        }
    }
}
