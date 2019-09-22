using DoorStep.Infrastructure.Database;
using DoorStep.Infrastructure.Repository;
using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DoorStep.Infrastructure.Implementation
{
    public class MobileApi : IMobileApi
    {
        public bool SaveUpdateMobileNoWithOtp(MobileOtpDetails mobileOtpDetails)
        {
            using (var db = new ObickeEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var IsAlreadyExists = db.MobileOtps.Where(a => a.MobileNo == mobileOtpDetails.MobileNo).FirstOrDefault();
                        if (IsAlreadyExists != null)
                        {
                            IsAlreadyExists.OTP = mobileOtpDetails.OTP;
                            IsAlreadyExists.OTPSendCount = IsAlreadyExists.OTPSendCount + 1;
                            IsAlreadyExists.ModifiedDateTime = DateTime.Now;
                            db.SaveChanges();
                        }
                        else
                        {
                            MobileOtp mobileOtp = new MobileOtp()
                            {
                                MobileNo = mobileOtpDetails.MobileNo,
                                OTP = mobileOtpDetails.OTP,
                                OTPSendCount = 1,
                                IsActive = true,
                                CreatedDateTime = DateTime.Now,
                                ModifiedDateTime = DateTime.Now
                            };
                            db.MobileOtps.Add(mobileOtp);
                            db.SaveChanges();
                        }
                        transaction.Commit();
                        //Send sms on mobile
                        SendSMS(mobileOtpDetails.MobileNo, mobileOtpDetails.OTP);
                        return true;
                    }
                    catch (Exception Ex)
                    {
                        string msg = Ex.ToString();
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool IsMobileNoVerified(string MobileNo, string Otp)
        {
            using (var db = new ObickeEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var IsAlreadyExists = db.MobileOtps.Where(a => a.MobileNo == MobileNo && a.OTP == Otp).FirstOrDefault();
                        if (IsAlreadyExists != null)
                            return true;
                        else
                            return false;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        public bool UpdateSecretToken(string MobileNo, string Token)
        {
            using (var db = new ObickeEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var IsAlreadyExists = db.MobileOtps.Where(a => a.MobileNo == MobileNo).FirstOrDefault();
                        if (IsAlreadyExists != null)
                        {
                            IsAlreadyExists.SecretToken = Token;
                            IsAlreadyExists.ModifiedDateTime = DateTime.Now;
                            db.SaveChanges();
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception Ex)
                    {
                        string msg = Ex.ToString();
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public string MakeWebRequestGET(string url) //url is https API
        {
            string result = "";
            try
            {
                WebRequest WReq = WebRequest.Create(url);
                WebResponse wResp = WReq.GetResponse();
                StreamReader sr = new StreamReader(wResp.GetResponseStream());
                result = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {
            }
            finally
            {
            }
            return result; //result will provide you MsgID
        }        protected void SendSMS(string MobileNo, string Otp)
        {
            string
            url = "https://smsapi.24x7sms.com/api_2.0/SendSMS.aspx?APIKEY=d8UgiiMYGWU&MobileNo=91" + MobileNo + "&SenderID=OBicke&Message=" + HttpUtility.UrlEncode("Welcome! Your otp is " + Otp + "") + "&ServiceName=TEMPLATE_BASED";
            string request = url;
            string success = MakeWebRequestGET(request);
        }

        public string SaveUserDetails(UserDetails userDetails)
        {
            using (var db = new ObickeEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var IsAlreadyExists = db.UserDetails.Where(a => a.SecretToken == userDetails.SecretToken).FirstOrDefault();
                        if (IsAlreadyExists != null)
                        {
                            IsAlreadyExists.Fullname = userDetails.FullName;
                            IsAlreadyExists.UserEmailId = userDetails.UserEmailId;
                            IsAlreadyExists.MobileNo = userDetails.MobileNo;
                            IsAlreadyExists.CurrentLocation = userDetails.Address;
                            IsAlreadyExists.AddressType = userDetails.AddressType;
                            IsAlreadyExists.LandMark = userDetails.LandMark;
                            IsAlreadyExists.TimeFrom = Convert.ToInt32(userDetails.TimeFrom);
                            IsAlreadyExists.TimeTo = Convert.ToInt32(userDetails.TimeTo);
                            IsAlreadyExists.AMPM = userDetails.AMPM;
                            IsAlreadyExists.City = userDetails.City;
                            IsAlreadyExists.Pincode = userDetails.Pincode;
                            IsAlreadyExists.ModifiedDateTime = DateTime.Now;
                            db.SaveChanges();
                        }
                        else
                        {
                            UserDetail userD = new UserDetail()
                            {
                                Fullname = userDetails.FullName,
                                UserEmailId = userDetails.UserEmailId,
                                MobileNo = userDetails.MobileNo,
                                SecretToken = userDetails.SecretToken,
                                CurrentLocation = userDetails.Address,
                                AddressType = userDetails.AddressType,
                                LandMark = userDetails.LandMark,
                                IsVerified = true,
                                IsActive = true,
                                TimeFrom = Convert.ToInt32(userDetails.TimeFrom),
                                TimeTo = Convert.ToInt32(userDetails.TimeTo),
                                AMPM = userDetails.AMPM,
                                City = userDetails.City,
                                Pincode = userDetails.Pincode,
                                CreatedDateTime = DateTime.Now,
                                ModifiedDateTime = DateTime.Now
                            };
                            db.UserDetails.Add(userD);
                            db.SaveChanges();
                        }

                        var userCartDetailsCount = db.CartDetails.Where(a => a.SecretToken == userDetails.SecretToken).Count();
                        if (userCartDetailsCount != 0)
                        {
                            var alreadyExists = db.CartDetails.Where(a => a.SecretToken == userDetails.SecretToken).ToList();
                            db.CartDetails.RemoveRange(alreadyExists);
                            db.SaveChanges();
                        }
                        foreach (var bikeId in userDetails.BikecartlistIds)
                        {
                            var cartD = new CartDetail()
                            {
                                BikeId = bikeId.BikeId,
                                SecretToken = userDetails.SecretToken,
                                RequestGeneratedBy = "mobile",
                                CreatedDateTime = DateTime.Now,
                                ModifiedDateTime = DateTime.Now
                            };
                            db.CartDetails.Add(cartD);
                            db.SaveChanges();
                        }
                        transaction.Commit();
                        return "Success";
                    }
                    catch (Exception Ex)
                    {
                        string msg = Ex.ToString();
                        transaction.Rollback();
                        return msg;
                    }
                }
            }
        }










        public string AddCart(BikeInfoById bikeInfoById)
        {
            using (var db = new ObickeEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var alreadyExists = db.AddToCarts.Where(a => a.SecretToken == bikeInfoById.SecretToken && a.BikeId == bikeInfoById.BikeId).FirstOrDefault();
                        if (alreadyExists != null)
                        {
                            return "Already added in cart";
                        }
                        var count = db.AddToCarts.Where(a => a.SecretToken == bikeInfoById.SecretToken).Count();
                        if (count < 2)
                        {
                            AddToCart addToCart = new AddToCart()
                            {
                                BikeId = bikeInfoById.BikeId,
                                SecretToken = bikeInfoById.SecretToken,
                                InitiatedThrough = "Mobile",
                                CreatedDateTime = DateTime.Now,
                                ModifiedDateTime = DateTime.Now
                            };
                            db.AddToCarts.Add(addToCart);
                            db.SaveChanges();
                            transaction.Commit();
                            return "Success";
                        }
                        else
                        {
                            return "Sorry, you can't add more than 2 bikes";
                        }
                    }
                    catch (Exception Ex)
                    {
                        string msg = Ex.ToString();
                        transaction.Rollback();
                        return Ex.Message;
                    }
                }
            }
        }

        public string RemoveCart(BikeInfoById bikeInfoById)
        {
            using (var db = new ObickeEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var isExists = db.AddToCarts.Where(a => a.SecretToken == bikeInfoById.SecretToken && a.BikeId == bikeInfoById.BikeId).FirstOrDefault();
                        if (isExists != null)
                        {
                            db.AddToCarts.Remove(isExists);
                            db.SaveChanges();
                            transaction.Commit();
                            return "Success";
                        }
                        else
                        {
                            return "No record found";
                        }
                    }
                    catch (Exception Ex)
                    {
                        string msg = Ex.ToString();
                        transaction.Rollback();
                        return Ex.Message;
                    }
                }
            }
        }
    }
}
