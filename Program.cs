using System;
using System.Collections.Generic;
using System.IO;

namespace LB1
{
    class Program
    {
        static void PrintPawn(Pawn pawn)
        {
            string married = "";
            if (pawn.married)
            {
                married = "Женат";
            }
            else
            {
                married = "Холост";
            }
            Console.WriteLine("\n\n" + "Имя: " + pawn.Name + "\n"
                + "Фамилия: " + pawn.Surname + "\n" 
                + "Адрес: " + pawn.Address + "\n"
                + "Возраст: " + pawn.Age + "\n"
                + married);

        }
        static void PrintAll(List<Pawn> list)
        {
            foreach(Pawn pawn in list)
            {
                PrintPawn(pawn);
            }

        }
        static Pawn GetByIndex(List<Pawn> list, int index)
        {
            if (index < list.Count&&index>0 )
            {
                return list[index];
            }
            else
            {
                return null;
            }
        }
            static void Main(string[] args)
        {
            List<Pawn> list = new List<Pawn>();
            string filePath = "file.csv";

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    var values = line.Split(',');
                    if (line.Length < 5)
                    {
                        Console.WriteLine("Недостаточно данных");
                        Environment.Exit(0);
                    }
                    list.Add(new Pawn(values[0], values[1], values[2], Convert.ToInt32(values[3]), Convert.ToBoolean(values[4]) ) );
               
                }
            }
            Console.WriteLine("\n1 -- Вывести все записи\n2 -- Вывести по индексу\n3 -- Добавить запись\n4 -- Удалить запись\n5 -- Сохранить данные");
            System.ConsoleKey key = Console.ReadKey().Key;
            while (key != ConsoleKey.Escape)
            {

                switch (key)
                    {
                    case ConsoleKey.D1:
                        PrintAll(list);
                    break;
                    case ConsoleKey.D2:
                        Console.Write("\nВведите индекс: ");
                        try
                        {
                            int ind = Convert.ToInt32(Console.ReadLine())+1;
                            if(GetByIndex(list, ind)!=null)
                            {
                                PrintPawn(GetByIndex(list, ind));
                            }
                            else
                            {
                                Console.WriteLine("Индекс не найден");
                            }
                            
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
                            Console.Write("\nВведите семейный статус: ");
                            bool married = Convert.ToBoolean(Console.ReadLine());
                            list.Add(new Pawn(name, surname, address, age, married));
                            Console.WriteLine("Данные добавлены");
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
                            if (GetByIndex(list, ind) != null)
                            {
                                list.RemoveAt(ind);
                            }
                            else
                            {
                                Console.WriteLine("Индекс не найден");
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case ConsoleKey.D5:
                        using (var writer = new StreamWriter(filePath))
                        {

                        }
                            break;
                }
                Console.WriteLine("\n1 -- Вывести все записи\n2 -- Вывести по индексу\n3 -- Добавить запись\n4 -- Удалить запись\n5 -- Сохранить данные");
                key = Console.ReadKey().Key;
            }
        }
    }
}
