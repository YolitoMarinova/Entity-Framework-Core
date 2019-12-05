namespace Cinema.DataProcessor
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using Data.Models;
    using DataProcessor.ImportDto;
    using System.Linq;
    using System.Xml.Serialization;
    using System.IO;
    using System.Globalization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            StringBuilder result = new StringBuilder();

            var movieDtos = JsonConvert.DeserializeObject<MovieImportDTO[]>(jsonString);

            foreach (var movieDto in movieDtos)
            {
                if (IsValid(movieDto) &&
                    !IsMovieExist(context, movieDto.Title))
                {
                    Movie movie = Mapper.Map<Movie>(movieDto);

                    context.Movies.Add(movie);

                    result.AppendLine(String.Format(SuccessfulImportMovie, movie.Title, movie.Genre, movie.Rating.ToString("F2")));
                }
                else
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            StringBuilder result = new StringBuilder();

            var hallDtos = JsonConvert.DeserializeObject<HallImportDTO[]>(jsonString);

            foreach (var hallDto in hallDtos)
            {
                if (IsValid(hallDto) && hallDto.SeatsCount > 0)
                {
                    Hall hall = Mapper.Map<Hall>(hallDto);

                    context.Halls.Add(hall);

                    AddSeats(context, hallDto.SeatsCount, hall);

                    string projectionType = GetProjectionType(hall.Is3D, hall.Is4Dx);

                    result.AppendLine(String.Format(SuccessfulImportHallSeat, hall.Name, projectionType, hallDto.SeatsCount));
                }
                else
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            return result.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            var serizlizer = new XmlSerializer(typeof(ProjectionImportDTO[]), new XmlRootAttribute("Projections"));

            var projectionsDtos = (ProjectionImportDTO[])serizlizer.Deserialize(new StringReader(xmlString));

            foreach (var projectionDto in projectionsDtos)
            {
                if (IsValid(projectionDto) &&
                    IsMovieExist(context, projectionDto.MovieId) &&
                    IsHallExist(context, projectionDto.HallId))
                {
                    Projection projection = Mapper.Map<Projection>(projectionDto);

                    context.Projections.Add(projection);

                    string movieTitle = context.Movies
                        .Where(m => m.Id == projection.MovieId)
                        .Select(m => m.Title)
                        .First();

                    string dateTimeResult = projection.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                    result.AppendLine(String.Format(SuccessfulImportProjection,movieTitle, dateTimeResult));
                }
                else
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            context.SaveChanges();

            return result.ToString().TrimEnd();
        }       

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            var serizlizer = new XmlSerializer(typeof(CustomerImportDTO[]), new XmlRootAttribute("Customers"));

            var customersDtos = (CustomerImportDTO[])serizlizer.Deserialize(new StringReader(xmlString));

            foreach (var customerDto in customersDtos)
            {
                if (IsValid(customerDto))
                {
                    Customer customer = Mapper.Map<Customer>(customerDto);

                    context.Customers.Add(customer);

                    AddTickets(context, customer, customerDto.TicketsDtos);

                    result.AppendLine(String.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, customer.Tickets.Count));
                }
                else 
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            return result.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, context, results, true);
        }

        private static bool IsMovieExist(CinemaContext context, string title)
        {
            return context.Movies.Any(m => m.Title == title);
        }

        private static string GetProjectionType(bool is3D, bool is4Dx)
        {
            string result = string.Empty;

            if (is3D)
                result = "3D";
            else if (is4Dx)
                result = "4Dx";
            else
                result = "Normal";

            return result;
        }

        private static void AddSeats(CinemaContext context, int seatsCount, Hall hall)
        {
            var seats = new List<Seat>();

            for (int i = 1; i <= seatsCount; i++)
            {
                Seat seat = new Seat { Hall = hall };

                seats.Add(seat);
            }

            context.Seats.AddRange(seats);
            context.SaveChanges();
        }

        private static bool IsHallExist(CinemaContext context, int hallId)
           => context.Halls.Any(h => h.Id == hallId);

        private static bool IsMovieExist(CinemaContext context, int movieId)
            => context.Movies.Any(m => m.Id == movieId);

        private static void AddTickets(CinemaContext context, Customer customer, TicketCustomerImportDTO[] ticketsDtos)
        {
            foreach (var ticketDto in ticketsDtos)
            {
                if (IsProjectionExist(context, ticketDto.ProjectionId))
                {
                    Ticket ticket = new Ticket
                    {
                        Price = ticketDto.Price,
                        ProjectionId = ticketDto.ProjectionId,
                        Customer = customer
                    };

                    context.Tickets.Add(ticket);
                }
            }

            context.SaveChanges();
        }

        private static bool IsProjectionExist(CinemaContext context, int projectionId)
            => context.Projections.Any(p => p.Id == projectionId);
    }
}