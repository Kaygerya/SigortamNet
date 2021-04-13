using SigortamNet.Core.Entities.Base;
using SigortamNet.Core.Entities.Bids;
using System.Collections.Generic;


namespace SigortamNet.Web.Models.Home
{
    public class ListBidModel : EntityModel
    {
        public ListBidModel() {
            Bids = new List<BidResponse>();
        }
        public string IdentityNumber { get; set; }
        public List<BidResponse> Bids { get; set; }
    }
}
