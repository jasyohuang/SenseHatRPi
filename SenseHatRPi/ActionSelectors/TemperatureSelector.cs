using System;
using Emmellsoft.IoT.Rpi.SenseHat;
using SenseHatRPi.Actions;

namespace SenseHatRPi
{
    public static class TemperatureSelector
    {
        public static bool AlsoUseHdmiOutput = false;

        public static SenseHatRPi GetAction(ISenseHat senseHat, Action<string> setScreenText)
        {
            if (!AlsoUseHdmiOutput)
            {
                setScreenText = null;
            }
            return new WriteTemperature(senseHat, setScreenText);
        }
    }
}
