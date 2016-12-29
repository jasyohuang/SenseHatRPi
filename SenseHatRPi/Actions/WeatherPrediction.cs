using System;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;
using Emmellsoft.IoT.Rpi.SenseHat.Fonts.SingleColor;

namespace SenseHatRPi.Actions
{
    public class WeatherPrediction : SenseHatRPi
    {
        public WeatherPrediction(ISenseHat senseHat, Action<string> setScreenText)
            : base(senseHat, setScreenText) { }

        private double humidity;
        private double temperature;

        public override void Run()
        {
            //get the humidity data
            SenseHat.Sensors.HumiditySensor.Update();
            humidity = (SenseHat.Sensors.Humidity.Value);

            //get the temperature data
            temperature = (SenseHat.Sensors.Temperature.Value);

            if ((humidity > 60) & (temperature < 28))
            {
                drawrain();
            }
            else if ((humidity < 40) & (temperature > 35)){
                drawsun();
            }
            /*else
            {
                drawcloud();
            }*/

            Sleep(TimeSpan.FromSeconds(2));
            ActionRunner.Run(senseHat => HomeSelector.GetAction(senseHat, SetScreenText));


        }
        private void drawcloud()
        {
            SenseHat.Display.Clear();
            for (int y = 1; y < 7; y++)
            {
                SenseHat.Display.Screen[5, y] = Colors.DarkBlue;
            }
            for (int x = 4; x < 7; x++)
            {
                SenseHat.Display.Screen[x, 2] = Colors.DarkBlue;
            }
            for (int x = 2; x < 8; x++)
            {
                SenseHat.Display.Screen[x, 3] = Colors.DarkBlue;
            }
            for (int x = 1; x < 9; x++)
            {
                SenseHat.Display.Screen[x, 4] = Colors.DarkBlue;
            }
            for (int x = 2; x < 9; x++)
            {
                SenseHat.Display.Screen[x, 5] = Colors.DarkBlue;
            }
            for (int x = 5; x < 7; x++)
            {
                SenseHat.Display.Screen[x, 6] = Colors.DarkBlue;
            }
            SenseHat.Display.Update();
        }
        private void drawsun()
        {
            SenseHat.Display.Clear();
            for (int y = 1; y<8; y++)
            {
                SenseHat.Display.Screen[4, y] = Colors.Yellow;
            }
            for (int x = 1; x<8; x++)
            {
                SenseHat.Display.Screen[x, 4] = Colors.Yellow;
            }
            for (int x = 2; x<7; x++)
            {
                SenseHat.Display.Screen[x, x] = Colors.Yellow;
            } 
            for (int x = 2; x<7; x++)
            {
                SenseHat.Display.Screen[x, 8-x] = Colors.Yellow;
            }
            SenseHat.Display.Update();
        }
        private void drawrain()
        {
            SenseHat.Display.Clear();
            for (int y = 2; y<7; y++)
            {
                SenseHat.Display.Screen[4, y] = Colors.DarkBlue;
            }
            for (int x = 3; x<6; x++)
            {
                SenseHat.Display.Screen[x, 3] = Colors.DarkBlue;
            }
            for (int x = 2; x<7; x++)
            {
                SenseHat.Display.Screen[x, 4] = Colors.DarkBlue;
            }
            for (int x = 2; x < 7; x++)
            {
                SenseHat.Display.Screen[x, 5] = Colors.DarkBlue;
            }
            for (int x = 3; x < 6; x++)
            {
                SenseHat.Display.Screen[x, 6] = Colors.DarkBlue;
            }
            SenseHat.Display.Update();
        }
       
    }
}

