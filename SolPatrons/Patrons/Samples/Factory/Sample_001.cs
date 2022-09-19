namespace Patrons.Samples.Factory
{
    public interface ISample_001
    {
        string CurrentDateTime { get; set; }
    }

    public class Sample_001 : ISample_001
    {
        public string CurrentDateTime { get; set; } = DateTime.Now.ToString();
    }
}
