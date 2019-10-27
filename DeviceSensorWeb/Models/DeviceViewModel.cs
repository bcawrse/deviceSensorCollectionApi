using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceSensorWeb.Models
{
    public class DeviceViewModel
    {
        public IEnumerable<Device> Devices { get; set; }
    }
}
