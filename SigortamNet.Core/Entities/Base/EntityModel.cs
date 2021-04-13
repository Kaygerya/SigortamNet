using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace SigortamNet.Core.Entities.Base
{
    [BsonIgnoreExtraElements]
    public class EntityModel
    {
        public EntityModel()
        {
            Errors = new List<string>();
        }
        [BsonIgnore]
        public List<string> Errors { get; set; }

        [BsonIgnore]
        public bool IsSuccess { get { return Errors.Count == 0; } }
        [BsonIgnore]
        public string SuccessMessage { get; set; }

    }
}
