using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emmellsoft.IoT.Rpi.SenseHat;
using Windows.UI;

namespace SenseHatRPi.Actions
{
    public class Home : SenseHatRPi
    {
        public Home(ISenseHat senseHat, Action<string> setScreenText)
            : base(senseHat, setScreenText) { }

        private bool selectingstate;

        public string newMessage;
        public string oldMessage;

        public override void Run()
        {
            selectingstate = true;

            ISenseHatDisplay display = SenseHat.Display;
            ISenseHatJoystick joystick = SenseHat.Joystick;

            SenseHat.Display.Clear();
            SenseHat.Display.Fill(Colors.Black);
            SenseHat.Display.Update();

            //get message from azure
            //if(newMessage != oldMessage){
            //oldMessage = newMessage;
            //ActionRunner.Run(senseHat => TextViewSelector.GetAction(senseHat, SetScreenText));
            //selectingstate = false;
            //}



            while (selectingstate == true)
            {
                if (SenseHat.Joystick.Update())
                {
                    if (SenseHat.Joystick.LeftKey == KeyState.Pressed)
                    {
                        ActionRunner.Run(senseHat => TemperatureSelector.GetAction(senseHat, SetScreenText));
                        selectingstate = false;
                    }
                    else if (SenseHat.Joystick.RightKey == KeyState.Pressed)
                    {
                        ActionRunner.Run(senseHat => HumiditySelector.GetAction(senseHat, SetScreenText));
                        selectingstate = false;
                    }
                    else if (SenseHat.Joystick.DownKey == KeyState.Pressed)
                    {
                        ActionRunner.Run(senseHat => PressureSelector.GetAction(senseHat, SetScreenText));
                        selectingstate = false;
                    }
                    else if (SenseHat.Joystick.EnterKey == KeyState.Pressed)
                    {
                        ActionRunner.Run(senseHat => TextViewSelector.GetAction(senseHat, SetScreenText));
                        selectingstate = false;
                    }
                }
            }

        }
    }
}
