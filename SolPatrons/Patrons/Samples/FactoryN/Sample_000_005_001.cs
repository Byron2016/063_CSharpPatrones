namespace Patrons.Samples.FactoryN
{
    //5
    public interface ISample_000_005_001
    {
        int RandomValue { get; set; }
    }

    public class Sample_000_005_001 : ISample_000_005_001
    {
        public int RandomValue { get; set; }

        public Sample_000_005_001()
        {
            RandomValue = Random.Shared.Next(1, 100);
        }
    }
}
