using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStep.Infrastructure.Repository
{
    public interface IMobileApi
    {
        bool SaveUpdateMobileNoWithOtp(MobileOtpDetails mobileOtpDetails);
        bool IsMobileNoVerified(string MobileNo, string Otp);
        bool UpdateSecretToken(string MobileNo, string Token);
        string AddCart(BikeInfoById bikeInfoById);
        string RemoveCart(BikeInfoById bikeInfoById);
        string SaveUserDetails(UserDetails userDetails);
    }
}
