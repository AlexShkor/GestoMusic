using System;
using System.Threading;

namespace GestoMusic
{
    public class Program
    {
        public static void Main()
        {
            var gesturesObserver = new GesturesObserver();
            var samplesFactory = new SamplesFactory();
            var gitare = samplesFactory.GetGitare();
            var plate = samplesFactory.GetGitare();
            var tube = samplesFactory.GetGitare();
            var drum = samplesFactory.GetGitare();
            gesturesObserver.TrackGesture(
                GestureTypeEnum.RightHand, () =>
                    {
                        gitare.Play();
                        Console.WriteLine("{0} recognized. {1}",GestureTypeEnum.RightHand, gitare);
                    }
                );
            gesturesObserver.TrackGesture(
                GestureTypeEnum.LeftHand, () =>
                    {
                        plate.Play();
                        Console.WriteLine("{0} recognized. {1}", GestureTypeEnum.LeftHand, plate);
                    }
                );
            gesturesObserver.TrackGesture(
                GestureTypeEnum.RighLeg, () =>
                    {
                        tube.Play();
                        Console.WriteLine("{0} recognized. {1}", GestureTypeEnum.RighLeg, tube);
                    }
                );
            gesturesObserver.TrackGesture(
                GestureTypeEnum.LeftLeg, () =>
                    {
                        drum.Play();
                        Console.WriteLine("{0} recognized. {1}", GestureTypeEnum.LeftLeg, drum);
                    }
                );
            while (true)
            {
                Thread.Sleep(100);
                gesturesObserver.Update();
            }
        }
    }

    public enum GestureTypeEnum
    {
        RightHand,
        LeftHand,
        RighLeg,
        LeftLeg
    }
}