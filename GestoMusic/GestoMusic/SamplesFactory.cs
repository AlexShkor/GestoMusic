using System.IO;

namespace GestoMusic
{
    public class SamplesFactory: ISamplesFactory
    {
        private const string BasePath = @"./Samples/";

        public Sample GetGitare()
        {
            return new Sample(GetFullPath("guitar2cut11.mp3"));
        }

        public Sample GetDrum()
        {
            return new Sample(GetFullPath("hhopen.wav"));
        }

        public Sample GetPlate()
        {
            return new Sample(GetFullPath("ride1.wav"));
        }

        public Sample GetTube()
        {
            return new Sample(GetFullPath("808-clap.wav"));
        }

        public Sample GetMetronom()
        {
            return new Sample(GetFullPath("kalimba.mp3"));
        }

        private string GetFullPath(string path)
        {
            return Path.GetFullPath(string.Format("{0}{1}", BasePath, path));
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