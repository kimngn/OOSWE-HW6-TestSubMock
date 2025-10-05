namespace StoreFront.Core.Models
{
    public class Promotion
    {
        public decimal Percentage { get; set; }
        public bool AppliesTo(decimal subtotal) => subtotal > 0; 
        public decimal Apply(decimal subtotal) => subtotal * (Percentage / 100m);
    }
}
