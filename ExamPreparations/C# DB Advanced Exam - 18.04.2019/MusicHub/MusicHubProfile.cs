namespace MusicHub
{
    using System;
    using System.Linq;
    using System.Globalization;

    using AutoMapper;

    using Data.Models;
    using DataProcessor.ImportDtos;
    using DataProcessor.ExportDtos;

    public class MusicHubProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public MusicHubProfile()
        {
            this.CreateMap<WriterImportDTO, Writer>();

            this.CreateMap<ProducerImportDTO, Producer>();

            this.CreateMap<SongImportDTO, Song>()
                .ForMember(cfg => cfg.Duration, src => src.MapFrom(cfg => TimeSpan.ParseExact(cfg.Duration, "c", CultureInfo.InvariantCulture)))
                .ForMember(cfg => cfg.CreatedOn, src => src.MapFrom(cfg => DateTime.ParseExact(cfg.CreatedOn, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(cfg => cfg.Genre, src => src.MapFrom(cfg => cfg.Genre));

            this.CreateMap<PerformerImportDTO, Performer>();

            this.CreateMap<Song, SongAlbumExportDTO>()
                .ForMember(cfg => cfg.SongName, src => src.MapFrom(cfg => cfg.Name))
                .ForMember(cfg => cfg.Price, src => src.MapFrom(cfg => cfg.Price.ToString("F2")))
                .ForMember(cfg => cfg.Writer, src => src.MapFrom(cfg => cfg.Writer.Name));

            this.CreateMap<Album, AlbumExportDTO>()
                .ForMember(cfg => cfg.AlbumName, src => src.MapFrom(cfg => cfg.Name))
                .ForMember(cfg => cfg.ReleaseDate, src => src.MapFrom(cfg => cfg.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(cfg => cfg.ProducerName, src => src.MapFrom(cfg => cfg.Producer.Name))
                .ForMember(cfg => cfg.Songs, src => src.MapFrom(cfg => cfg.Songs
                                                                            .OrderByDescending(s => s.Name)
                                                                            .ThenBy(s => s.Writer.Name)))
                .ForMember(cfg => cfg.AlbumPrice, src => src.MapFrom(cfg => cfg.Songs.Sum(s => s.Price).ToString("F2")));

            this.CreateMap<Song, SongExportDTO>()
                .ForMember(cfg => cfg.SongName, src => src.MapFrom(cfg => cfg.Name))
                .ForMember(cfg => cfg.Writer, src => src.MapFrom(cfg => cfg.Writer.Name))
                .ForMember(cfg => cfg.Performer, src => src.MapFrom(cfg => cfg.SongPerformers.Select(p => p.Performer.FirstName + " " + p.Performer.LastName).FirstOrDefault()))
                .ForMember(cfg => cfg.AlbumProducer, src => src.MapFrom(cfg => cfg.Album.Producer.Name))
                .ForMember(cfg => cfg.Duration, src => src.MapFrom(cfg => cfg.Duration.ToString("c")));
        }
    }
}
