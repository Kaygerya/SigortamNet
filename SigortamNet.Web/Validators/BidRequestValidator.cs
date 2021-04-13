using FluentValidation;
using SigortamNet.Core.Constants;
using SigortamNet.Core.Entities.Bids;

namespace SigortamNet.Web.Validators
{
  
    public class BidRequestValidator : AbstractValidator<BidRequest>
    {
        public BidRequestValidator()
        {
            RuleFor(x => x.IdentityNumber).NotNull().WithMessage("TC Kimlik No boş bırakılamaz");
            RuleFor(x => x.IdentityNumber).Matches("^\\d{"+ BiddingConstants.IdentityNumberCount+"}$").WithMessage($"TC Kimlik No geçersiz. Kimlik numarası { BiddingConstants.IdentityNumberCount} karakter olmalıdır");
            RuleFor(x => x.Plate).NotNull().WithMessage("Plaka boş bırakılamaz");
            RuleFor(x => x.Plate).Matches("(?<İl>[0-8][0-9])(?<A>[a-zA-Z]{1,3})(?<N>[0-9]{2,5})").WithMessage("Plaka geçerli değil. Boşluk veya geçersiz karakter kullanmayınız");
            RuleFor(x => x.LicenseSerial).NotNull().WithMessage("Ruhsat Seri Kodu boş bırakılamaz");
            RuleFor(x => x.LicenseNumber).NotNull().WithMessage("Ruhsat Seri No boş bırakılamaz");
            RuleFor(x => x.LicenseNumber).Matches("^[0-9]*$").WithMessage("Ruhsat seri numarası sadece rakamlardan oluşmalıdır");
            
        }
    }
}
