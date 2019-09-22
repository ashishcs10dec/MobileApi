
using DoorStep.Core.Repository;
using DoorStep.Infrastructure.Database;
using DoorStep.Infrastructure.Repository;
using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStep.Core.Implementation
{
    public class BikeDetailsCore : IBikeDetailsCore
    {
        private IBikeDetails _iBikeDetails;
        string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        public BikeDetailsCore(IBikeDetails iBikeDetails)
        {
            _iBikeDetails = iBikeDetails;
        }

        public List<CoreBikeInformation> BikeInfoDetails()
        {
            var responseList = new List<CoreBikeInformation>();
            var bikeDetails = _iBikeDetails.BikeInfo();
            if (bikeDetails.Count > 0)
            {
                foreach (var item in bikeDetails)
                {
                    var coreBikeInformation = new CoreBikeInformation()
                    {
                        BikeId = item.BikeId,
                        BikeBrand = item.BikeBrand,
                        BikeModel =  Convert.ToString(item.BikeModel),
                        BikeKmDriven = item.BikeKmDriven,
                        BikeDescription = item.BikeDescription,
                        BikePrice = item.BikePrice,
                        BikePreferDateTimeIn = item.BikePreferDateTimeIn,
                        BikePreferDateTimeOut = item.BikePreferDateTimeOut,
                        BikeOwnerName = item.BikeOwnerName,
                        BikeResonForSale = item.BikeResonForSale,
                        BikeEngineNo = item.BikeEngineNo,
                        BikeChasisNo = item.BikeChasisNo,
                        IsActive = item.IsActive,
                        IsDelete = item.IsDelete,
                        CreatedDateTime = item.CreatedDateTime,
                        ModifiedDateTime = item.ModifiedDateTime
                    };
                    responseList.Add(coreBikeInformation);
                }
            }
            return responseList;
        }

         public BikeInformation PostBikeInfoDetails(CoreBikeInformation BikeInformations)
        {
            var result = _iBikeDetails.PostBikeInfoDetails(BikeInformations);
            return result;
        }

        public CoreBikeInformation GetBikeDetails(int?id)
        {
            var result = _iBikeDetails.GetBikInfo(id);

            return result;
        }

       public List<BikeHomeModel> GetBikeDetailCart(List<BikecartlistIds> Bikeids)
        {
            var bikeDetails = _iBikeDetails.GetBikInfoCart(Bikeids);
            return bikeDetails;
        }

        public List<BikeHomeModel> GetBikeDetailsByIds(List<BikeInfoByIds> bikeInfoById)
        {
            var bikeDetails = new List<BikeHomeModel>();
            var bikeInformation = _iBikeDetails.BikeInfoByIds(bikeInfoById);
            if (bikeInformation != null && bikeInformation.Count() > 0)
            {
                foreach (var item in bikeInformation)
                {
                    var tempBikeDetails = new BikeHomeModel()
                    {
                        BikeId = item.BikeId,
                        BikeName = item.BikeBrand,
                        ImageUrl = item.ImageDetails.Count() == 0 ? (baseUrl + @"/UploadImage/tempImage.jpg") : (baseUrl+item.ImageDetails.FirstOrDefault().ImgPath.Remove(0, 1)),
                        Driven = item.BikeKmDriven,
                        ModelYear = Convert.ToString(item.BikeModel),
                        Price = (decimal)item.BikePrice,
                        IsActive = (bool)item.IsActive
                    };
                    bikeDetails.Add(tempBikeDetails);
                }
            }
            return bikeDetails;
        }
    }
}
