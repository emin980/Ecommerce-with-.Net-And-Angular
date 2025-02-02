﻿using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Application.Responses
{
    public class BaseResponse
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}
