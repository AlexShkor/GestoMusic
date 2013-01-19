using WMPLib;

namespace GestoMusic
{
    public class Sample
    {
        private readonly string _sample;

        public Sample(string sample)
        {
            _sample = sample;
        }

        public void Play(double rate)
        {
            var player = new WindowsMediaPlayer();
            player.URL = _sample;
            player.settings.rate = rate;
        }
    }
}