using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOUR_OPERATOR
{
    //Interface
    public interface CD
    {
        void insert(IComparable key, Object attribute);
        Object find(IComparable key);
        Object remove(IComparable key);
    }
    public interface Container
    {
        bool isEmpty();
        void makeEmpty();
        int size();
    }

    //Classe TourOperator
    public class TourOperator : CD, Container
    {
        private string nextClientCode;
        private Dictionary<IComparable, object> dizionario;


        //costruttore
        public TourOperator(string codice)
        {
            this.nextClientCode = codice;
            this.dizionario = new Dictionary<IComparable, object>();
        }

        //Metodo add
        public void add(string nome, string dest)
        {
            string codice = this.nextClientCode;
            this.nextClientCode = this.getNextCode(codice);
            this.dizionario[codice] = new Client(nome, dest);
        }

        //Metodo to string
        public override string ToString()
        {
            //dichiarazione variabile
            string stringa = "";

            //ciclo
            foreach (KeyValuePair<IComparable, object> cliente in this.dizionario)
            {
                //assegnazione
                string codice = cliente.Key.ToString();
                Client temp = (Client)cliente.Value;
                //concatena stringa
                stringa += codice + " : " + temp.name + " : " + temp.dest + "\n";
            }
            //ritorna la stringa
            return stringa;
        }

        // Implementazione interfaccia CD
        //Metodo insert
        public void insert(IComparable key, object attribute)
        {
            //controlla se esiste già
            if (this.dizionario.ContainsKey(key))
            {
                this.dizionario[key] = attribute;
            }
            //altrimenti crea
            else
            {
                this.dizionario.Add(key, attribute);
            }
        }

        //Metodo find
        public object find(IComparable key)
        {
            //controlla se esiste
            if (this.dizionario.ContainsKey(key))
            {
                return this.dizionario[key];
            }
            //altrimenti genera eccezione
            else
            {
                throw new System.Collections.Generic.KeyNotFoundException();
            }
        }

        //Metodo remove
        public object remove(IComparable key)
        {
            //Se trova
            if (this.dizionario.ContainsKey(key))
            {
                object attribute = this.dizionario[key];
                this.dizionario.Remove(key);
                return attribute;
            }
            //Altrimenti genera eccezione
            else
            {
                throw new System.Collections.Generic.KeyNotFoundException();
            }
        }

        // Implementazione interfaccia Container
        //Metodo isEmpty
        public bool isEmpty()
        {
            return this.dizionario.Count == 0;
        }

        //Metodo makeEmpty
        public void makeEmpty()
        {
            this.dizionario.Clear();
        }

        //Metodo size
        public int size()
        {
            return this.dizionario.Count;
        }

        // Classi Client
        private class Client
        {
            public string name; // nome del cliente
            public string dest; // destinazione
            public Client(string aName, string aDest)
            {
                this.name = aName;
                this.dest = aDest;
            }
        }

        //Classe coppia
        private class Coppia : IComparable
        {
            //dichiarazione variabili
            public string code;
            public Client client;

            //costruttore
            public Coppia(string aCode, Client aClient)
            {
                this.code = aCode;
                this.client = aClient;
            }

            //Metodo compare
            public int CompareTo(object obj)
            {
                Coppia tmpC = (Coppia)obj;
                return this.code.CompareTo(tmpC.code);
            }
        }

        // Metodo genera nuova codice
        private string getNextCode(string code)
        {
            //dichiarazione variabili
            char car = code[0];
            int n = int.Parse(code.Substring(1));
            //controlla se numero supera 999
            if (n < 999)
                n++;
            else
            {
                car++;
                //controlla se car supera z
                if (car > 'Z')
                {
                    throw new Exception("Contenitore pieno");
                }
                n = 0;
            }
            string nuovaCodice = car + n.ToString();
            //ritorna nuova codice
            return nuovaCodice;
        }
    }

    class Program
    {
        //main
        static void Main(string[] args)
        {
            //dichiarazione variabile
            string nome;
            string destinazione;
            //chiede al utente il codice iniziale
            Console.WriteLine("Inserisci Il codice");
            //inizializza oggeto t1
            TourOperator t1 = new TourOperator(Console.ReadLine());
            //ciclo
            while(true)
            {
                //chiede al utente il nome
                Console.WriteLine("Insersci le sue informazioni, premi 1 per uscire: ");
                Console.WriteLine("Insersci il nome: ");
                nome = Console.ReadLine();
                //se l'utente inserisce 1
                if (nome == "1")
                    break;
                //chiede al utente la destinazione
                Console.WriteLine("Insersci la destinazione: ");
                destinazione = Console.ReadLine();
                //aggiunge al t1
                t1.add(nome, destinazione);
            }
            //stampa a video il contenuto del t1
            Console.WriteLine(t1.ToString());
            Console.ReadLine();
        }
    }
}
