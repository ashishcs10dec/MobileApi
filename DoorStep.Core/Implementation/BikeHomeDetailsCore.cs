using DoorStep.Core.Repository;
using DoorStep.Infrastructure.Repository;
using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Configuration;

namespace DoorStep.Core.Implementation
{
    public class BikeHomeDetailsCore : IBikeHomeDetailsCore
    {
        private IBikeHomeDetails _iBikeHomeDetails;
        string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        public BikeHomeDetailsCore(IBikeHomeDetails iBikeHomeDetails)
        {
            _iBikeHomeDetails = iBikeHomeDetails;
        }
        public AllBikesPagerModel BikeInfo(int PageNo, int PageSize)
        {
            var allBikesPagerModel = new AllBikesPagerModel();
            var bikeDetails = new List<BikeHomeModel>();
            var bikeInformation = _iBikeHomeDetails.BikeHomeInfo(PageNo, PageSize);
            var pager = _iBikeHomeDetails.BikeListPager(PageNo, PageSize);
            if (bikeInformation != null && bikeInformation.Count() > 0)
            {
                foreach (var item in bikeInformation)
                {
                    var tempBikeDetails = new BikeHomeModel()
                    {
                        BikeId = item.BikeId,
                        BikeName = item.BikeBrand,
                        ImageUrl = item.ImageDetails.Count() == 0 ? (baseUrl + @"/UploadImage/tempImage.jpg") : item.ImageDetails.FirstOrDefault().ImgPath,
                        //ImageUrl = @"UploadImage/b1.jpg",
                        Driven = item.BikeKmDriven,
                        ModelYear = Convert.ToString(item.BikeModel),
                        Price = (decimal)item.BikePrice,
                        IsActive = (bool)item.IsActive
                    };
                    bikeDetails.Add(tempBikeDetails);
                }
            }
            allBikesPagerModel.BikeHomeModel = bikeDetails;
            allBikesPagerModel.Pager = pager;
            return allBikesPagerModel;
        }
    }
}
