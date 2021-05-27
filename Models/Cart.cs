namespace ElectricalShop.Models
{
    public class Cart
{
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public short Quantity { get; set; }

        public string MemberId { get; set; }
    }
}

