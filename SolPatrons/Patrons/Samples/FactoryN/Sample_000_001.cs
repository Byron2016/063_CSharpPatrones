namespace Patrons.Samples.FactoryN
{
    public interface ISample_000_001
    {
        string CurrentDateTime { get; set; }
    }

    public class Sample_000_001 : ISample_000_001
    {
        public string CurrentDateTime { get; set; } = DateTime.Now.ToString();
    }
}
