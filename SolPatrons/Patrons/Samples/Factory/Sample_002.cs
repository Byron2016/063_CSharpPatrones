namespace Patrons.Samples.Factory
{
    public interface ISample_002
    {
        int RandomValue { get; set; }
    }

    public class Sample_002 : ISample_002
    {
        public int RandomValue { get; set; }

        public Sample_002()
        {
            RandomValue = Random.Shared.Next(1, 100);
        }
    }
}
