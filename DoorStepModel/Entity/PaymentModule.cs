using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStepModel.Entity
{
    public class PaymentModule
    {
        public int TransId { get; set; }
        public string UserName { get; set; }
        public string CustomerId { get; set; }
        public string MobileNo { get; set; }
        public string TxnId { get; set; }
        public decimal TxnAmount { get; set; }
        public string Currency { get; set; }
        public string OrderId { get; set; }
        public string EmailId { get; set; }
        public string TxnStatus { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public string PaymentMode { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime TxnDate { get; set; }
        public string GatewayName { get; set; }
        public string BankTxnId { get; set; }
        public string BankName { get; set; }
        public string PaymentInitiatedDateTime { get; set; }
        public string PaymentCompletedDateTime { get; set; }
    }
}
