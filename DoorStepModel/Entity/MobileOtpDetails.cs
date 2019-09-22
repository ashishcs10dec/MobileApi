using System;

namespace DoorStepModel.Entity
{
    public class MobileOtpDetails
    {
        public string MobileNo { get; set; }
        public string OTP { get; set; }
        public string SecretToken { get; set; }
        public bool IsActive { get; set; }
        public int OTPSendCount { get; set; }
    }
}
