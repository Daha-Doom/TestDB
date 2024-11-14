using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDB
{
    internal class Menu
    {
        static public void LoadMainMenu()
        {
            Console.WriteLine("\nМеню");
            Console.WriteLine("1. Вывести всех сотрубников");
            Console.WriteLine("2. Добавить нового сотрудника");
            Console.WriteLine("3. Обновить данные о сотруднике");
            Console.WriteLine("4. Удалить сотрудника");
            Console.WriteLine("5. Выйти из приложения\n");
        }
    }
}
