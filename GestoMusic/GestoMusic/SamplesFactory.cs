using System.IO;

namespace GestoMusic
{
    public class SamplesFactory: ISamplesFactory
    {
        private const string BasePath = @"./Samples/";

        public Sample GetGitare()
        {
            return new Sample(GetFullPath("GC001.wav"));
        }

        public Sample GetDrum()
        {
            return new Sample(GetFullPath("drum.wav"));
        }

        public Sample GetPlate()
        {
            return new Sample(GetFullPath("GPA007.wav"));
        }

        public Sample GetTube()
        {
            return new Sample(GetFullPath("GPA005.wav"));
        }

        public Sample GetMusic()
        {
            return new Sample(GetFullPath("Kalimba.mp3"));
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