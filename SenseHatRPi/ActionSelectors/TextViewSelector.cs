using System;
using Emmellsoft.IoT.Rpi.SenseHat;
using SenseHatRPi.Actions;

namespace SenseHatRPi
{
    public static class TextViewSelector
    {
        public static bool AlsoUseHdmiOutput = false;
        public static string MessageToShow;


        public static SenseHatRPi GetAction(ISenseHat senseHat, Action<string> setScreenText)
        {
            if (!AlsoUseHdmiOutput)
            {
                setScreenText = null;
            }
            //get the string from azure
            //MessageToShow =  that
            MessageToShow = "Test";
            return new TextView(senseHat, MessageToShow);
        }
    }
}