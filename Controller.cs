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
       
        public   List<Pawn> list = new List<Pawn>();
        string filePath = "file.csv";
     
     public   string PrintPawn(Pawn pawn)
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
          string s= "\n\n"+"Id: " +pawn.Id + "\n" + "Имя: " + pawn.Name + "\n"
                + "Фамилия: " + pawn.Surname + "\n"
                + "Адрес: " + pawn.Address + "\n"
                + "Возраст: " + pawn.Age + "\n"
                + married;
            return s;
        }
        public  string PrintAll()
        {
            string s = "";
            using (PawnContext db=new PawnContext())
            {
                var Pawns = db.Pawns;
                foreach(Pawn pawn in Pawns)
                {
                    s += PrintPawn(pawn);
                }
            }
            return s; 
        }
        public  string PrintByIndex(int ind)
        {
            if (GetByIndex( ind) != null)
            {
               return PrintPawn(GetByIndex( ind));
            }
            else
            {
                return "Индекс не найден";
            }
        }
        public  string DeleteByIndex(int ind)
        {
           // var v = GetByIndex(ind);
            if ( ind>0)
            {
                using (PawnContext db = new PawnContext())
                {
                    var pawn = db.Pawns.Find(ind);
                    if (pawn != null)
                    {
                        db.Pawns.Remove(pawn);
                        db.SaveChanges();
                    }
                  
                  
                }
                return ("\nДанные удалены");
            }
            else
            {
                return "Индекс не найден";
            }

        }

            public  Pawn GetByIndex( int index)
        {
            using (PawnContext db = new PawnContext())
            {
                var Pawns = db.Pawns;
              
                if ( index >= 0)
                {
                    return db.Pawns.Find(index);
                }

                else
                {
                    return null;
                }
            }
        }


       

    }
}
