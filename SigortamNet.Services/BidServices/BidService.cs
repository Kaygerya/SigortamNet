using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SigortamNet.Core.Entities.Bids;
using SigortamNet.DAL.Abstract;
using SigortamNet.Services.Abstract;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace SigortamNet.Services.BidServices
{
    public class BidService : IBidService
    {
        private readonly IRepository<BidRequest> _bidRequestRepository;
        private readonly IRepository<BidResponse> _bidResponseRepository;
        private readonly ILogger _logger;

        public BidService(IRepository<BidRequest> bidRequestRepository,
            IRepository<BidResponse> bidResponseRepository,
           ILoggerFactory logger)
        {
            _bidRequestRepository = bidRequestRepository;
            _bidResponseRepository = bidResponseRepository;
            _logger = logger.CreateLogger("BidService");
        }

        public List<BidResponse> ListBidsFromCompany(List<string> companyUrlList, BidRequest request)
        {
            ConcurrentStack<BidResponse> conCurrentStack = new ConcurrentStack<BidResponse>();
            List<Task> taskList = new List<Task>();
            foreach (var url in companyUrlList)
            {
                taskList.Add(Task.Factory.StartNew(() => Post(url, request, conCurrentStack)));
            }
            Task.WaitAll(taskList.ToArray());

            var responseBidList = conCurrentStack.ToList();
            InsertBidRequest(request);
            foreach (var responseBid in responseBidList)
            {
                if (responseBid != null)
                {
                    responseBid.IdentityNumber = request.IdentityNumber;
                    responseBid.Plate = request.Plate;
                    InsertBidResponse(responseBid);
                }
            }
            return responseBidList;
        }

        public List<BidResponse> ListBidsByIdentityNumber(string identityNumber)
        {
            if (string.IsNullOrWhiteSpace(identityNumber))
                return new List<BidResponse>();

            List<BidResponse> bidResponses = (_bidResponseRepository.Query(k => k.IdentityNumber.Contains(identityNumber))).ToList();
            return bidResponses;
        }

        public BidRequest GetLicenseInfoBtIdentityNumberAndPlate(string identityNumber, string plate)
        {
            if (string.IsNullOrWhiteSpace(identityNumber) || string.IsNullOrWhiteSpace(plate))
                    return new BidRequest();

            var bidRequest = _bidRequestRepository.Query(k => k.IdentityNumber == identityNumber && k.Plate == plate.Replace(" ", "").ToUpper(new System.Globalization.CultureInfo("en-US"))).FirstOrDefault();
            if (bidRequest == null)
                return new BidRequest();

            return bidRequest;

        }

        private void Post(string url, BidRequest request, ConcurrentStack<BidResponse> conCurrentStack)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(request);

                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (result != null)
                        conCurrentStack.Push(JsonConvert.DeserializeObject<BidResponse>(result));
                }
            }
            catch
            {
                _logger.LogError($"{url} adresine erişilemedi");
            }
        }

        private void InsertBidRequest(BidRequest bidRequest)
        {
            if (bidRequest == null)
                throw new InvalidDataException();

            bidRequest.Plate = bidRequest.Plate.Replace(" ", "").ToUpper(new System.Globalization.CultureInfo("en-US"));
            bidRequest.LicenseSerial = bidRequest.LicenseSerial.ToUpper(new System.Globalization.CultureInfo("en-US"));

            var item = _bidRequestRepository.Query(k => k.IdentityNumber == bidRequest.IdentityNumber && k.Plate == bidRequest.Plate).FirstOrDefault();
            if (item == null)
                _bidRequestRepository.Insert(bidRequest);
            else
            {
                bidRequest.Id = item.Id;
                _bidRequestRepository.Update(bidRequest);
            }

        }

        private void InsertBidResponse(BidResponse bidResponse)
        {
            if (bidResponse == null)
                throw new InvalidDataException();

            bidResponse.Plate = bidResponse.Plate.Replace(" ","").ToUpper(new System.Globalization.CultureInfo("en-US"));

            var item = _bidResponseRepository.Query(k => k.IdentityNumber == bidResponse.IdentityNumber && k.Plate == bidResponse.Plate && k.CompanyName == bidResponse.CompanyName).FirstOrDefault();
            if (item == null)
                _bidResponseRepository.Insert(bidResponse);
            else
            {
                bidResponse.Id = item.Id;
                _bidResponseRepository.Update(bidResponse);
            }

        }

    }
}
