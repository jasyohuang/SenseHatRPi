using System;
using System.Threading;
using Emmellsoft.IoT.Rpi.SenseHat;


namespace SenseHatRPi
{
    public abstract class SenseHatRPi
    {
        private readonly ManualResetEventSlim _waitEvent = new ManualResetEventSlim(false);

        protected SenseHatRPi(ISenseHat senseHat, Action<string> setScreenText = null)
        {
            SetScreenText = setScreenText;
            SenseHat = senseHat;
        }

        protected Action<string> SetScreenText { get; }
        protected ISenseHat SenseHat { get; }
        protected ISenseHatJoystick JoyStick { get; }
        public abstract void Run();
        protected void Sleep(TimeSpan duration)
        {
            _waitEvent.Wait(duration);
        }
    }
}
