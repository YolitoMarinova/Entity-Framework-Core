namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlType("Users")]
    public class UserOutputDTO
    {
        [XmlElement("count")]
        public int UsersCount { get; set; }

        [XmlArray("users")]
        [XmlArrayItem()]
        public UserSoldProductsDTO[] Users { get; set; }
    }
}
