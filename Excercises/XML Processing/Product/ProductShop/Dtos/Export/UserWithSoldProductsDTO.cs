namespace ProductShop.Dtos.Export
{
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [XmlType("User")]
    public class UserWithSoldProductsDTO
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlArray("soldProducts")]
        [XmlArrayItem("Product")]
        public SoldProductDTO[] SoldProducts { get; set; }
    }
}
