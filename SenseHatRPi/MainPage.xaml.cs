using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Emmellsoft.IoT.Rpi.SenseHat;
using Windows.UI.Xaml;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SenseHatRPi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

        public sealed partial class MainPage : Page
        {
            public MainPage()
            {

                this.InitializeComponent();
                ActionRunner.Run(senseHat => HomeSelector.GetAction(senseHat, SetScreenText));




            }
            private async void SetScreenText(string text)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal, () =>
                    {
                        ScreenText.Text = text;
                    });

            }
            private async void MainPage_Loaded(object sender, RoutedEventArgs e)
            {
                //get a reference to SenseHat
                senseHat = await SenseHatFactory.GetSenseHat();
                //initialize the timer
                DispatcherTimer timer = new DispatcherTimer();
                timer.Tick += Timer_Tick;
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Start();
            }

            private async void Timer_Tick(object sender, object e)
            {
                senseHat.Sensors.HumiditySensor.Update();
                senseHat.Sensors.PressureSensor.Update();

                //gather data
                SenseHatDatas data = new SenseHatDatas();
                data.TemperatureData = senseHat.Sensors.Temperature;
                data.HumidityData = senseHat.Sensors.Humidity;
                data.PressureData = senseHat.Sensors.Pressure;

                //send them to the cloud
                //await AzureIoTHub.SendSenseHatDataToCloudAsync("test");



                /*notify UI
                TempText.Text = data.Temperature.ToString();
                TempHumidity.Text = data.Humidity.ToString();
                TempPressure.Text = data.Pressure.ToString();*/
            }

            ISenseHat senseHat;
        }
    }
