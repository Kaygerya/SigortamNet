using MongoDB.Bson.Serialization.Attributes;
using SigortamNet.Core.Entities.Base;

namespace SigortamNet.Core.Entities.Bids
{
    [BsonIgnoreExtraElements]
    public class BidResponse
        :  Entity, IEntity
    {
        [BsonElement("in")]
        public string IdentityNumber { get; set; }
        [BsonElement("cn")]
        public string CompanyName { get; set; }
        [BsonElement("cl")]
        public string CompanyLogo { get; set; }

        [BsonElement("bd")]
        public string BidDescription { get; set; }

        [BsonElement("bp")]
        public decimal BidPrice { get; set; }

        [BsonElement("pt")]
        public string Plate { get; set; }

    }
}
