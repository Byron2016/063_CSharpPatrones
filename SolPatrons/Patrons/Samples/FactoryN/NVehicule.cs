
namespace Patrons.Samples.FactoryN
{
    public interface INVehicule
    {
        string VehicleType { get; set; }
        string? Owner { get; set; }

        string Start();
    }
    public class Car : INVehicule
    {
        public string VehicleType { get; set; } = "Car";
        public string? Owner { get; set; }

        public string Start()
        {
            return $"The car has been started, y el dueño es: {Owner}";
        }
    }

    public class Truck : INVehicule
    {
        public string VehicleType { get; set; } = "Truck";
        public string? Owner { get; set; }

        public string Start()
        {
            return $"The truck has been started, y el dueño es: {Owner}";
        }
    }

    public class Van : INVehicule
    {
        public string VehicleType { get; set; } = "Van";
        public string? Owner { get; set; }

        public string Start()
        {
            return $"The van has been started, y el dueño es: {Owner}";
        }
    }
}
