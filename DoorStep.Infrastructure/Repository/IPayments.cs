using DoorStep.Infrastructure.Database;
using DoorStepModel.Entity;

namespace DoorStep.Infrastructure.Repository
{
    public interface IPayments
    {
        bool SavePrePaymentDetails(PaymentModule transactionDetail);
        bool UpdatePostPaymentDetails(PaymentModule transactionDetail);
    }
}
