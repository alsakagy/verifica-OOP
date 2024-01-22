using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace verifica_OOP
{
    class Persona
    {
        protected string provincia;
        protected string nome;
        public string Provincia
        {
            get { return provincia; }
            set
            {
                if (value.Length == 2)
                {
                    provincia = value;
                }
                else
                {
                    provincia = "Sconosciuta";
                }
            }
        }
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
        public string Info()
        {
            return $"Nome: {nome}\nProvincia: {provincia}";
        }

        public Persona()
        {
            nome = "";
            provincia = "";
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

        public string Get_Info()
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
            return $"Nome:{nome}\nProvincia:{provincia}\nSaldo:{euro}\nStato:{a}";
        }

        public new string Info()
        {
            string a;
            if (chius == true)
            {
                a = "Chiuso";
            }
            else
            {
                a = "Aperto";
            }

            return $"{base.Info()}\nSaldo:{euro}\nStato:{a}";
        }
    }

    class Banca
    {
        List<Conto> array = new List<Conto>();

        public int CercaConto(string a)
        {
            foreach(Conto conto in array)
            {
                if (conto.Nome == a)
                {
                    return array.IndexOf(conto);
                }
            }
            return -1;
        }
        public void ApriConto(string a, string b)
        {
            array.Add(new Conto());
            array[array.Count - 1].Apri();
            array[array.Count - 1].Nome = a;
            array[array.Count - 1].Provincia = b;
        }
        public void ApriConto(int i)
        {
            array[i].Apri();
        }
        public void ChiudiConto(int i)
        {
            array[i].Chiusura();
        }
        public void DepositaConto(int i, float a)
        {
            array[i].Deposita(a);
        }
        public void PrelevaConto(int i, float a)
        {
            array[i].Preleva(a);
        }
        public float VediSaldoConto(int i)
        {
            return array[i].Saldo();
        }
        public string GetInfoConto(int i)
        {
            return array[i].Get_Info();
        }
        public string Info()
        {
            string a = "";
            foreach(Conto conto in array)
            {
                if (conto.Nome != "")
                {
                    a += conto.Info();
                    a += "\n";
                }
            }
            return a;
        }
        public string NomeConto(int i)
        {
            return array[i].Nome;
        }
        public bool StatoConto(int i)
        {
            return array[i].Chiuso();
        }
        public float SaldoContiProvincia(string b)
        {
            float a = 0;
            foreach(Conto conto in array)
            {
                if (conto.Provincia == b)
                {
                    a += conto.Saldo();
                }
            }
            return a;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            bool Uscita1 = false;
            Banca banca = new Banca();

            while(Uscita1 == false)
            {
                Console.WriteLine("Benvenuto User, nella Banca Internazionare Intercontinentale, Cosa vuoi fare?");
                Console.WriteLine("\tQualsiasi altro tasto) Chiudi programma.\n\t1) Aprire un nuovo conto.\n\t2) Gestire un conto già esistente.\n\t3) Informazioni generiche sui conti in banca");
                switch(Console.ReadLine())
                { 
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Inserisci il nome del titolare del nuovo conto.");
                        string a, b;
                        a = Console.ReadLine();
                        Console.WriteLine("Inserisci la provincia del conto.");
                        b = Console.ReadLine();
                        banca.ApriConto(a,b);
                        Console.Clear();
                        break;
                    case "2":
                        int i = -1;

                        // Inserimento nome per ricerca conto + controllo
                        Console.Clear();
                        while(i == -1)
                        {
                            Console.WriteLine("Inserisci il nome del titolare per riconoscere il conto.");
                            i = banca.CercaConto(Console.ReadLine());
                            if(i == -1)
                            {
                                Console.WriteLine("il nome inserito non corrisponde a nessun conto, premere invio e rinserire il nome corretto.");
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        Console.Clear();
                        bool Uscita2 = false;

                        // Secondo menù per scelta azioni conto
                        while (Uscita2 == false)
                        {
                            Console.WriteLine($"Benvenuto nella gestione del sul conto {banca.NomeConto(i)}, scegli l'azione da svolgere sul tuo conto.");
                            Console.WriteLine("\tQualsiasi altro tasto) Torna al menù precedente.\n\t1) Congela il conto.\n\t2) Scongela il conto.\n\t3) Deposita sul conto.\n\t4) Preleva dal conto.\n\t5) Visualizza Saldo del conto.\n\t6) Visualizza tutte le informazioni del conto.");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    if(banca.StatoConto(i) == false)
                                    {
                                        banca.ChiudiConto(i);
                                    }
                                    else
                                    {
                                        Console.WriteLine("non puoi congelare un conto che è già congelato. (invio per andare avanti)");
                                        Console.ReadLine();
                                    }
                                    Console.Clear();
                                    break;
                                case "2":
                                    if (banca.StatoConto(i))
                                    {
                                        banca.ApriConto(i);
                                    }
                                    else
                                    {
                                        Console.WriteLine("non puoi scongelare un conto che non è congelato. (invio per andare avanti)");
                                        Console.ReadLine();
                                    }
                                    Console.Clear();
                                    break;
                                case "3":
                                    if(banca.StatoConto(i) == false)
                                    {
                                        Console.WriteLine("Quanto vuoi depositare?");
                                        banca.DepositaConto(i, float.Parse(Console.ReadLine()));
                                    }
                                    else
                                    {
                                        Console.WriteLine("non puoi depositare dei soldi su un conto congelato. (invio per andare avanti)");
                                        Console.ReadLine();
                                    }
                                    Console.Clear();
                                    break;
                                case "4":
                                    if (banca.StatoConto(i) == false)
                                    {
                                        if (banca.VediSaldoConto(i) != 0)
                                        {
                                            float al = 0;
                                            Console.WriteLine("Quanto vuoi prelevare?");
                                            al = float.Parse(Console.ReadLine());
                                            while (al > banca.VediSaldoConto(i))
                                            {
                                                if (al > banca.VediSaldoConto(i))
                                                {
                                                    Console.WriteLine("i soldi da prelevare superano i soldi sul conto, rinserisci la quantità da prelevare e riprova.");
                                                    al = float.Parse(Console.ReadLine());
                                                }
                                            }
                                            banca.PrelevaConto(i, al);
                                        }
                                        else
                                        {
                                            Console.WriteLine("non puoi prelevare essendo che il conto è vuoto. (invio per andare avanti)");
                                            Console.ReadLine();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("non puoi prelevare dei soldi su un conto congelato. (invio per andare avanti)");
                                        Console.ReadLine();
                                    }
                                    Console.Clear();
                                    break;
                                case "5":
                                    Console.WriteLine($"questo è l'ammontare del conto:\n{banca.VediSaldoConto(i)} (invio per andare avanti)");
                                    Console.ReadLine();
                                    Console.Clear();
                                    break;
                                case "6":
                                    Console.WriteLine($"queste sono tutte le informazioni del conto:\n{banca.GetInfoConto(i)} (invio per andare avanti)");
                                    Console.ReadLine();
                                    Console.Clear();
                                    break;
                                default:
                                    Uscita2 = true;
                                    break;
                            }
                        }
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("\tQualsiasi altro tasto) Torna al menù precedente.\n\t1) Ammontare dei saldi in una provincia.\n\t2) Tutte info dei conti");
                        bool Uscita3 = false;
                        while (Uscita3 == false)
                        {
                            switch(Console.ReadLine())
                            {
                                case "1":
                                    Console.WriteLine("inserisci la provincia (2 lettere).");
                                    Console.WriteLine($"Saldo totale dei conti della provincia: {banca.SaldoContiProvincia(Console.ReadLine())} (invio per andare avanti)");
                                    Console.ReadLine();
                                    break;
                                case "2":
                                    Console.WriteLine(banca.Info());
                                    break;
                                default:
                                    Uscita3 = true;
                                    break;
                            }
                        }
                        Console.Clear();
                        break;
                    default:
                        Uscita1 = true;
                        break;
                }
            }
        }
    }
}
