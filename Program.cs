using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace WorkTime
{
  
    public class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public string  Gender { get; set; }

        public virtual void PrintWorkSchedule()
        {
            Console.WriteLine("Рабочий график сотрудника не определен.");
        }
    }

    public class YoungEmployee : Employee
    {
        public override void PrintWorkSchedule()
        {
            Console.WriteLine($"Сотрудник {Name} ({Position}) работает 4/7, 4 часов в день.");
        }
    }

    public class AdultEmployee : Employee
    {
        public override void PrintWorkSchedule()
        {
            Console.WriteLine($"Сотрудник {Name} ({Position}) работает 5/7, 8 часов в день.");
        }
    }

    public class SeniorEmployee : Employee
    {
        public override void PrintWorkSchedule()
        {
            Console.WriteLine($"Сотрудник {Name} ({Position}) работает 4/7, 8 часов в день.");
        }
    }

    public class EmployeeManager
    {
        private List<Employee> employees;

        public EmployeeManager()
        {
            employees = new List<Employee>();
        }

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        public List<Employee> GetAllEmployees()
        {
            return employees;
        }

        // Другие методы управления сотрудниками

        public void PrintAllWorkSchedules()
        {
            foreach (Employee employee in employees)
            {
                employee.PrintWorkSchedule();
            }
        }

        public void PrintWorkScheduleByName(string name)
        {
            foreach (Employee employee in employees)
            {
                if (employee.Name == name)
                {
                    employee.PrintWorkSchedule();
                    return;
                }
            }

            Console.WriteLine($"Сотрудник с именем '{name}' не найден.");
        }
        //сохранение в файл
        public void SaveEmployeesToFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (Employee employee in employees)
                {
                    writer.WriteLine($"{employee.Name},{employee.Age},{employee.Position},{employee.Gender}");
                }
            }

            Console.WriteLine("Список сотрудников успешно сохранен в файл.");
        }


    }

    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeManager employeeManager = new EmployeeManager();

            
            string Position = "";
            string Gender = "";
            Console.WriteLine("Введите количество сотрудников");
            string num = Console.ReadLine();
            int X,Y=0;
            bool control = int.TryParse(num, out X);
            if (control)
            {
                Console.WriteLine("Количество сотрудников: " + X);
            }
            else
            {
                Console.WriteLine("Ошибка ввода. Введите корректное целое число.");
            }
            do
            {
                // Добавление сотрудников
                Console.WriteLine($"\nВведите  имя  {Y+1}-го сотрудника:") ;
                string Name = Console.ReadLine();
                Console.WriteLine("Укажите пол сотрудника (M/F) ");
                Gender = Console.ReadLine();
                Console.WriteLine("Введите  возраст сотрудника");
                string input = Console.ReadLine();
                int Age;
                bool success = int.TryParse(input, out Age);
                if (success)
                {
                    Console.WriteLine("Введен возраст сотрудника: " + Age);
                }
                else
                {
                    Console.WriteLine("Ошибка ввода. Введите корректное целое число.");
                }
                if (Age >= 14 && Age <= 17)
                    employeeManager.AddEmployee(new YoungEmployee { Name = Name, Age = Age, Position = "Junior" });

                if ((Age >= 60 && Gender == "F") || (Age >= 65 && Gender == "M"))
                    employeeManager.AddEmployee(new SeniorEmployee { Name = Name, Age = Age, Position = "Senior" });

                if (Age >= 18 && Age<60)
                    employeeManager.AddEmployee(new AdultEmployee { Name = Name, Age = Age, Position = "Manager" });

                Y++;
            } while (Y != X);



            // Вывод рабочих графиков
            employeeManager.PrintAllWorkSchedules();

            // Вывод рабочего графика по имени
            Console.WriteLine("Вывод рабочего графика по имени . Ведите имя сотрудника:");
            string name = Console.ReadLine();
            employeeManager.PrintWorkScheduleByName(name);


            // Сохранение списка сотрудников в файл
            Console.WriteLine("Введите имя файла для сохранения списка сотрудников:");
            string fileName = Console.ReadLine();
            employeeManager.SaveEmployeesToFile(fileName);


        }
    }
}
