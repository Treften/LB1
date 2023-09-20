using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1
{
  public  class Pawn
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    name = "Jane";
                }
                else
                {
                    name = value;
                }
            }
        }
        private string surname;
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    surname = "Doe";
                }
                else
                {
                    surname = value;
                }
            }
        }
        private string address;
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    address = "Ishim";
                }
                else
                {
                    address = value;
                }
            }
        }
        private int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value<=0)
                {
                    age = 1;
                }
                else
                {
                    age = value;
                }
            }
        }
        public bool married;
        
        public Pawn(string newName, string newSurname,string newAddress,int newAge,bool isMarried)
        {
            Name = newName;
            Surname = newSurname;
            Address = newAddress;
            Age = newAge;
            married = isMarried;
        }
    }
}
