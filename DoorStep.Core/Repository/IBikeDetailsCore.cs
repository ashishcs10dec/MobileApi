
using DoorStep.Infrastructure.Database;
using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStep.Core.Repository
{
    public interface IBikeDetailsCore
    {
        List<CoreBikeInformation> BikeInfoDetails();
        BikeInformation  PostBikeInfoDetails(CoreBikeInformation BikeInformations);
        //Get Bike Details
        CoreBikeInformation GetBikeDetails(int?id);
        //Get Bike Details Cart
        List<BikeHomeModel> GetBikeDetailCart(List<BikecartlistIds> Bikeids);
        List<BikeHomeModel> GetBikeDetailsByIds(List<BikeInfoByIds> bikeInfoById);
    }
}
