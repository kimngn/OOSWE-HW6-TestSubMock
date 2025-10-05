using StoreFront.Core.Models;

namespace StoreFront.Core.Services
{
    public interface IPaymentService
    {
        // returns true on success
        bool ProcessPayment(int orderId, decimal amount);
    }
}
