using DeviceSensorApi.Models;
using DeviceSensorApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DeviceSensorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceService _deviceService;

        public DeviceController(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpPost]
        public ActionResult<Device> RegisterDevice(RegisterDevice newDevice)
        {

            var existingDevice = _deviceService.GetBySerial(newDevice.SerialNumber);

            if (existingDevice != null)
            {
                return BadRequest();
            }

            var device = new Device()
            {
                SerialNumber = newDevice.SerialNumber,
                FirmwareVersion = newDevice.FirmwareVersion,
                RegistrationDate = System.DateTime.Now
            };

            _deviceService.Create(device);

            return CreatedAtRoute("GetDevice", new { id = device.Id.ToString() }, device);
        }

        [HttpGet]
        public ActionResult<List<Device>> Get() =>
            _deviceService.Get();

        [HttpGet("{id:length(24)}", Name = "GetDevice")]
        public ActionResult<Device> Get(string id)
        {
            var device = _deviceService.Get(id);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }

        [HttpGet("{serialNumber}")]
        public ActionResult<Device> Get(string serialNumber)
        {
            var device = _deviceService.GetBySerial(serialNumber);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }

        //[HttpPost]
        //public ActionResult<Device> Create(Device device)
        //{
        //    _deviceService.Create(device);

        //    return CreatedAtRoute("GetDevice", new { id = device.Id.ToString() }, device);
        //}

        //[HttpPut("{id:length(24)}")]
        //public IActionResult Update(string id, Device deviceIn)
        //{
        //    var device = _deviceService.Get(id);

        //    if (device == null)
        //    {
        //        return NotFound();
        //    }

        //    _deviceService.Update(id, deviceIn);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public IActionResult Delete(string id)
        //{
        //    var device = _deviceService.Get(id);

        //    if (device == null)
        //    {
        //        return NotFound();
        //    }

        //    _deviceService.Remove(device.Id);

        //    return NoContent();
        //}
    }
}