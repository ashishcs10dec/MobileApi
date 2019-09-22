using DoorStep.Infrastructure.Database;
using DoorStepModel.Entity;
using System.Collections.Generic;

namespace DoorStep.Infrastructure.Repository
{
    public interface IBikeDetails
    {
        List<BikeInformation> BikeInfo();
        BikeInformation PostBikeInfoDetails(CoreBikeInformation BikeInformations);
        CoreBikeInformation GetBikInfo(int? id);
        List<BikeHomeModel> GetBikInfoCart(List<BikecartlistIds> Bikeids);
        List<BikeInformation> BikeInfoByIds(List<BikeInfoByIds> bikeInfoByIds);
    }
}
