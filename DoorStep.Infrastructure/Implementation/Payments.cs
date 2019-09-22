using DoorStep.Infrastructure.Database;
using DoorStep.Infrastructure.Repository;
using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStep.Infrastructure.Implementation
{
    public class Payments : IPayments
    {
        public bool SavePrePaymentDetails(PaymentModule transactionDetail)
        {
            using (var db = new ObickeEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //if OrderId is already exists, update the other inputs
                        var existsModel = db.TransactionDetails.Where(a => a.OrderId == transactionDetail.OrderId).FirstOrDefault();
                        if (existsModel != null)
                        {
                            existsModel.BankName = transactionDetail.BankName;
                            existsModel.BankTxnId = transactionDetail.BankTxnId;
                            existsModel.Currency = transactionDetail.Currency;
                            existsModel.CustomerId = transactionDetail.CustomerId;
                            existsModel.EmailId = transactionDetail.EmailId;
                            existsModel.GatewayName = transactionDetail.GatewayName;
                            existsModel.IsCompleted = transactionDetail.IsCompleted;
                            existsModel.MobileNo = transactionDetail.MobileNo;
                            existsModel.PaymentCompletedDateTime = DateTime.Now;
                            existsModel.PaymentInitiatedDateTime = DateTime.Now;
                            existsModel.PaymentMode = transactionDetail.PaymentMode;
                            existsModel.ResponseCode = transactionDetail.ResponseCode;
                            existsModel.ResponseMsg = transactionDetail.ResponseMsg;
                            existsModel.TxnAmount = transactionDetail.TxnAmount;
                            existsModel.TxnDate = DateTime.Now;
                            existsModel.TxnId = transactionDetail.TxnId;
                            existsModel.TxnStatus = transactionDetail.TxnStatus;
                            existsModel.UserName = transactionDetail.UserName;
                            db.SaveChanges();
                        }
                        else
                        {
                            //Save new entry
                            var transactionsModel = new TransactionDetail()
                            {
                                BankName = transactionDetail.BankName,
                                BankTxnId = transactionDetail.BankTxnId,
                                Currency = transactionDetail.Currency,
                                CustomerId = transactionDetail.CustomerId,
                                EmailId = transactionDetail.EmailId,
                                GatewayName = transactionDetail.GatewayName,
                                IsCompleted = transactionDetail.IsCompleted,
                                MobileNo = transactionDetail.MobileNo,
                                OrderId = transactionDetail.OrderId,
                                PaymentCompletedDateTime = DateTime.Now,
                                PaymentInitiatedDateTime = DateTime.Now,
                                PaymentMode = transactionDetail.PaymentMode,
                                ResponseCode = transactionDetail.ResponseCode,
                                ResponseMsg = transactionDetail.ResponseMsg,
                                TxnAmount = transactionDetail.TxnAmount,
                                TxnDate = DateTime.Now,
                                TxnId = transactionDetail.TxnId,
                                TxnStatus = transactionDetail.TxnStatus,
                                UserName = transactionDetail.UserName
                            };
                            db.TransactionDetails.Add(transactionsModel);
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

        public bool UpdatePostPaymentDetails(PaymentModule transactionDetail)
        {
            using (var db = new ObickeEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //if OrderId is already exists, update the other inputs
                        var existsModel = db.TransactionDetails.Where(a => a.OrderId == transactionDetail.OrderId).FirstOrDefault();
                        if (existsModel != null)
                        {
                            existsModel.BankName = transactionDetail.BankName;
                            existsModel.BankTxnId = transactionDetail.BankTxnId;
                            existsModel.Currency = transactionDetail.Currency;
                            existsModel.GatewayName = transactionDetail.GatewayName;
                            existsModel.IsCompleted = transactionDetail.IsCompleted;
                            existsModel.PaymentCompletedDateTime = DateTime.Now;
                            existsModel.PaymentMode = transactionDetail.PaymentMode;
                            existsModel.ResponseCode = transactionDetail.ResponseCode;
                            existsModel.ResponseMsg = transactionDetail.ResponseMsg;
                            existsModel.TxnAmount = transactionDetail.TxnAmount;
                            existsModel.TxnDate = DateTime.Now;
                            existsModel.TxnId = transactionDetail.TxnId;
                            existsModel.TxnStatus = transactionDetail.TxnStatus;
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
    }
}
