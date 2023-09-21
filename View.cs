using System;
using System.Collections.Generic;
using System.IO;

namespace LB1
{
    class View
    {
      
            static void Main(string[] args)
        {
            Controller.ReadFile();
            Console.WriteLine("\n1 -- Вывести все записи\n2 -- Вывести по индексу\n3 -- Добавить запись\n4 -- Удалить запись\n5 -- Сохранить данные");
            System.ConsoleKey key = Console.ReadKey().Key;
            while (key != ConsoleKey.Escape)
            {

                switch (key)
                    {
                    case ConsoleKey.D1:
                    Console.WriteLine (Controller.PrintAll());
                    break;
                    case ConsoleKey.D2:
                        Console.Write("\nВведите индекс: ");
                        try
                        {
                            int ind = Convert.ToInt32(Console.ReadLine())+1;
                           Console.WriteLine (Controller.PrintByIndex(ind));
                            
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case ConsoleKey.D3:
                        try
                        {
                            Console.Write("\nВведите имя: ");
                            string name = Console.ReadLine();
                            Console.Write("\nВведите фамилию: ");
                            string surname = Console.ReadLine();
                            Console.Write("\nВведите адрес: ");
                            string address = Console.ReadLine();
                            Console.Write("\nВведите возраст: ");
                            int age = Convert.ToInt32(Console.ReadLine());
                            System.ConsoleKey markey = System.ConsoleKey.Spacebar;
                            bool married=false;
                            while (markey != ConsoleKey.Y && markey!= ConsoleKey.N)
                            {


                                Console.Write("\nВведите семейный статус Y-- женат N -- холост: ");
                                 markey = Console.ReadKey().Key;
                                
                                if (markey == System.ConsoleKey.Y)
                                {
                                    married = true;
                                }
                                else if(markey == System.ConsoleKey.N)
                                {
                                    married = false;
                                }
                            }
                            Controller.list.Add(new Pawn(name, surname, address, age, married));
                            Console.WriteLine("\nДанные добавлены");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case ConsoleKey.D4:
                        Console.Write("\nВведите индекс: ");
                        try
                        {
                            int ind = Convert.ToInt32(Console.ReadLine())+1;
                            Console.WriteLine(Controller.DeleteByIndex(ind));

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine("\nДанные удалены");
                        break;
                    case ConsoleKey.D5:
                       
                        Console.WriteLine("\nДанные сохранены");
                            break;
                }
                Console.WriteLine("\n1 -- Вывести все записи\n2 -- Вывести по индексу\n3 -- Добавить запись\n4 -- Удалить запись\n5 -- Сохранить данные");
                key = Console.ReadKey().Key;
            }
        }
    }
}
