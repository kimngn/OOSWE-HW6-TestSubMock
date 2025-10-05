namespace StoreFront.Core.Models
{
    public class ShippingPolicy
    {
        public decimal BaseCost { get; set; } = 5.00m;
        public decimal FreeShippingThreshold { get; set; } = 100.00m;

        public decimal CalculateShipping(decimal subtotal)
        {
            return subtotal >= FreeShippingThreshold ? 0m : BaseCost;
        }
    }
}
