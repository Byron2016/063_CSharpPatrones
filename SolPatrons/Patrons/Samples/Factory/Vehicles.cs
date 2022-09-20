namespace Patrons.Samples.Factory
{
    public interface IVehicule
    {
        string VehicleType { get; set; }

        string Start();
    }

    public class Car : IVehicule
    {
        public string VehicleType { get; set; } = "Car";

        public string Start()
        {
            return "The car has been started";
        }
    }

    public class Truck : IVehicule
    {
        public string VehicleType { get; set; } = "Truck";

        public string Start()
        {
            return "The truck has been started";
        }
    }

    public class Van : IVehicule
    {
        public string VehicleType { get; set; } = "Van";

        public string Start()
        {
            return "The van has been started";
        }
    }
}
