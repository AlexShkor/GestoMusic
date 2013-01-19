namespace GestoMusic
{
    public class SamplesFactory: ISamplesFactory
    {
        public Sample GetGitare()
        {
            return new Sample("./Samples/Guitar.wav");
        }

        public Sample GetDrum()
        {
            return new Sample("./Samples/808-clap.wav");
        }

        public Sample GetPlate()
        {
            return new Sample("./Samples/808-clap.wav");
        }

        public Sample GetTube()
        {
            return new Sample("./Samples/808-clap.wav");
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