using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercitiu
{
    class RandomNumber
    {
        private System.Random a = new System.Random(); 

        private List<int> check = new List<int>();

        private int aux = 0;
        private int MyNumber = 0;

        public void NewNumber()
        {
            aux = a.Next(0, 7);
            while (check.Contains(aux))
            {
                aux = a.Next(0, 7);
            }
            check.Add(aux);
            MyNumber = aux;
        }

        public int getNumber()
        {
            return MyNumber;
        }

        public void Empty()
        {
            check.Clear();
        }
    }
}
