using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace DeviceSensorApi.Models
{
    public class RegisterDevice
    {
        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public string FirmwareVersion { get; set; }
    }
}
