using System;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;
using Emmellsoft.IoT.Rpi.SenseHat.Fonts.SingleColor;

namespace SenseHatRPi
{
    public class WriteTemperature : SenseHatRPi
    {
        public WriteTemperature(ISenseHat senseHat, Action<string> setScreenText)
            : base(senseHat, setScreenText) { }

        private enum TemperatureUnit
        {
            Celcius, Fahrenheit, Kelvin
        }

        private bool temperaturestate;
        public override void Run()
        {
            temperaturestate = true;

            var tinyFont = new TinyFont();

            ISenseHatDisplay display = SenseHat.Display;

            TemperatureUnit unit = TemperatureUnit.Celcius; //we want celcius :v

            string unitText = GetUnitText(unit);

            while (temperaturestate == true)
            {
                SenseHat.Sensors.HumiditySensor.Update();

                if (SenseHat.Sensors.Temperature.HasValue)
                {
                    double temperatureValue = ConvertTemperatureValue(unit, SenseHat.Sensors.Temperature.Value);

                    int temperature = (int)Math.Round(temperatureValue);
                    string text = temperature.ToString();

                    if (text.Length > 2)
                    {
                        // too long to display :'v
                        text = "**";
                    }

                    display.Clear();
                    tinyFont.Write(display, text, Colors.White);
                    display.Update();

                    SetScreenText?.Invoke($"{temperatureValue:0:1} {unitText}");

                    Sleep(TimeSpan.FromSeconds(2));

                    temperaturestate = false;
                }
                else
                {
                    //rapid update until value is available
                    Sleep(TimeSpan.FromSeconds(0.5));
                }
                ActionRunner.Run(senseHat => HomeSelector.GetAction(senseHat, SetScreenText));
            }
        }

        private static double ConvertTemperatureValue(TemperatureUnit unit, double temperatureInCelcius)
        {
            switch (unit)
            {
                case TemperatureUnit.Celcius:
                    return temperatureInCelcius;

                case TemperatureUnit.Fahrenheit:
                    return temperatureInCelcius * 9 / 5 + 32;

                case TemperatureUnit.Kelvin:
                    return temperatureInCelcius + 273;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static string GetUnitText(TemperatureUnit unit)
        {
            switch (unit)
            {
                case TemperatureUnit.Celcius:
                    return "\u00B0C"; //\u00B0 ==> degree symbol

                case TemperatureUnit.Fahrenheit:
                    return "\u00B0F";

                case TemperatureUnit.Kelvin:
                    return "K";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
