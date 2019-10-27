namespace DeviceSensorWeb.Models
{
    public class DeviceDatabaseSettings : IDeviceDatabaseSettings
    {
        public string DeviceCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDeviceDatabaseSettings
    {
        string DeviceCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}