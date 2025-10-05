using StoreFront.Core.Models;

namespace StoreFront.Core.Services
{
    public class OrderService : IOrderService
    {
        public decimal CalculateOrderTotal(IEnumerable<OrderLineItem> items, ShippingPolicy shippingPolicy, Promotion? promo = null)
        {
            var subtotal = items.Sum(i => i.LineTotal());
            var discount = (promo != null && promo.AppliesTo(subtotal)) ? promo.Apply(subtotal) : 0m;
            var subtotalAfterDiscount = subtotal - discount;
            var shipping = shippingPolicy.CalculateShipping(subtotalAfterDiscount);
            return Math.Round(subtotalAfterDiscount + shipping, 2);
        }
    }
}
