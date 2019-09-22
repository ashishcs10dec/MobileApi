using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DoorStepModel.Entity
{
    public class CoreBikeInformation
    {
        public int BikeId { get; set; }
        public string BikeBrand { get; set; }
        public string BikeModel { get; set; }
        public int ? BikeRegistrationYear { get; set; }
        public string BikeKmDriven { get; set; }
        public string BikeDescription { get; set; }
        public decimal? BikePrice { get; set; }
        public DateTime? BikePreferDateTimeIn { get; set; }
        public DateTime? BikePreferDateTimeOut { get; set; }
        public string BikeOwnerName { get; set; }
        public string BikeResonForSale { get; set; }
        public string BikeEngineNo { get; set; }
        public string BikeChasisNo { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public List<CoreImagedetails> Imagepath { get; set; }
    }

    public class BikecartlistIds
    {
        public int BikeId { get; set; }

    }
}
