using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SigortamNet.Core.Entities.Base
{
    public class Entity : EntityModel
    {
        public Entity()
        {

        }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

  
        [BsonElement("cd")]
        public DateTime CreatedDate { get { return DateTime.Now; } }

 

    }
}
