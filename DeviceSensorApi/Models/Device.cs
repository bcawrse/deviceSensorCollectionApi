using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeviceSensorApi.Models
{
    public class Device
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string SerialNumber { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime RegistrationDate { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string FirmwareVersion { get; set; }

        public IList<SensorReading> SensorReadings { get; }
    }
}