using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestDB
{
    internal class Commands
    {
        static DataBaseConnection dataBaseConnection = new DataBaseConnection();
        static string regexEmail = @"^[a-zA-Z0-9_]{3,}@[a-zA-Z0-9]{3,}\.[a-zA-Z]{2,}$";

        public static void AddNewEmployee(List<Employee> employees)
        {
            Employee employee = EnterInsertData();

            if (employee.FirstName == "" || employee.LastName == "" || employee.Email == "" || employee.DateOfBirth == DateTime.Parse("1 1 1") || employee.Salary == 0)
                return;

            if (dataBaseConnection.InsertBD(employee))
                employees.Add(employee);
        }

        public static void UpdateEmployee(List<Employee> employees)
        {
            Console.WriteLine("\nВведите ID сотрудника");

            int id;

            if (!int.TryParse(Console.ReadLine(),out id))
            {
                Console.WriteLine("\nНедействительный индекс сотрудника");
                return;
            }

            if (id > 0 && BinarySearch(employees, id, 0, employees.Count) != -1)
            {
                Employee employee = employees[id-1];

                Console.WriteLine("\nЕсли поле не нуждается в изменении, то оставьте строку пустой");

                if (EnterUpdateData(employee))
                {
                    if (dataBaseConnection.UpdateBD(employee))
                        dataBaseConnection.LoadFromBD(employees);
                }
                else
                    Console.WriteLine("\nНет данных для изменения");
            }
            else
                Console.WriteLine("\nНедействительный индекс сотрудника");
        }

        public static void DeleteEmployee(List<Employee> employees)
        {
            Console.WriteLine("\nВведите ID сотрудника");

            int id;

            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("\nНедействительный индекс сотрудника");
                return;
            }

            if (id > 0 && BinarySearch(employees, id, 0, employees.Count) != -1)
            {
                Employee employee = employees[id - 1];

                Console.WriteLine("\nID\tИмя и Фамилия\tЕлектронная почта\tДата рождения\tЗарплата");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                Console.WriteLine(employee.ToString());

                Console.WriteLine("\nПодтвердить удаление? (д/н)\n");

                string str = Console.ReadLine();

                if (str.ToLower() == "д" || str.ToLower() == "y")
                {
                    if (dataBaseConnection.DeleteBD(id))
                        employees.RemoveAt(id - 1);
                }
                else
                    Console.WriteLine("\nУдаление отменено");
            }
            else
                Console.WriteLine("\nНедействительный индекс сотрудника");
        }

        private static int BinarySearch(List<Employee> employees, int searchedValue, int left, int right)
        {
            while (left <= right)
            {
                int middle = (left + right) / 2;

                if (searchedValue == employees[middle].employeeID)
                {
                    return middle;
                }
                else if (searchedValue < employees[middle].employeeID)
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle + 1;
                }
            }
            return -1;
        }

        private static Employee EnterInsertData()
        {
            Employee employee = new Employee();

            string fName, lName, email;
            DateTime dateOfBirth;
            decimal salary;

            try
            {
                Console.WriteLine("Введите имя сотрудника");
                fName = Console.ReadLine();
                if (fName.Replace(" ", "") == "")
                    throw (new SystemException("Имя не должно быть пустым"));

                Console.WriteLine("Введите фамилию сотрудника");
                lName = Console.ReadLine();
                if (lName.Replace(" ", "") == "")
                    throw (new SystemException("Фамилия не должна быть пустой"));

                Console.WriteLine("Введите електронную почту сотрудника");
                email = Console.ReadLine();
                if (email.Replace(" ", "") == "")
                    throw (new SystemException("Электронная почта не должна быть пустой"));
                else if (!Regex.IsMatch(email, regexEmail, RegexOptions.IgnoreCase))
                    throw (new SystemException("Неверный формат электронной почты"));

                Console.WriteLine("Введите дату рождения сотрудника");
                dateOfBirth = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Введите зарплату сотрудника");
                salary = decimal.Parse(Console.ReadLine());

                employee = new Employee(0, fName, lName, email, dateOfBirth, salary);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в заполнении данных\n" + ex.Message);

                return new Employee();
            }

            return employee;
        }

        private static bool EnterUpdateData(Employee employee)
        {
            bool changeCheck = false;

            string fName, lName, email;
            DateTime dateOfBirth;
            decimal salary;

            try
            {
                Console.WriteLine("\nИмя: " + employee.FirstName);
                Console.WriteLine("Введите новое имя сотрудника");
                fName = Console.ReadLine();
                if (fName.Replace(" ", "") != "")
                {
                    employee.FirstName = fName;
                    changeCheck = true;
                }

                Console.WriteLine("\nФамилия: " + employee.LastName);
                Console.WriteLine("Введите новую фамилию сотрудника");
                lName = Console.ReadLine();
                if (lName.Replace(" ", "") != "")
                {
                    employee.LastName = lName;
                    changeCheck = true;
                }

                Console.WriteLine("Почтовый адрес: " + employee.Email);
                Console.WriteLine("Введите новую електронную почту сотрудника");
                email = Console.ReadLine();
                if (email.Replace(" ", "") != "")
                    if (Regex.IsMatch(email, regexEmail, RegexOptions.IgnoreCase))
                    {
                        employee.Email = email;
                        changeCheck = true;
                    }
                    else
                        throw (new SystemException("Неверный формат электронной почты"));

                Console.WriteLine("Дата рождения: " + employee.DateOfBirth);
                Console.WriteLine("Введите новую дату рождения сотрудника");
                string dob = Console.ReadLine();
                if (dob.Replace(" ", "") != "")
                {
                    employee.DateOfBirth = DateTime.Parse(Console.ReadLine());
                    changeCheck = true;
                }

                Console.WriteLine("Зарплата: " + employee.Salary);
                Console.WriteLine("Введите новую зарплату сотрудника");
                string salaryString = Console.ReadLine();
                if (salaryString.Replace(" ", "") != "")
                {
                    employee.Salary = decimal.Parse(Console.ReadLine());
                    changeCheck = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в заполнении данных\n" + ex.Message);

                return false;
            }

            return changeCheck;
        }
    }
}
