using DoorStep.Infrastructure.Database;
using DoorStep.Infrastructure.Repository;
using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DoorStep.Infrastructure.Implementation
{
    public class BikeDetails : IBikeDetails
    {
        string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        public List<BikeInformation> BikeInfo()
        {
            var bikeInformation = new List<BikeInformation>();
            using (var db = new ObickeEntities())
            {
                bikeInformation = db.BikeInformations.ToList();
            }
            return bikeInformation;
        }

        public BikeInformation PostBikeInfoDetails(CoreBikeInformation BikeInformationsmodel)
        {

            using (var db = new ObickeEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        BikeInformation data = new BikeInformation()
                        {

                            BikeBrand = BikeInformationsmodel.BikeBrand,
                            BikeModel = Convert.ToDecimal(BikeInformationsmodel.BikeModel),
                            BikeRegistrationYear = BikeInformationsmodel.BikeRegistrationYear,
                            BikeKmDriven = BikeInformationsmodel.BikeKmDriven,
                            BikeDescription = BikeInformationsmodel.BikeDescription,
                            BikeOwnerName = BikeInformationsmodel.BikeOwnerName,
                            BikePrice = BikeInformationsmodel.BikePrice,
                            IsActive = true,
                            IsDelete = false

                        };
                        db.BikeInformations.Add(data);
                        db.SaveChanges();
                        foreach (var obj in BikeInformationsmodel.Imagepath)
                        {
                            ImageDetail imgdeatails = new ImageDetail()
                            {
                                BikeId = data.BikeId,
                                ImgPath = obj.Imagepath
                            };
                            db.ImageDetails.Add(imgdeatails);
                        }
                        db.SaveChanges();
                        transaction.Commit();

                        return data;
                    }
                    catch (Exception Ex)
                    {
                        string msg = Ex.ToString();
                        transaction.Rollback();

                        return null;
                    }

                }

            }

        }

        public CoreBikeInformation GetBikInfo(int? id)
        {
            CoreBikeInformation BikeResult = new CoreBikeInformation();
            List<CoreImagedetails> imagedetails = new List<CoreImagedetails>();
            using (var db = new ObickeEntities())
            {
                var Bikeinfo = db.stpGetBikedetailsInfo(id).ToList();

                if (Bikeinfo != null && Bikeinfo.Count > 0)
                {
                    var BikeInformation = Bikeinfo.FirstOrDefault();

                    BikeResult = new CoreBikeInformation()
                    {
                        BikeId = BikeInformation.BikeId,
                        BikeBrand = BikeInformation.BikeBrand,
                        BikeDescription = BikeInformation.BikeDescription,
                        BikeKmDriven = BikeInformation.BikeKmDriven,
                        BikeModel = Convert.ToString(BikeInformation.BikeModel),
                        BikeRegistrationYear = BikeInformation.BikeRegistrationYear,
                        BikePrice = BikeInformation.BikePrice
                    };

                    foreach (var item in Bikeinfo)
                    {
                        CoreImagedetails imgRes = new CoreImagedetails()
                        {
                            Bikeid = BikeInformation.BikeId,
                            Imagepath = baseUrl + item.ImgPath.Remove(0, 1)

                        };
                        imagedetails.Add(imgRes);
                    }
                    BikeResult.Imagepath = imagedetails;

                }

            }
            return BikeResult;
        }

        public List<BikeHomeModel> GetBikInfoCart(List<BikecartlistIds> Bikeids)
        {
            var bikeInformation = new List<BikeHomeModel>();
            if (Bikeids != null)
            {
                using (var db = new ObickeEntities())
                {
                    for (int i = 0; i < Bikeids.Count; i++)
                    {
                        var _Bikeobj = new BikeHomeModel();
                        int id = Bikeids[i].BikeId;
                        var Bikedetail = db.BikeInformations.Where(x => x.BikeId == id).FirstOrDefault();
                        if (Bikedetail != null)
                        {
                            _Bikeobj.BikeId = id;
                            _Bikeobj.Price = Bikedetail.BikePrice ?? 0;
                            _Bikeobj.BikeName = Bikedetail.BikeBrand;
                            _Bikeobj.Driven = Bikedetail.BikeKmDriven;
                            _Bikeobj.ModelYear = Convert.ToString(Bikedetail.BikeRegistrationYear);
                            _Bikeobj.IsActive = Bikedetail.IsActive;
                            _Bikeobj.ImageUrl = Bikedetail.ImageDetails.Where(x => x.BikeId == id).Select(k => k.ImgPath).FirstOrDefault() == null ? "" : Bikedetail.ImageDetails.Where(x => x.BikeId == id).Select(k => k.ImgPath).FirstOrDefault();
                        }
                        bikeInformation.Add(_Bikeobj);
                    }

                }
            }
            return bikeInformation;
        }

        public List<BikeInformation> BikeInfoByIds(List<BikeInfoByIds> bikeInfoByIds)
        {
            var bikeInformation = new List<BikeInformation>();
            using (var db = new ObickeEntities())
            {
                foreach (var item in bikeInfoByIds)
                {
                    int newBikeId = Convert.ToInt32(item.BikeId);
                    var bikeInfo = db.BikeInformations.Where(a => a.BikeId == newBikeId).FirstOrDefault();
                    if (bikeInfo != null)
                    {
                        bikeInformation.Add(bikeInfo);
                        bikeInformation.ForEach(a => a.ImageDetails.Where(x => x.BikeId == a.BikeId));
                    }
                }
            }
            return bikeInformation;
        }
    }
}
