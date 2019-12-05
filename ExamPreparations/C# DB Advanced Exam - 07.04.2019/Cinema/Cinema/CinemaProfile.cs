namespace Cinema
{
    using System;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using Cinema.DataProcessor.ExportDto;
    using Data.Models;
    using DataProcessor.ImportDto;
    public class CinemaProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public CinemaProfile()
        {
            this.CreateMap<MovieImportDTO, Movie>();

            this.CreateMap<HallImportDTO, Hall>();

            this.CreateMap<ProjectionImportDTO, Projection>()
                .ForMember(cfg => cfg.DateTime, src => src.MapFrom(cfg => DateTime.ParseExact(cfg.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)));

            this.CreateMap<CustomerImportDTO, Customer>();

            this.CreateMap<Movie, MovieExportDTO>()
                .ForMember(cfg => cfg.MovieName, src => src.MapFrom(cfg => cfg.Title))
                .ForMember(cfg => cfg.Rating, src => src.MapFrom(cfg => cfg.Rating.ToString("F2")))
                .ForMember(cfg => cfg.TotalIncomes, src => src.MapFrom(cfg => cfg.Projections.Sum(p => p.Tickets.Sum(t => t.Price)).ToString("F2")))
                .ForMember(cfg => cfg.Customers, src => src.MapFrom(cfg => cfg.Projections
                                                            .SelectMany(p => p.Tickets.Select(t => t.Customer))
                                                            .OrderByDescending(c => c.Balance.ToString())
                                                            .ThenBy(c => c.FirstName)
                                                            .ThenBy(c => c.LastName))
                );

            this.CreateMap<Customer, CustomerMovieExportDTO>()
                .ForMember(cfg => cfg.Balance, src => src.MapFrom(cfg => cfg.Balance));

            this.CreateMap<Customer, CustomerExportDTO>()
                .ForMember(cfg => cfg.SpentMoney, src => src.MapFrom(cfg => cfg.Tickets.Sum(t => t.Price).ToString("F2")))
                .ForMember(cfg => cfg.SpentTime, src => src.MapFrom(cfg => new TimeSpan(cfg.Tickets.Sum(t => t.Projection.Movie.Duration.Ticks)).ToString("hh\\:mm\\:ss")));
        }
    }
}
