using MongoDB.Bson.Serialization.Attributes;
using SigortamNet.Core.Entities.Base;

namespace SigortamNet.Core.Entities.Bids
{
    [BsonIgnoreExtraElements]
    public class BidRequest : Entity, IEntity
    {
        [BsonElement("in")]
        public string IdentityNumber { get; set; }
        [BsonElement("pt")]
        public string Plate { get; set; }
        [BsonElement("ls")]
        public string LicenseSerial { get; set; }
        [BsonElement("ln")]
        public string LicenseNumber { get; set; }


    }
}