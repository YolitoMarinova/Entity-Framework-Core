namespace TeisterMask.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    using static Data.Models.Validations.DataValidator.Task;

    [XmlType("Task")]
    public class TaskImportDTO
    {
        [Required]
        [StringLength(MaxNameLenght, MinimumLength = MinNameLenght)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [Required]
        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [Required]
        [XmlElement("ExecutionType")]
        public int ExcecutionType { get; set; }

        [Required]
        [XmlElement("LabelType")]
        public int LabelType { get; set; }
    }
}
