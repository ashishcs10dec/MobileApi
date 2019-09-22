using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStepModel.Entity
{
    public class UserDetails
    {
        public string FullName { get; set; }
        public string UserEmailId { get; set; }
        public string MobileNo { get; set; }
        public string SecretToken { get; set; }
        public string Address { get; set; }
        public int AddressType { get; set; }
        public string LandMark { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string AMPM { get; set; }
        public int City { get; set; }
        public string Pincode { get; set; }

        public List<BikecartlistIds> BikecartlistIds { get; set; }
    }
}
