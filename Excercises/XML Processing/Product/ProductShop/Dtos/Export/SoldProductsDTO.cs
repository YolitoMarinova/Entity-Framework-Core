namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    public class SoldProductsDTO
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        [XmlArrayItem("Product")]
        public SoldProductDTO[] Products { get; set; }
    }
}
