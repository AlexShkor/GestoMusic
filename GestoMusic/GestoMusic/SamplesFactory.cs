namespace GestoMusic
{
    public class SamplesFactory: ISamplesFactory
    {
        public Sample GetGitare()
        {
            throw new System.NotImplementedException();
        }

        public Sample GetDrum()
        {
            throw new System.NotImplementedException();
        }

        public Sample GetPlate()
        {
            throw new System.NotImplementedException();
        }

        public Sample GetTube()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface ISamplesFactory
    {
        Sample GetGitare();
        Sample GetDrum();
        Sample GetPlate();
        Sample GetTube();
    }
}