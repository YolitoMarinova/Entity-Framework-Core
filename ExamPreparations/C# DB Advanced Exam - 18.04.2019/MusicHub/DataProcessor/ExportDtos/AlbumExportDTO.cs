namespace MusicHub.DataProcessor.ExportDtos
{
    public class AlbumExportDTO
    {
        public string AlbumName { get; set; }

        public string ReleaseDate { get; set; }

        public string ProducerName { get; set; }

        public SongAlbumExportDTO[] Songs { get; set; }

        public string AlbumPrice { get; set; }
    }
}
