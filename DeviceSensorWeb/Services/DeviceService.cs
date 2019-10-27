using DeviceSensorWeb.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DeviceSensorWeb.Services
{
    public class DeviceService
    {
        private readonly IMongoCollection<Device> _devices;

        public DeviceService(IDeviceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _devices = database.GetCollection<Device>(settings.DeviceCollectionName);
        }

        public List<Device> Get() =>
            _devices.Find(device => true).ToList();

        public Device Get(string id) =>
            _devices.Find<Device>(device => device.Id == id).FirstOrDefault();

        public Device GetBySerial(string serialNumber) =>
            _devices.Find<Device>(device => device.SerialNumber == serialNumber).FirstOrDefault();

        public Device Create(Device device)
        {
            _devices.InsertOne(device);
            return device;
        }

        public void Update(string id, Device deviceIn) =>
            _devices.ReplaceOne(device => device.Id == id, deviceIn);

        public void Remove(Device deviceIn) =>
            _devices.DeleteOne(device => device.Id == deviceIn.Id);

        public void Remove(string id) => 
            _devices.DeleteOne(device => device.Id == id);
    }
}