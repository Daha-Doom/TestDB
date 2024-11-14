using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace TestDB
{
    internal class DataBaseConnection
    {
        static string connectionString = @"Server=MSI;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;";

        static async Task ConnectionToBD()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("Подключение открыто");
            }
            Console.WriteLine("Подключение закрыто...");
            Console.WriteLine("Программа завершила работу.");
            Console.Read();
        }

        public void LoadFromBD(List<Employee> employees)
        {
            employees.Clear();

            string sqlExpression = "SELECT * FROM Employees";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine("Подключение открыто");

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("Найдены данные");

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string firstName = reader.GetString(1);
                        string lastName = reader.GetString(2);
                        string email = reader.GetString(3);
                        DateTime dateOfBirth = reader.GetDateTime(4);
                        decimal salary = reader.GetDecimal(5);

                        employees.Add(new Employee(id, firstName, lastName, email, dateOfBirth, salary));
                    }

                    Console.WriteLine("Загрузка данных завершена");
                }

                reader.Close();
            }

            Console.WriteLine("Подключение закрто");
        }

        public bool UpdateBD(Employee employee)
        {
            string sqlExpression = $"UPDATE Employees SET {employee.ToDBUpdateString()} WHERE EmployeeID={employee.employeeID}";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                Console.WriteLine($"\nДанные обновлены");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return true;
        }

        public bool InsertBD(Employee employee)
        {
            string sqlExpression = $"INSERT INTO Employees (FirstName, LastName, Email, DateOfBirth, Salary) VALUES ({employee.ToDBInsertString()})";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                Console.WriteLine($"\nРаботник добавлен");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return true;
        }

        public bool DeleteBD(int id)
        {
            string sqlExpression = $"DELETE FROM Employees WHERE EmployeeID={id}";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                Console.WriteLine($"\nРаботник удалён");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return true;
        }
    }
}
