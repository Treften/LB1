using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LB1
{
   
    class Controller
    {
        public static List<Pawn> list = new List<Pawn>();
         static   string filePath = "file.csv";
     public   static string PrintPawn(Pawn pawn)
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
          string s= "\n\n" + "Имя: " + pawn.Name + "\n"
                + "Фамилия: " + pawn.Surname + "\n"
                + "Адрес: " + pawn.Address + "\n"
                + "Возраст: " + pawn.Age + "\n"
                + married;
            return s;
        }
        public static string PrintAll()
        {
            string s="";
            foreach (Pawn pawn in list)
            {
             s+=   PrintPawn(pawn);
            }
            return s;
        }
        public static string PrintByIndex(int ind)
        {
            if (GetByIndex(list, ind) != null)
            {
               return PrintPawn(GetByIndex(list, ind));
            }
            else
            {
                return "Индекс не найден";
            }
        }
        public static string DeleteByIndex(int ind)
        {
            if (GetByIndex(list, ind) != null)
            {
                list.RemoveAt(ind);
                return ("\nДанные удалены");
            }
            else
            {
                return "Индекс не найден";
            }
        }
        public static void SaveFile()
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (Pawn pawn in list)
                {
                    writer.WriteLine(pawn.Name + "," + pawn.Surname + "," + pawn.Address + "," + pawn.Age + "," + pawn.married);
                }

            }
        }
            public static Pawn GetByIndex(List<Pawn> list, int index)
        {
            if (index < list.Count && index > 0)
            {
                return list[index];
            }
            else
            {
                return null;
            }
        }


        public static void ReadFile()
        {
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
                    list.Add(new Pawn(values[0], values[1], values[2], Convert.ToInt32(values[3]), Convert.ToBoolean(values[4])));

                }
            }
        }
    }
}
