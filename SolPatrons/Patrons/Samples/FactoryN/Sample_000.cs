namespace Patrons.Samples.FactoryN
{
    public interface ISample_000
    {
        string CurrentDateTime { get; set; }
    }

    public class Sample_000 : ISample_000
    {
        public string CurrentDateTime { get; set; } = DateTime.Now.ToString();
    }
}
