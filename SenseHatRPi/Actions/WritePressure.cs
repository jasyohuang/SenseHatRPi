using System;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;
using Emmellsoft.IoT.Rpi.SenseHat.Fonts.SingleColor;

namespace SenseHatRPi.Actions
{
    public class WritePressure : SenseHatRPi
    {
        public WritePressure(ISenseHat senseHat, Action<string> setScreenText)
            : base(senseHat, setScreenText) { }

        private bool pressurestate;

        public override void Run()
        {
            pressurestate = true;

            var tinyFont = new TinyFont();

            ISenseHatDisplay display = SenseHat.Display;

            while (pressurestate == true)
            {
                SenseHat.Sensors.PressureSensor.Update();

                if (SenseHat.Sensors.Pressure.HasValue)
                {
                    double pressurevalue = SenseHat.Sensors.Pressure.Value;

                    int pressure = (int)Math.Round(pressurevalue / 1000);
                    string text = pressure.ToString();

                    if (text.Length > 2)
                    {
                        // too long to display :'v
                        text = "**";
                    }

                    display.Clear();
                    tinyFont.Write(display, text, Colors.Green);
                    display.Update();

                    SetScreenText?.Invoke($"{pressure:0:1}");

                    Sleep(TimeSpan.FromSeconds(2));

                    pressurestate = false;
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
