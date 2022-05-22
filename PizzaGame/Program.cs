using System;

namespace PizzaGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Pizza Game!");

            var primo =  GetName("Primo"); // "Alice1";//
            var secondo = GetName("Secondo"); // "Bob2"; 

            var game = new Game(new Player(primo), new Player(secondo));
            var winner = game.Start();

            Console.WriteLine($"Winner is {winner.PlayerName}");
        }
        static string GetName(string numero)
        {
            Console.WriteLine($"Digitare il nome del {numero} giocatore e premere invio");
            var userName = Console.ReadLine();
            if(string.IsNullOrEmpty(userName))
            {
                return GetName(numero);
            }
            return userName;
        }
    }
}
