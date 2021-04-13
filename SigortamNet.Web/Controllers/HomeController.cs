using Microsoft.AspNetCore.Mvc;
using SigortamNet.Core.Entities.Bids;
using SigortamNet.DAL.Settings;
using SigortamNet.Services.Abstract;
using SigortamNet.Web.Models.Home;
using SigortamNet.Web.Validators;
using System.Collections.Generic;
using System.Linq;

namespace SigortamNet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBidService _bidService;
        private readonly DataAccessSettings _dataAccessSettings;
        public HomeController(IBidService bidService, DataAccessSettings dataAccessSettings)
        {
            _bidService = bidService;
            _dataAccessSettings = dataAccessSettings;
        }

        [HttpGet]
        public IActionResult Index()
        {
            BidRequest model = new BidRequest();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(BidRequest bidRequest)
        {
            var bidRequestValidator = new BidRequestValidator();
            var validationResult = bidRequestValidator.Validate(bidRequest);
            if (validationResult.Errors.Any())
            {
                bidRequest.Errors = validationResult.Errors.Select(k => k.ErrorMessage).ToList();
                return View(bidRequest);
            }

            var bidListFromCompanies = _bidService.ListBidsFromCompany(_dataAccessSettings.CompanyUrls, bidRequest);
            if (bidListFromCompanies == null)
            {
                bidRequest.Errors.Add("Uygun teklif bulunamadı");
                return View(bidRequest);
            }

            return ListCompanyBids(bidListFromCompanies);
        }


        public IActionResult ListCompanyBids(List<BidResponse> bidResponses)
        {
            if (bidResponses == null)
                return View(new List<BidResponse>());


            return View("~/Views/Home/ListCompanyBids.cshtml", bidResponses);
        }

        public IActionResult GetSingleChar()
        {
            string text = "geeksforgeeks";

            string s = text.ToCharArray().GroupBy(k => k).FirstOrDefault(w => w.Count() == 1).Key.ToString();

            int limit = 100;
            List<string> packets = new List<string>();
            for (int i = 0; i < (text.Length / limit)+1 ; i++)
            {
                string subText = new string( text.Skip(i * limit).Take(limit).ToArray());
            }

            return Content(s);
        }

        [HttpGet]
        public IActionResult ListBids()
        {
            ListBidModel model = new ListBidModel();
            return View(model);

        }
        [HttpPost]
        public IActionResult ListBids(string identityNumber)
        {
            ListBidModel model = new ListBidModel();
            model.IdentityNumber = identityNumber;
            model.Bids = _bidService.ListBidsByIdentityNumber(identityNumber);
            return View(model);

        }

        public IActionResult GetLicenseInfoBtIdentityNumberAndPlate(string identityNumber, string plate)
        {
           var bidRequest =  _bidService.GetLicenseInfoBtIdentityNumberAndPlate(identityNumber, plate);
            return  Json(bidRequest);
        }

        public IActionResult Error()
        {

            return View();
        }


    }
}
