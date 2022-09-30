namespace Patrons.Samples.FactoryN
{
    public interface INUserData
    {
        string? Name { get; set; }
    }
    public class NUserData : INUserData
    {
        public string? Name { get; set; }
    }
}