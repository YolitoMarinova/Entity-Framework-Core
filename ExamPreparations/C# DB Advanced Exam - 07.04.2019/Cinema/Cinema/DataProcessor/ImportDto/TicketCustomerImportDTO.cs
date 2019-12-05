namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Ticket")]
    public class TicketCustomerImportDTO
    {
        [XmlElement("ProjectionId")]
        public int ProjectionId { get; set; }

        [Range(0.01, double.MaxValue)]
        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}
