using TestDB;

List<Employee> employees = new List<Employee>();

DataBaseConnection dataBaseConnection = new DataBaseConnection();

dataBaseConnection.LoadFromBD(employees);

string line = "";

while (line != "5")
{
    Menu.LoadMainMenu();

    line = Console.ReadLine();

    switch (line)
    {
        case "1":
            {
                Console.WriteLine("\nID\tИмя и Фамилия\tЕлектронная почта\tДата рождения\tЗарплата");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");

                foreach (Employee employee in employees)
                    Console.WriteLine(employee.ToString());
            }
            break;

        case "2":
            {
                Commands.AddNewEmployee(employees);
            }
            break;

        case "3":
            {
                Commands.UpdateEmployee(employees);
            }
            break;

        case "4":
            {
                Commands.DeleteEmployee(employees);
            }
            break;

        case "5":
            {
                Console.WriteLine("\nХорошего дня");
            }
            break;

        default:
            {
                Console.WriteLine("\nОтсутствует такой пункт меню");
            }
            break;
    }
}