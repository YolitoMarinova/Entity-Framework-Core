namespace Cinema.DataProcessor.ExportDto
{
    public class MovieExportDTO
    {
        public string MovieName { get; set; }

        public string Rating { get; set; }

        public string TotalIncomes { get; set; }

        public CustomerMovieExportDTO[] Customers { get; set; }
    }
}
