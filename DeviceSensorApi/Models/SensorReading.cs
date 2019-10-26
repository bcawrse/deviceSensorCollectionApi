using System;

namespace DeviceSensorApi.Models
{
  public class SensorReading {
    
    public DateTime ReadingDateTime { get; set; }
    
    public double TemperatureCelcius { get; set; }
    
    public double HumidityPercentage { get; set; }
    
    public double CarbonMonoxideLevel { get; set; }
    
    public string DeviceHealth { get; set; }
  }
}