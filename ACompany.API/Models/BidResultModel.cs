using System.Collections.Generic;

namespace ACompany.API.Models
{
    public class BidResultModel
    {
        public BidResultModel()
        {
            Errors = new List<string>();
        }
        public string IdentityNumber { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogo { get; set; }

        public string BidDescription { get; set; }

        public decimal BidPrice { get; set; }

        public string Plate { get; set; }

        public List<string> Errors { get; set; }
        public bool IsScuccess
        {
            get
            {
                return Errors.Count == 0;
            }
        }
    }
}
