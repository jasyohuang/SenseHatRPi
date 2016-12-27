using System;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;
using Emmellsoft.IoT.Rpi.SenseHat.Fonts.SingleColor;


namespace SenseHatRPi.Actions
{
    public class WriteHumidity : SenseHatRPi
    {
        public WriteHumidity(ISenseHat senseHat, Action<string> setScreenText)
            : base(senseHat, setScreenText) { }

        private bool humiditystate;
        public override void Run()
        {
            humiditystate = true;

            var tinyFont = new TinyFont();

            ISenseHatDisplay display = SenseHat.Display;

            while (humiditystate == true)
            {
                SenseHat.Sensors.HumiditySensor.Update();

                if (SenseHat.Sensors.Humidity.HasValue)
                {
                    double humidityValue = SenseHat.Sensors.Humidity.Value;
                    int humidity = (int)Math.Round(humidityValue);
                    string text = humidity.ToString();

                    if (text.Length > 2)
                    {
                        text = "**";
                    }

                    display.Clear();
                    tinyFont.Write(display, text, Colors.Blue);
                    display.Update();

                    SetScreenText?.Invoke($"{humidityValue:0:1}");

                    Sleep(TimeSpan.FromSeconds(2));

                    humiditystate = false;

                }
                else
                {
                    Sleep(TimeSpan.FromSeconds(0.5));
                }
                ActionRunner.Run(senseHat => HomeSelector.GetAction(senseHat, SetScreenText));
            }
        }
    }
}
