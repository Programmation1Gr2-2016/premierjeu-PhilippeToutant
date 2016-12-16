using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question5
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] tableau=new bool[100];
            tableau[0] = true;
            tableau[99] = true;
            Random rnd = new Random();
            int nombre1 = 0;
            bool joueur = true;
            for (int i = 0; i < tableau.Length; i++)
            {
                nombre1 = rnd.Next(0, 2);
                if (nombre1 == 0)
                {
                    tableau[i] = true;
                }
                else 
                {
                    tableau[i] = false;
                }
            }
            Console.WriteLine("Bonjours joueur1!");
            Console.WriteLine("Voici les régles");
            Console.WriteLine("A pour reculer de 3");
            Console.WriteLine("S pour reculer de 2");
            Console.WriteLine("D pour reculer de 1");
            Console.WriteLine("G pour reculer de 2");
            Console.WriteLine("H pour reculer de 4");

            positionJoueur = tableau[0];

            while (joueur == true)
            {

            }
        }
    }
}
