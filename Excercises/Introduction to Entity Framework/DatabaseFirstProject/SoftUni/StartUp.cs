using SoftUni.Data;
using SoftUni.Models;
using SoftUni.ResultModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main()
        {
            SoftUniContext softUniContext = new SoftUniContext();

            string employees = GetDepartmentsWithMoreThan5Employees(softUniContext);

            Console.WriteLine(employees);
        }

        //Excercise 3
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var employeesFullInfo = context
                .Employees
                .OrderBy(e => e.EmployeeId)
                .Select(e => new EmployeeFullInfoResultModel
                {
                    FirstName = e.FirstName,
                    MiddleName = e.MiddleName,
                    LastName = e.LastName,
                    JobTitle = e.JobTitle,
                    Salary = e.Salary
                }
                )
                .ToList();

            foreach (var employeeInfo in employeesFullInfo)
            {
                result.AppendLine($"{employeeInfo.FirstName} {employeeInfo.LastName} {employeeInfo.MiddleName} {employeeInfo.JobTitle} {employeeInfo.Salary:F2}");
            }

            return result.ToString().TrimEnd();
        }

        //Excercise 4
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var employees = context
                .Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }

            return result.ToString().TrimEnd();
        }

        //Excercise 5
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var employees = context
                    .Employees
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        DepartmentName = e.Department.Name,
                        e.Salary
                    })
                    .Where(e => e.DepartmentName == "Research and Development")
                    .OrderBy(e => e.Salary)
                    .ThenByDescending(e => e.FirstName)
                    .ToList();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:F2}");
            }

            return result.ToString().TrimEnd();
        }

        //Excercise 6
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            Address newAddress = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            context.Addresses.Add(newAddress);

            Employee nakov = context
                .Employees
                .First(e => e.LastName == "Nakov");

            nakov.Address = newAddress;

            context.SaveChanges();

            var adressesText = context
                .Employees
                .OrderByDescending(e => e.AddressId)
                .Select(e => e.Address.AddressText)
                .Take(10)
                .ToList();

            foreach (var adressText in adressesText)
            {
                result.AppendLine(adressText);
            }

            return result.ToString().TrimEnd();
        }

        //Excercise 7
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.EmployeesProjects
                .Any(p => p.Project.StartDate.Year >= 2001
                       && p.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    EmployeeName = e.FirstName + " " + e.LastName,
                    ManagerName = e.Manager.FirstName + " " + e.Manager.LastName,
                    Projects = e.EmployeesProjects
                    .Select(p => new
                    {
                        ProjectName = p.Project.Name,
                        ProjectStartDate = p.Project.StartDate,
                        ProjectEndDate = p.Project.EndDate
                    })
                    .ToList()
                })
                .Take(10)
                .ToList();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.EmployeeName} - Manager: {employee.ManagerName}");

                string datePattern = "M/d/yyyy h:mm:ss tt";

                foreach (var projects in employee.Projects)
                {
                    string endDate = projects.ProjectEndDate == null ? "not finished" : projects.ProjectEndDate.Value.ToString(datePattern, CultureInfo.InvariantCulture);
                    string startDate = projects.ProjectStartDate.ToString(datePattern, CultureInfo.InvariantCulture);

                    result.AppendLine($"--{projects.ProjectName} - {startDate} - {endDate}");
                }
            }

            return result.ToString().TrimEnd();
        }

        //Excercise 8
        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var addresses = context
                .Addresses
                .Select(a => new
                {
                    Address = a.AddressText,
                    TownName = a.Town.Name,
                    EmployeesCount = a.Employees.Count
                })
                .OrderByDescending(a => a.EmployeesCount)
                .ThenBy(a => a.TownName)
                .ThenBy(a => a.Address)
                .Take(10)
                .ToList();

            foreach (var address in addresses)
            {
                result.AppendLine($"{address.Address}, {address.TownName} - {address.EmployeesCount} employees");
            }

            return result.ToString().TrimEnd();
        }

        //Excercise 9
        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            int employeeId = 147;

            var employee = context
                .Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    Name = e.FirstName + " " + e.LastName,
                    e.JobTitle,
                    Projects = e.EmployeesProjects
                    .Select(p => p.Project.Name)
                    .OrderBy(p => p)
                })
                .FirstOrDefault(e => e.EmployeeId == employeeId);

            result.AppendLine($"{employee.Name} - {employee.JobTitle}");

            foreach (var projectName in employee.Projects)
            {
                result.AppendLine(projectName);
            }

            return result.ToString().TrimEnd();
        }

        //Excercise 10
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {

            int minEmployeesCount = 6;

            var departments = context
                .Departments
                .Where(d => d.Employees.Count >= minEmployeesCount)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    Manager = d.Manager.FirstName + " " + d.Manager.LastName,
                    EmployeesCount = d.Employees.Count,
                    Employees = d.Employees
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    })
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList()
                })
                .ToList();

            StringBuilder result = new StringBuilder();

            foreach (var department in departments)
            {
                result.AppendLine($"{department.DepartmentName} - {department.Manager}");

                foreach (var employee in department.Employees)
                {
                    result.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }
            
            return result.ToString().TrimEnd();
        }

        //Excercise 11
        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var projects = context
                .Projects
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                })
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .ToList();

            foreach (var project in projects.OrderBy(p => p.Name))
            {
                result.AppendLine(project.Name);
                result.AppendLine(project.Description);
                result.AppendLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
            }

            return result.ToString().TrimEnd();
        }

        //Excercise 12
        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            List<Employee> employees = context
                .Employees
                .Where(e => e.Department.Name == "Engineering" ||
                            e.Department.Name == "Tool Design" ||
                            e.Department.Name == "Marketing" ||
                            e.Department.Name == "Information Services")
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var employee in employees)
            {
                employee.Salary += employee.Salary * 0.12m;

                context.SaveChanges();

                result.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F2})");
            }

            return result.ToString().TrimEnd();
        }

        //Excercise 13
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            string startForName = "Sa";

            var employees = context
                .Employees
                .Where(e => e.FirstName.StartsWith(startForName))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:F2})");
            }

            return result.ToString().TrimEnd();
        }

        //Excercise 14
        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var projectsToDelete = context
                    .Projects
                    .Find(2);

            var employeeProjects = context
                .EmployeesProjects
                .Where(ep => ep.ProjectId == projectsToDelete.ProjectId);

            context.EmployeesProjects.RemoveRange(employeeProjects);

            context.Projects.Remove(projectsToDelete);

            context.SaveChanges();

            var projects = context
                .Projects
                .Select(p => p.Name)
                .Take(10)
                .ToList();

            foreach (var projectName in projects)
            {
                result.AppendLine(projectName);
            }

            return result.ToString().TrimEnd();
        }

        //Excercise 15
        public static string RemoveTown(SoftUniContext context)
        {
            string townName = "Seattle";

            List<Employee> employeesToChange = context
                .Employees.Where(a => a.Address.Town.Name == townName)
                .ToList();

            foreach (var employee in employeesToChange)
            {
                employee.AddressId = null;

                context.SaveChanges();
            }

            List<Address> addressesToRemove = context
                .Addresses
                .Where(t => t.Town.Name == townName)
                .ToList();

            int deletedAddressesCount = addressesToRemove.Count();

            context.Addresses.RemoveRange(addressesToRemove);

            Town townToRemove = context
                .Towns
                .FirstOrDefault(n => n.Name == townName);

            context.Towns.Remove(townToRemove);

            context.SaveChanges();

            return $"{deletedAddressesCount} addresses in Seattle were deleted";
        }
    }
}
