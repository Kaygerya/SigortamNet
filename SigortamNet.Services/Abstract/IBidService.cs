
using SigortamNet.Core.Entities.Bids;
using System.Collections.Generic;

namespace SigortamNet.Services.Abstract
{
    public interface IBidService
    {
        List<BidResponse> ListBidsFromCompany(List<string> companyUrlList, BidRequest request);

        List<BidResponse> ListBidsByIdentityNumber(string identityNumber);

        BidRequest GetLicenseInfoBtIdentityNumberAndPlate(string identityNumber, string plate);
    }
}
