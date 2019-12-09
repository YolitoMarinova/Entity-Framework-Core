namespace TeisterMask.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    using static Data.Models.Validations.DataValidator.Project;

    [XmlType("Project")]
    public class ProjectImportDTO
    {
        [Required]
        [StringLength(MaxNameLenght, MinimumLength = MinNameLenght)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [XmlArray("Tasks")]
        public TaskImportDTO[] Tasks { get; set; }
    }
}
