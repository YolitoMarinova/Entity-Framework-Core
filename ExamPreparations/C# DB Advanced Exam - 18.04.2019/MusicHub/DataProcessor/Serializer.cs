namespace MusicHub.DataProcessor
{
    using System;
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using AutoMapper.QueryableExtensions;

    using Data;
    using DataProcessor.ExportDtos;

    public class Serializer
    {
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albums = context
                .Albums
                .Where(a => a.ProducerId == producerId)
                .OrderByDescending(a => a.Songs.Sum(s => s.Price))
                .ProjectTo<AlbumExportDTO>()
                .ToArray();

            var jsonResult = JsonConvert.SerializeObject(albums, Formatting.Indented);

            return jsonResult;
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            StringBuilder result = new StringBuilder();

            var songs = context
                .Songs
                .Where(s => s.Duration.TotalSeconds > duration)
                .ProjectTo<SongExportDTO>()
                .OrderBy(s => s.SongName)
                .ThenBy(s => s.Writer)
                .ThenBy(s => s.Performer)
                .ToArray();

            var serializer = new XmlSerializer(typeof(SongExportDTO[]), new XmlRootAttribute("Songs"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            using (var songsWriter = new StringWriter(result))
            {
                serializer.Serialize(songsWriter, songs, namespaces);
            }

            return result.ToString().TrimEnd();
        }
    }
}