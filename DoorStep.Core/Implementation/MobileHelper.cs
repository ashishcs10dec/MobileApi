using DoorStep.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStep.Core.Implementation
{
    public class MobileHelper : IMobileHelper
    {
        private Random _random = new Random();

        public string GenerateOtp()
        {
            return _random.Next(0, 9999).ToString("D4");
        }
    }
}
