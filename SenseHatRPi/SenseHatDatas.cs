using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emmellsoft.IoT.Rpi.SenseHat;

namespace SenseHatRPi
{
    public class SenseHatDatas
    {
        public double? HumidityData { get; set; }
        public double? PressureData { get; set; }
        public double? TemperatureData { get; set; }

    }
}
