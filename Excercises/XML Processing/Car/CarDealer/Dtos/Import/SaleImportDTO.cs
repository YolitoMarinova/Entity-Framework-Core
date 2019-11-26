namespace CarDealer.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("Sale")]
    public class SaleImportDTO
    {
        [XmlElement("discount")]
        public decimal Discount { get; set; }

        [XmlElement("carId")]
        public int CarId { get; set; }

        [XmlElement("customerId")]
        public int CustomerId { get; set; }
    }
}
