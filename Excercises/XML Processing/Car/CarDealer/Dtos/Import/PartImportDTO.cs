namespace CarDealer.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("Part")]
    public class PartImportDTO
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("Quantity")]
        public int Quantity { get; set; }

        [XmlElement("supplierId")]
        public int SupplierId { get; set; }
    }
}
