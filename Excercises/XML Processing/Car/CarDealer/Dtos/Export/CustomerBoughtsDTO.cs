namespace CarDealer.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlType("customer")]
    public class CustomerBoughtsDTO
    {
        [XmlAttribute("full-name")]
        public string FullName { get; set; }

        [XmlAttribute("bought-cars")]
        public int BoughtCars { get; set; }

        [XmlAttribute("spent-money")]
        public decimal TotalSpentMoney { get; set; }
    }
}
