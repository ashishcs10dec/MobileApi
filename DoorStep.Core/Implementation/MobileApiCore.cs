using DoorStep.Core.Repository;
using DoorStep.Infrastructure.Repository;
using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStep.Core.Implementation
{
    public class MobileApiCore : IMobileApiCore
    {
        private IMobileApi _iMobileApi;
        private IMobileHelper _iMobileHelper;
        public MobileApiCore(IMobileApi iMobileApi, IMobileHelper iMobileHelper)
        {
            _iMobileApi = iMobileApi;
            _iMobileHelper = iMobileHelper;
        }

        public bool SaveUpdateMobileOtp(MobileOtpDetails mobileOtpDetails)
        {
            mobileOtpDetails.OTP = _iMobileHelper.GenerateOtp();
            return _iMobileApi.SaveUpdateMobileNoWithOtp(mobileOtpDetails);
        }

        public bool IsMobileNoVerified(string MobileNo, string Otp)
        {
            return _iMobileApi.IsMobileNoVerified(MobileNo, Otp);
        }

        public bool UpdateSecretToken(string MobileNo, string Token)
        {
            return _iMobileApi.UpdateSecretToken(MobileNo, Token);
        }

        public string AddToCartCore(BikeInfoById bikeInfoById)
        {
            return _iMobileApi.AddCart(bikeInfoById);
        }

        public string RemovedCart(BikeInfoById bikeInfoById)
        {
            return _iMobileApi.RemoveCart(bikeInfoById);
        }

        public string SaveUserDetials(UserDetails userDetails)
        {
            return _iMobileApi.SaveUserDetails(userDetails);
        }
    }
}
