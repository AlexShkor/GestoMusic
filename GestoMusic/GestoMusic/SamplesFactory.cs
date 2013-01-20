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

        public Sample GetDrumHead()
        {
            return new Sample(GetFullPath("head.wav"));
        }

        public Sample GetDrumLegLeft()
        {
            return new Sample(GetFullPath("left_leg.wav"));
        }

        public Sample GetDrumLegRight()
        {
            return new Sample(GetFullPath("right_leg.wav"));
        }

        public Sample GetDrumHandLeft()
        {
            return new Sample(GetFullPath("l_hand.wav"));
        }

        public Sample GetDrumHandRight()
        {
            return new Sample(GetFullPath("r_hand.wav"));
        }

        public Sample Wawe()
        {
            return new Sample(GetFullPath("guitar_for_wave.wav"));
        }

        public Sample GetMetronom()
        {
            return new Sample(GetFullPath("Beat.wav"));
        }

        public Sample GetGuitarLoop()
        {
            return new Sample(GetFullPath("GuitarLoop.wav"))
                {
                    IsConcurent = true
                };
        }


        public Sample GetPianoLoop()
        {
            return new Sample(GetFullPath("Piano.wav"))
                {
                    IsConcurent = true
                };
        }

        private string GetFullPath(string path)
        {
            return Path.GetFullPath(string.Format("{0}{1}", BasePath, path));
        }
    }
}