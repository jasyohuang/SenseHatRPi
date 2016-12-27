using System;
using System.Threading.Tasks;
using Emmellsoft.IoT.Rpi.SenseHat;

namespace SenseHatRPi
{
    public static class ActionRunner
    {
        public static void Run(Func<ISenseHat, SenseHatRPi> createAction)
        {
            Task.Run(async () =>
            {
                ISenseHat senseHat = await SenseHatFactory.GetSenseHat().ConfigureAwait(false);

                SenseHatRPi action = createAction(senseHat);

                action.Run();
            }).ConfigureAwait(false);
        }
    }
}