using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorStep.Api.Models
{
    public class LoginRequest
    {
        public string MobileNo { get; set; }
        public string OTP { get; set; }
    }
}