namespace CarDealer.DTO
{
    public class SaleDTO
    {
        public CarDTO car { get; set; }

        public string customerName { get; set; }

        public string Discount { get; set; }

        public string price { get; set; }

        public string priceWithDiscount { get; set; }
    }
}
