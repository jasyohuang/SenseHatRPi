using System;
using Emmellsoft.IoT.Rpi.SenseHat;
using SenseHatRPi.Actions;

namespace SenseHatRPi
{
    public static class HumiditySelector
    {
        public static bool AlsoUseHdmiOutput = false;

        public static SenseHatRPi GetAction(ISenseHat senseHat, Action<string> setScreenText)
        {
            if (!AlsoUseHdmiOutput)
            {
                setScreenText = null;
            }
            return new WriteHumidity(senseHat, setScreenText);
        }
    }
}
