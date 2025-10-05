using StoreFront.Core.Models;

namespace StoreFront.Core.Services
{
    public interface IOrderService
    {
        decimal CalculateOrderTotal(IEnumerable<OrderLineItem> items, ShippingPolicy shippingPolicy, Promotion? promo = null);
    }
}
