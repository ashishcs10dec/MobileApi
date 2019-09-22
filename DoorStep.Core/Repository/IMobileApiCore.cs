using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStep.Core.Repository
{
    public interface IMobileApiCore
    {
        bool SaveUpdateMobileOtp(MobileOtpDetails mobileOtpDetails);
        bool IsMobileNoVerified(string MobileNo, string Otp);
        bool UpdateSecretToken(string MobileNo, string Token);
        string AddToCartCore(BikeInfoById bikeInfoById);
        string RemovedCart(BikeInfoById bikeInfoById);
        string SaveUserDetials(UserDetails userDetails);
    }
}
