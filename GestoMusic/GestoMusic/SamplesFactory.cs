using System.IO;

namespace GestoMusic
{
    public class SamplesFactory
    {
        private const string BasePath = @"./Samples/new/";

        public Sample GetGitare()
        {
            return new Sample(GetFullPath("guitar_for_wave.wav"));
        }

        public Sample GetDrum()
        {
            return new Sample(GetFullPath("head.wav"));
        }

        public Sample GetDrum2()
        {
            return new Sample(GetFullPath("left_leg.wav"));
        }

        public Sample GetDrum3()
        {
            return new Sample(GetFullPath("right_leg.wav"));
        }

        public Sample GetPlate()
        {
            return new Sample(GetFullPath("l_hand.wav"));
        }

        public Sample GetPlate1()
        {
            return new Sample(GetFullPath("r_hand.wav"));
        }

        public Sample Wawe()
        {
            return new Sample(GetFullPath("guitar_for_wave.wav"));
        }

        public Sample GetMetronom()
        {
            return new Sample(GetFullPath("guitar_for_wave.wav"));
        }

        private string GetFullPath(string path)
        {
            return Path.GetFullPath(string.Format("{0}{1}", BasePath, path));
        }
    }
}