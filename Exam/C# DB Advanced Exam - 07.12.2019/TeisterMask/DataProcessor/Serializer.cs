﻿namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            StringBuilder result = new StringBuilder();

            var projects = context
                .Projects
                .Where(p => p.Tasks.Count > 0)
                .Select(p => new ProjectExportDTO
                {
                    TaskCount = p.Tasks.Count,
                    Name = p.Name,
                    HasEndDate = p.DueDate != null ? "Yes" : "No",
                    Tasks = p.Tasks.Select(t => new TaskPojectExportDTO
                    {
                        Name = t.Name,
                        Label = t.LabelType.ToString()
                    })
                    .OrderBy(t => t.Name)
                    .ToArray(),
                })
                .OrderByDescending(p => p.TaskCount)
                .OrderBy(p => p.Name)
                .ToArray();

            var serizlizer = new XmlSerializer(typeof(ProjectExportDTO[]), new XmlRootAttribute("Projects"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            using (var projectsWriter = new StringWriter(result))
            {
                serizlizer.Serialize(projectsWriter, projects, namespaces);
            }

            return result.ToString().TrimEnd();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context
                .Employees
                .Where(e => e.EmployeesTasks.Any(et => DateTime.Compare(et.Task.OpenDate, date) >= 0))
                .Select(e => new EmployeeExportDTO
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                    .Where(t => DateTime.Compare(t.Task.OpenDate, date) >= 0)
                    .OrderByDescending(t => t.Task.DueDate)
                    .ThenBy(t => t.Task.Name)
                    .Select(et => new TaskExportDTO
                    {
                        TaskName = et.Task.Name,
                        OpenDate = et.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                        DueDate = et.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        LabelType = et.Task.LabelType.ToString(),
                        ExecutionType = et.Task.ExecutionType.ToString()
                    })
                    .ToArray()
                })    
                .ToArray()
                .OrderByDescending(e => e.Tasks.Length)
                .ThenBy(e => e.Username)
                .Take(10)
                .ToArray();

            var jsonResult = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return jsonResult;
        }
    }
}