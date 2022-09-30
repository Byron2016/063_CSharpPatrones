namespace Patrons.Samples.FactoryN
{
    //5
    public interface ISample_000_005
    {
        string CurrentDateTime { get; set; }
    }

    public class Sample_000_005 : ISample_000_005
    {
        public string CurrentDateTime { get; set; } = DateTime.Now.ToString();
    }
}
