namespace MusicHub.DataProcessor
{
    using System;
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Globalization;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    using AutoMapper;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using DataProcessor.ImportDtos;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter
            = "Imported {0}";
        private const string SuccessfullyImportedProducerWithPhone
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";
        private const string SuccessfullyImportedSong
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            StringBuilder result = new StringBuilder();

            var writersDtos = JsonConvert.DeserializeObject<WriterImportDTO[]>(jsonString);

            foreach (var writerDto in writersDtos)
            {
                if (IsValid(writerDto))
                {
                    Writer writer = Mapper.Map<Writer>(writerDto);

                    context.Writers.Add(writer);

                    result.AppendLine(String.Format(SuccessfullyImportedWriter, writer.Name));
                }
                else
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            StringBuilder result = new StringBuilder();

            var producersDtos = JsonConvert.DeserializeObject<ProducerImportDTO[]>(jsonString);

            foreach (var producerDto in producersDtos)
            {
                if (IsValid(producerDto))
                {
                    Producer producer = Mapper.Map<Producer>(producerDto);

                    context.Producers.Add(producer);

                    AddAlbums(context, producer, producerDto.AlbumsDtos);

                    result.AppendLine(ReturnStringResult(context, producer));
                }
                else
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            var serializer = new XmlSerializer(typeof(SongImportDTO[]), new XmlRootAttribute("Songs"));

            var songsDtos = (SongImportDTO[])serializer.Deserialize(new StringReader(xmlString));

            foreach (var songDto in songsDtos)
            {
                if (IsValid(songDto) &&
                    IsValidGenre(songDto.Genre) &&
                    IsValidAlbum(context, songDto.AlbumId) &&
                    IsValidValidWriter(context, songDto.WriterId))
                {
                    Song song = Mapper.Map<Song>(songDto);

                    context.Songs.Add(song);

                    result.AppendLine(String.Format(SuccessfullyImportedSong, song.Name, song.Genre, song.Duration));
                }
                else
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            var serizalizer = new XmlSerializer(typeof(PerformerImportDTO[]), new XmlRootAttribute("Performers"));

            var performersDtos = (PerformerImportDTO[])serizalizer.Deserialize(new StringReader(xmlString));

            foreach (var performerDto in performersDtos)
            {
                if (IsValid(performerDto))
                {
                    Performer performer = Mapper.Map<Performer>(performerDto);

                    context.Performers.Add(performer);

                    AddSongPerformers(context, performer, performerDto.PerformersSongs);

                    result.AppendLine(GetStringResult(context, performer));
                }
                else
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            context.SaveChanges();

            return result.ToString().TrimEnd();
        }
       
        private static bool IsValid(object obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, context, results, true);
        }

        private static string ReturnStringResult(MusicHubDbContext context, Producer producer)
        {
            if (!context.Producers.Contains(producer))
            {
                return ErrorMessage;
            }

            if (producer.PhoneNumber != null)
            {
                return String.Format(SuccessfullyImportedProducerWithPhone, producer.Name, producer.PhoneNumber, producer.Albums.Count);
            }

            return String.Format(SuccessfullyImportedProducerWithNoPhone, producer.Name, producer.Albums.Count);
        }

        private static void AddAlbums(MusicHubDbContext context, Producer producer, AlbumProducerImportDTO[] albumsDtos)
        {
            var validAlbums = new List<Album>();

            foreach (var albumDto in albumsDtos)
            {
                if (IsValid(albumDto))
                {
                    Album album = new Album
                    {
                        Name = albumDto.Name,
                        ReleaseDate = DateTime.ParseExact(albumDto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Producer = producer
                    };

                    validAlbums.Add(album);
                }
                else
                {
                    context.Producers.Remove(producer);
                    context.SaveChanges();
                    return;
                }
            }

            context.Albums.AddRange(validAlbums);

            context.SaveChanges();
        }

        private static bool IsValidValidWriter(MusicHubDbContext context, int writerId)
           => context.Writers.Any(w => w.Id == writerId);

        private static bool IsValidAlbum(MusicHubDbContext context, int? albumId)
            => context.Albums.Any(a => a.Id == albumId);

        private static bool IsValidGenre(string genre)
        {
            if (Genre.Blues.ToString() == genre ||
                Genre.Jazz.ToString() == genre ||
                Genre.PopMusic.ToString() == genre ||
                Genre.Rap.ToString() == genre ||
                Genre.Rock.ToString() == genre)
            {
                return true;
            }

            return false;
        }

        private static string GetStringResult(MusicHubDbContext context, Performer performer)
        {
            if (context.Performers.Contains(performer))
            {
                return String.Format(SuccessfullyImportedPerformer, performer.FirstName, performer.PerformerSongs.Count);
            }

            return ErrorMessage;
        }

        private static void AddSongPerformers(MusicHubDbContext context, Performer performer, SongPerformerDTO[] performersSongs)
        {
            var validSongPerformers = new List<SongPerformer>();

            foreach (var performerSong in performersSongs)
            {
                if (IsSongValid(context, performerSong.SongId))
                {
                    SongPerformer songPerformer = new SongPerformer
                    {
                        SongId = performerSong.SongId,
                        Performer = performer
                    };

                    validSongPerformers.Add(songPerformer);
                }
                else
                {
                    context.Performers.Remove(performer);
                    context.SaveChanges();
                    return;
                }
            }

            context.SongsPerformers.AddRange(validSongPerformers);

            context.SaveChanges();
        }

        private static bool IsSongValid(MusicHubDbContext context, int songId)
            => context.Songs.Any(s => s.Id == songId);
    }
}