using System;
using System.Globalization;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P02_DatabaseFirst
{
    public class StartUp
    {
        public static void Main()
        {
            FindEmployeesByFirstName();
        }

        public static void RemoveTowns()
        {
            using (var db = new SoftUniContext())
            {
                var input = Console.ReadLine();

                var townToDelete = db.Towns.SingleOrDefault(t => t.Name == input);
                var addressesToDelete = db.Addresses.Where(a => a.Town.Name == input).ToList();

                foreach (var e in db.Employees)
                {
                    if (addressesToDelete.Contains(e.Address))
                    {
                        e.AddressId = null;
                    }
                }
            }
        }

        public static void DeleteProjectById()
        {
            using (var db = new SoftUniContext())
            {
                var employeeProjects = db.EmployeesProjects.Where(x => x.ProjectId == 2);

                foreach (var ep in employeeProjects)
                {
                    db.EmployeesProjects.Remove(ep);
                }

                var project = db.Projects.Find(2);

                db.Projects.Remove(project);
                db.SaveChanges();

                var projects = db.Projects.Take(10);

                foreach (var p in projects)
                {
                    Console.WriteLine($"{p.Name}");
                }
            }
        }

        public static void FindEmployeesByFirstName()
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
                    .Where(x => x.FirstName.StartsWith("Sa"))
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName);

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:f2})");
                }
            }
        }

        public static void IncreaseSalaries()
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
                    .Where(
                        e => e.Department.Name.Equals("Engineering") 
                             || e.Department.Name.Equals("Tool Design")
                             || e.Department.Name.Equals("Marketing")
                             || e.Department.Name.Equals("Information Services"))
                    .OrderBy(e => e.FirstName).ThenBy(e => e.LastName);

                foreach (var employee in employees)
                {
                    employee.Salary += employee.Salary * 0.12M;
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
                }
            }
        }

        public static void FindLatest10Projects()
        {
            using (var db = new SoftUniContext())
            {
                var projects = db.Projects
                    .OrderByDescending(p => p.StartDate)
                    .Take(10)
                    .OrderBy(p => p.Name)
                    .Select(p => new { p.Name, p.Description, p.StartDate });

                foreach (var project in projects)
                {
                    Console.WriteLine(project.Name);
                    Console.WriteLine(project.Description);
                    Console.WriteLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
                }
            }
        }

        public static void DepartmentsWithMoreThan5Employees()
        {
            using (var db = new SoftUniContext())
            {
                var departments = db.Departments
                    .Where(x => x.Employees.Count > 5)
                    .OrderBy(x => x.Employees.Count)
                    .ThenBy(x => x.Name)
                    .Select(
                        d => new
                        {
                            DepartmentInfo = $"{d.Name} - {d.Manager.FirstName} {d.Manager.LastName}",
                            EmployeesInfo =
                            d.Employees.Select(e => new { e.FirstName, e.LastName, e.JobTitle })
                                .OrderBy(e => e.FirstName).ThenBy(e => e.LastName)
                        }
                    )
                    .ToList();

                foreach (var department in departments)
                {
                    Console.WriteLine(department.DepartmentInfo);
                    foreach (var emp in department.EmployeesInfo)
                    {
                        Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}");
                    }
                    Console.WriteLine("----------");
                }
            }
        }

        public static void Employee147()
        {
            using (var db = new SoftUniContext())
            {
                var employee = db.Employees
                    .Where(x => x.EmployeeId.Equals(147)).Select(
                        e => new
                        {
                            Info = $"{e.FirstName} {e.LastName} - {e.JobTitle}",
                            e.EmployeesProjects
                        }
                    );
                var projects = db.Projects
                    .Where(p => p.EmployeesProjects.Any(ep => ep.EmployeeId == 147))
                    .Select(p => new { p.Name }).OrderBy(p => p.Name);

                Console.WriteLine(employee.First().Info);
                foreach (var project in projects)
                {
                    Console.WriteLine(project.Name);
                }

            }
        }

        public static void AddressesByTown()
        {
            using (var db = new SoftUniContext())
            {
                var addresses = db.Addresses
                    .OrderByDescending(x => x.Employees.Count)
                    .ThenBy(x => x.Town.Name)
                    .ThenBy(x => x.AddressText)
                    .Take(10).Select(
                        a => new { Info = $"{a.AddressText}, {a.Town.Name} - {a.Employees.Count} employees" });

                foreach (var address in addresses)
                {
                    Console.WriteLine(address.Info);
                }
            }
        }

        public static void EmployeesAndProjects()
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
                    .Where(
                    ep => ep.EmployeesProjects.Any(
                        e =>
                            e.Project.StartDate.Year > 2001 &&
                            e.Project.StartDate.Year <= 2003)).Take(30).Select(
                        e => new
                        {
                            EmployeeFullName = $"{e.FirstName} {e.LastName}",
                            EmployeeManagerFullName = $"{e.Manager.FirstName} {e.Manager.LastName}",
                            Projects = e.EmployeesProjects.Select(
                                ep => new { ep.Project, ep.Project.StartDate, ep.Project.EndDate })
                        }).ToList();


                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.EmployeeFullName} - Manager: {employee.EmployeeManagerFullName}");
                    foreach (var p in employee.Projects)
                    {
                        Console.Write(
                            $"--{p.Project.Name} - "
                            + $"{p.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)} - ");

                        Console.WriteLine(p.EndDate == null
                            ? "not finished"
                            : $"{p.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)}");
                    }
                }
            }
        }

        public static void AddingNewAddressAndUpdateEmployee()
        {
            Address address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            using (var db = new SoftUniContext())
            {
                Employee employee = db.Employees.FirstOrDefault(x => x.LastName.Equals("Nakov"));

                employee.Address = address;

                db.SaveChanges();

                var employees = db.Employees.OrderByDescending(x => x.AddressId).Take(10)
                    .Select(x => new { x.Address.AddressText });

                foreach (var e in employees)
                {
                    Console.WriteLine(e.AddressText);
                }
            }
        }

        public static void EmployeesFullInformation()
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees.OrderBy(x => x.EmployeeId).ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
                }
            }
        }

        public static void EmployeesWithSalaryOver50000()
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees.Where(x => x.Salary > 50000).OrderBy(x => x.FirstName);

                foreach (var employee in employees)
                {
                    Console.WriteLine(employee.FirstName);
                }
            }
        }

        public static void EmployeesFromResearchAndDevelopment()
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees.Where(x => x.Department.Name == "Research and Development")
                    .OrderBy(x => x.Salary)
                    .ThenByDescending(x => x.FirstName)
                    .Select(e => new { e.FirstName, e.LastName, e.Department.Name, e.Salary });

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} from {employee.Name} - ${employee.Salary:f2}");
                }
            }
        }
    }
}
