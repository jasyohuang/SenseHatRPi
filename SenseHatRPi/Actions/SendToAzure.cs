using System;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;
using Emmellsoft.IoT.Rpi.SenseHat.Fonts.SingleColor;
using SenseHatRPi;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;

namespace SenseHatRPi.Actions
{
    public class SendToAzure : SenseHatRPi
    {
        public SendToAzure(ISenseHat senseHat, Action<string> setScreenText)
            : base(senseHat, setScreenText) { }



        public override void Run()
        {

            // notify with blue screen
            SenseHat.Display.Clear();
            SenseHat.Display.Fill(Colors.DeepSkyBlue);
            SenseHat.Display.Update();
            //update the sensor
            SenseHat.Sensors.HumiditySensor.Update();
            SenseHat.Sensors.PressureSensor.Update();
            // get the data to send
            SenseHatDatas data = new SenseHatDatas();
            data.TemperatureData = SenseHat.Sensors.Temperature;
            data.HumidityData = SenseHat.Sensors.Humidity;
            data.PressureData = SenseHat.Sensors.Pressure;
            // send to cloud     
            AzureIoTHub.SendDeviceToCloudMessageAsync(data);
           
           

            ActionRunner.Run(senseHat => HomeSelector.GetAction(senseHat, SetScreenText));
        }

    }
    }

