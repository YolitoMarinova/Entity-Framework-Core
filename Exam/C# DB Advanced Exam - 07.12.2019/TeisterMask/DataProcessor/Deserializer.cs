namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using System.Xml.Serialization;
    using TeisterMask.DataProcessor.ImportDto;
    using System.IO;
    using System.Text;
    using TeisterMask.Data.Models;
    using System.Globalization;
    using TeisterMask.Data.Models.Enums;
    using Newtonsoft.Json;
    using System.Linq;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ProjectImportDTO[]), new XmlRootAttribute("Projects"));

            var projectsDtos = (ProjectImportDTO[])serializer.Deserialize(new StringReader(xmlString));

            foreach (var projectDto in projectsDtos)
            {
                //TO DO: Chek data if its valid
                if (IsValid(projectDto))
                {
                    Project project = new Project
                    {
                        Name = projectDto.Name,
                        OpenDate = DateTime.ParseExact(projectDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        DueDate = ParseDate(projectDto.DueDate)
                    };

                    context.Projects.Add(project);

                    string text = AddTasks(context, project, projectDto.Tasks);

                    if (!String.IsNullOrEmpty(text))
                    {
                        result.AppendLine(text);
                    }

                    result.AppendLine(String.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
                }
                else
                {
                    result.AppendLine(ErrorMessage);
                }
            }


            return result.ToString().TrimEnd();
        }

        private static string AddTasks(TeisterMaskContext context, Project project, TaskImportDTO[] tasks)
        {
            StringBuilder result = new StringBuilder();

            foreach (var taskDto in tasks)
            {
                if (IsValid(taskDto) &&
                    IsValidOpenDate(taskDto.OpenDate, project.OpenDate) &&
                    IsValidDueDate(taskDto.DueDate, project.DueDate))
                {
                    Task task = new Task
                    {
                        Name = taskDto.Name,
                        OpenDate = DateTime.ParseExact(taskDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        DueDate = DateTime.ParseExact(taskDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        ExecutionType = (ExecutionType)taskDto.ExcecutionType,
                        LabelType = (LabelType)taskDto.LabelType,
                        Project = project
                    };

                    context.Add(task);
                }
                else
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        private static bool IsValidDueDate(string dueDate1, DateTime? dueDate2)
        {
            if (dueDate2 == null)
            {
                return true;
            }

            var taskDate = DateTime.ParseExact(dueDate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (DateTime.Compare(taskDate, (DateTime)dueDate2) <= 0)
            {
                return true;
            }

            return false;
        }

        private static bool IsValidOpenDate(string taskOpenDate, DateTime projectOpenDate)
        {
            var taskDate = DateTime.ParseExact(taskOpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (DateTime.Compare(projectOpenDate, taskDate) <= 0)
            {
                return true;
            }

            return false;
        }

        private static DateTime? ParseDate(string dueDate)
        {
            if (dueDate != null &&
                dueDate != "")
            {
                return DateTime.ParseExact(dueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            return null;
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            StringBuilder result = new StringBuilder();

            var employeesDtos = JsonConvert.DeserializeObject<EmployeeImportDTO[]>(jsonString);

            foreach (var employeeDto in employeesDtos)
            {
                if (IsValid(employeeDto))
                {
                    Employee employee = new Employee
                    {
                        Username = employeeDto.Username,
                        Email = employeeDto.Email,
                        Phone = employeeDto.Phone
                    };

                    context.Employees.Add(employee);

                    string text = AddEmployeeTasks(context, employee, employeeDto.Tasks.Distinct().ToArray());

                    if (!String.IsNullOrEmpty(text))
                    {
                        result.AppendLine(text);
                    }

                    result.AppendLine(String.Format(SuccessfullyImportedEmployee, employee.Username, employee.EmployeesTasks.Count));
                }
                else
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        private static string AddEmployeeTasks(TeisterMaskContext context, Employee employee, int[] tasks)
        {
            StringBuilder result = new StringBuilder();

            foreach (var task in tasks)
            {
                if (IsTaskValid(context, task))
                {
                    EmployeeTask employeeTask = new EmployeeTask
                    {
                        Employee = employee,
                        TaskId = task
                    };

                    context.EmployeesTasks.Add(employeeTask);
                }
                else
                {
                    result.AppendLine(ErrorMessage);
                }
            }

            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        private static bool IsTaskValid(TeisterMaskContext context, int task)
        {
            return context.Tasks.Any(t => t.Id == task);
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}