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
            }
    
        }
    
