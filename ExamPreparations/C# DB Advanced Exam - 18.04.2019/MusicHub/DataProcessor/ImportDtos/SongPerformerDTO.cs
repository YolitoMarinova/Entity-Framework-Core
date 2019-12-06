namespace MusicHub.DataProcessor.ImportDtos
{
    using System.Xml.Serialization;

    [XmlType("Song")]
    public class SongPerformerDTO
    {
        [XmlAttribute("id")]
        public int SongId { get; set; }
    }
}
