using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace verifica_OOP
{
    class Persona
    {
        protected string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                if(value.Length >= 2)
                {
                    nome = value;
                }
                else
                {
                    nome = "Sconosciuto";
                }
            }
        }
    }
    class Conto : Persona
    {
        private float euro;
        private bool chius;

        public float Euro
        {
            get { return euro; }
            set { euro = value; }
        }
        public bool Chius
        {
            get { return chius; }
            set { chius = value; }
        }

        public Conto()
        {
            euro = 0;
            chius = true;
        }

        public void Apri()
        {
            chius = false;
        }

        public void Chiusura()
        {
            chius = true;
        }

        public void Deposita(float a)
        {
            euro += a;
        }

        public void Preleva(float a)
        {
            euro -= a;
        }

        public float Saldo()
        {
            return euro;
        }

        public bool Chiuso()
        {
            return chius;
        }

        public void Get_Info()
        {
            string a;
            if(chius == true)
            {
                a = "Chiuso";
            }
            else
            {
                a = "Aperto";
            }
            Console.WriteLine($"Nome:{nome}\nSaldo:{euro}\nStato:{a}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Conto conto = new Conto();

            Console.WriteLine("Chi vuole creare il conto?");
            conto.Nome = Console.ReadLine();

            while (true)
            {
                Console.WriteLine("Cosa vuoi fare nel tuo conto\n\t1) Aprire il conto\n\t2) Chiudere il conto\n\t3) Depositare sul conto\n\t4) Prelevare da conto\n\t5) Visualizzare il saldo\n\t6) Visualizzare info conto");
                switch(Console.ReadLine())
                {
                    case "1":
                        conto.Apri();
                        Console.WriteLine("il conto è stato aperto");
                        break;
                    case "2":
                        conto.Chiusura();
                        Console.WriteLine("il contro è stato chiuso");
                        break;
                    case "3":
                        Console.WriteLine("quanto vuoi depositare?");
                        conto.Deposita(float.Parse(Console.ReadLine()));
                        Console.WriteLine("i soldi sono stati depositati");
                        break;
                    case "4":
                        Console.WriteLine("quanto vuoi ritirare?");
                        conto.Preleva(float.Parse(Console.ReadLine()));
                        Console.WriteLine("i soldi sono stati Prelevati");
                        break;
                    case "5":
                        Console.WriteLine("Saldo:" + conto.Saldo());
                        break;
                    case "6":
                        conto.Get_Info();
                        break;
                }
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
