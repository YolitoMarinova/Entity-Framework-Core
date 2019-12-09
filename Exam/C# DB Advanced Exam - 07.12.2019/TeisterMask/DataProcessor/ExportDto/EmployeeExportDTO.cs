namespace TeisterMask.DataProcessor.ExportDto
{
    public class EmployeeExportDTO
    {
        public string Username { get; set; }

        public TaskExportDTO[] Tasks { get; set; }
    }
}
