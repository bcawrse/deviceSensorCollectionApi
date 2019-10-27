using DeviceSensorApi.Models;
using DeviceSensorApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceSensorApi.Services;

namespace DeviceSensorApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceService _deviceService;
        private readonly IUserService _userService;

        public DeviceController(DeviceService deviceService, IUserService userService)
        {
            _deviceService = deviceService;
            _userService = userService;
        }

        /// <summary>
        /// Register a new device.
        /// </summary>
        /// <param name="registerDevice">Device to register.</param>
        /// <returns>Status and route of newly created device.</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("RegisterDevice", Name = "RegisterDevice")]
        public ActionResult<Device> RegisterDevice(RegisterDevice registerDevice)
        {
            if (registerDevice == null || string.IsNullOrWhiteSpace(registerDevice.FirmwareVersion) || string.IsNullOrWhiteSpace(registerDevice.SerialNumber))
            {
                return BadRequest(new BadRequestObjectResult(ModelState) { Value = "Must provide device SerialNumber and FirmwareVersion" });
            }

            var existingDevice = _deviceService.GetBySerial(registerDevice.SerialNumber);

            if (existingDevice != null)
            {
                return BadRequest(new BadRequestObjectResult(ModelState) { Value = "Device already registered." });
            }

            var device = new Device()
            {
                SerialNumber = registerDevice.SerialNumber,
                FirmwareVersion = registerDevice.FirmwareVersion,
                RegistrationDate = System.DateTime.Now
            };

            _deviceService.Create(device);

            return CreatedAtRoute("GetDevice", new { id = device.Id.ToString() }, device);
        }

        /// <summary>
        /// Get collection of all existing devices in the system.
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet]
        public ActionResult<List<Device>> Get() =>
            _deviceService.Get();

        /// <summary>
        /// Get a device by it's Id.
        /// </summary>
        /// <param name="id">Id of device.</param>
        /// <returns></returns>
        [Produces("application/json")]
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

        /// <summary>
        /// Retrieve a device from it's SerialNumber.
        /// </summary>
        /// <param name="serialNumber">Serial Number to retrieve device with.</param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("GetBySerial/{serialNumber}", Name = "GetDeviceBySerial")]
        public ActionResult<Device> GetBySerial(string serialNumber)
        {
            var device = _deviceService.GetBySerial(serialNumber);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }

        /// <summary>
        /// Patch / Update an existing device
        /// </summary>
        /// <param name="id">Id of device to patch.</param>
        /// <param name="deviceIn">Device details for patch.</param>
        /// <returns></returns>
        [HttpPatch("{id:length(24)}")]
        public IActionResult Update(string id, Device deviceIn)
        {
            if (deviceIn == null)
            {
                return BadRequest();
            }

            var device = _deviceService.Get(id);

            if (device == null)
            {
                return NotFound();
            }

            var existingDevice = _deviceService.Get(deviceIn.SerialNumber);

            if (existingDevice != null)
            {
                return BadRequest(new BadRequestObjectResult(ModelState) { Value = "New Serial Number is already registered." });
            }

            _deviceService.Update(id, deviceIn);

            return NoContent();
        }

        /// <summary>
        /// Delete a device.
        /// </summary>
        /// <param name="id">Id of device to remove.</param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var device = _deviceService.Get(id);

            if (device == null)
            {
                return NotFound();
            }

            _deviceService.Remove(device.Id);

            return NoContent();
        }

        /// <summary>
        /// Add sensor reading to a Device by it's Serial Number.
        /// </summary>
        /// <param name="serialNumber">Serial Number of device to add sensor reading to.</param>
        /// <param name="sensorReading">Sensor readings recorded by device.</param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("AddSensorReading")]
        public IActionResult AddSensorReading(string serialNumber, SensorReadings sensorReading)
        {
            var device = _deviceService.GetBySerial(serialNumber);

            if (device == null)
            {
                return NotFound();
            }

            device.SensorReadings = device.SensorReadings.Concat(new List<SensorReadings> { sensorReading });

            _deviceService.Update(device.Id, device);

            return CreatedAtRoute("GetDevice", new { id = device.Id.ToString() }, device);
        }

        /// <summary>
        /// Bulk add sensor readings to a Device by it's Serial Number.
        /// </summary>
        /// <param name="serialNumber">Serial Number of device to add sensor readings to.</param>
        /// <param name="sensorReadings">Group of sensor readings recorded by device.</param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("BulkAddSensorReadings")]
        public IActionResult AddSensorReadings(string serialNumber, IEnumerable<SensorReadings> sensorReadings)
        {
            var device = _deviceService.GetBySerial(serialNumber);

            if (device == null)
            {
                return NotFound();
            }

            device.SensorReadings = device.SensorReadings.ToList().Union(sensorReadings);

            _deviceService.Update(device.Id, device);

            return CreatedAtRoute("GetDevice", new { id = device.Id.ToString() }, device);
        }
    }
}