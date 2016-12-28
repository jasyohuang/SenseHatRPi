using System;
using Emmellsoft.IoT.Rpi.SenseHat;
using SenseHatRPi.Actions;

namespace SenseHatRPi
{
    public static class TextViewSelector
    {
        public static bool AlsoUseHdmiOutput = false;

        private static string temperaturecon;
        private static string humiditycon;
        private static double temperatureValue;
        private static double humidityValue;

        private static string texttoshow;
        

        public static SenseHatRPi GetAction(ISenseHat senseHat, Action<string> setScreenText)
        {
            if (!AlsoUseHdmiOutput)
            {
                setScreenText = null;
            }
            
            // Getting the humidity and temperature data
            senseHat.Sensors.HumiditySensor.Update();
            //temperature
            if (senseHat.Sensors.Temperature.HasValue)
            {
                temperatureValue = senseHat.Sensors.Temperature.Value;
            }
            //humidity
            if (senseHat.Sensors.Humidity.HasValue)
            {
                humidityValue = senseHat.Sensors.Humidity.Value;
            }
            //find out the comfortness :v
            //temperature
            if (temperatureValue < 28)
            {
                temperaturecon = "Brrr.. your room is too cold... increase the A/C temperature";
            }
            else if (temperatureValue > 38)
            {
                temperaturecon = "Whoah.. your room is too hot... try to lower the A/C temperature";
            }
            else
            {
                temperaturecon = "Yeah your room is in ideal temperature... Have a nice day :D";
            }
            //humidity
            if (humidityValue < 40)
            {
                humiditycon = "Your room is to dry.. turn on the humidifier pls :')";
            }
            else if (humidityValue > 60)
            {
                humiditycon = "Your room is too humid.. turn on the dehumidifier pls :')";
            }
            else
            {
                humiditycon = "Yeah your room is in ideal humidity... Have a nice day :D";
            }

            texttoshow = temperaturecon + "---" + humiditycon;

            return new TextView(senseHat, texttoshow);
        }
    }
}