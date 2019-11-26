namespace CarDealer.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("Car")]
    public class CarImportDTO
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TravelledDistance { get; set; }

       [XmlArray("parts")]
       [XmlArrayItem("partId")]
        public ImportPartIdDTO[] PartsId { get; set; }
    }
}
