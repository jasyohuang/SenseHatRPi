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

        
        static ISenseHat senseHat;
        static DeviceClient deviceClient;
        static string iotHubUri = "start.azure-devices.net";
        static string deviceKey = "COjphwqpr6xpuLKoc/JOrty0338x3Cy14jN2ruStTgs=";

        public override void Run()
        {
            // notify with blue screen
            SenseHat.Display.Clear();
            SenseHat.Display.Fill(Colors.DeepSkyBlue);
            SenseHat.Display.Update();

            /*//gather data
            SenseHatDatas data = new SenseHatDatas();
            data.TemperatureData = senseHat.Sensors.Temperature;
            data.HumidityData = senseHat.Sensors.Humidity;
            data.PressureData = senseHat.Sensors.Pressure;

            GoToAzure(data);*/

            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("myFirstDevice", deviceKey), TransportType.Amqp);

            SendDeviceToCloudMessages();


            ActionRunner.Run(senseHat => HomeSelector.GetAction(senseHat, SetScreenText));
         }
        private static async void SendDeviceToCloudMessages()
        {
            double temperature = (senseHat.Sensors.Temperature.Value);
            double humidity = (senseHat.Sensors.Humidity.Value);
            double pressure = (senseHat.Sensors.Pressure.Value);

            var telemetryDataPoint = new
                {
                    deviceId = "myFirstDevice",
                    temperature, humidity, pressure
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);
               
            
        }
    }
    }

