using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeviceSensorApi.Models
{
    public class SensorReadings
    {
        [DataType(DataType.DateTime)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime ReadingDateTime { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public double TemperatureCelcius { get; set; }

        [Range(0, 100)]
        [BsonRepresentation(BsonType.Double)]
        public double HumidityPercentage { get; set; }
        
        [Range(0, double.MaxValue)]
        [BsonRepresentation(BsonType.Double)]
        public double CarbonMonoxideLevel { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string DeviceHealth { get; set; }
    }
}