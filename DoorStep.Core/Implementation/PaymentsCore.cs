using DoorStep.Core.Repository;
using DoorStep.Infrastructure.Repository;
using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStep.Core.Implementation
{
    public class PaymentsCore : IPaymentsCore
    {
        private IPayments _iPayments;
        public PaymentsCore(IPayments iPayments)
        {
            _iPayments = iPayments;
        }
        public bool PreSavePayment(PaymentModule paymentModule)
        {
            return _iPayments.SavePrePaymentDetails(paymentModule);
        }
        public bool PostSavePayment(PaymentModule paymentModule)
        {
            return _iPayments.UpdatePostPaymentDetails(paymentModule);
        }
    }
}
