using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeviceSensorApi.Models
{
    public class SensorReading
    {

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime ReadingDateTime { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public double TemperatureCelcius { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public double HumidityPercentage { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public double CarbonMonoxideLevel { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string DeviceHealth { get; set; }
    }
}