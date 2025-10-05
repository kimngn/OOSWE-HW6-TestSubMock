namespace StoreFront.Core.Models
{
    public class OrderLineItem
    {
        public Product Product { get; }
        public int Quantity { get; set; }

        public OrderLineItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal LineTotal() => Product.Price * Quantity;
    }
}
