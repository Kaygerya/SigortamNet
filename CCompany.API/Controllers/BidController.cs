using CCompany.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CCompany.API.Controllers
{
    [Route("api/[controller]")]
    public class BidController : Controller
    {
        private readonly CompanyInfoModel _companyInfoModel;

        public BidController(CompanyInfoModel companyInfoModel)
        {
            _companyInfoModel = companyInfoModel;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult GetBidResult([FromBody] BidRequestModel requestModel )
        {
            BidResultModel resultModel = new BidResultModel();
            if (requestModel == null)
            {
                resultModel.Errors.Add("Request Bilgisi Alınamadı");
                return BadRequest(resultModel);
            }
            if (string.IsNullOrWhiteSpace(requestModel.IdentityNumber))
            {
                resultModel.Errors.Add("TC Kimlik No blgisi alınamadı");
                return BadRequest(resultModel);
            }
            if (string.IsNullOrWhiteSpace(requestModel.Plate))
            {
                resultModel.Errors.Add("Plaka blgisi alınamadı");
                return BadRequest(resultModel);
            }

            Random rnd = new Random();
            decimal price =  rnd.Next(100, 500);

         
            resultModel.BidDescription = $"{requestModel.IdentityNumber}  ve {requestModel.Plate} plakalı araç için  {_companyInfoModel.CompanyName}'nin  size özel sigorta teklifi {price} TL'dir ";
            resultModel.BidPrice = price;
            resultModel.CompanyLogo = _companyInfoModel.CompanyLogo;
            resultModel.CompanyName = _companyInfoModel.CompanyName;
            resultModel.Plate = requestModel.Plate;

            return Ok(resultModel);
        }
    }
}
