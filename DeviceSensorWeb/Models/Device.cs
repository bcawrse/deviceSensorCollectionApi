using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeviceSensorWeb.Models
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

#pragma warning disable CA2227 // Collection properties should be read only
        public IEnumerable<SensorReadings> SensorReadings { get; set; } = new List<SensorReadings>();
#pragma warning restore CA2227 // Collection properties should be read only
    }
}