using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaGame
{
    public class Game
    {
        Player player1;
        Player player2;
        int numberOfPizzas;
        List<Mossa> mosseDisponibili;

        Player turnPlayer;
        Player notInTurnPlayer;
        Mossa lastMossa;
        Mossa currentMossa;

        public Game(Player one, Player two)
        {
            player1 = one;
            player2 = two;
            mosseDisponibili = new List<Mossa>();
            mosseDisponibili. Add(new Mossa { Name = "Una Pizza", Value = 1, Available = true });
            mosseDisponibili.Add(new Mossa { Name = "Due Pizze", Value = 2, Available = true });
            mosseDisponibili.Add(new Mossa { Name = "Tre Pizze", Value = 3, Available = true });
        }
        public Player Start()
        {
            var rd = new Random();
            numberOfPizzas = rd.Next(10, 30);
            turnPlayer = player1;
            notInTurnPlayer = player2;

            var end = false;
            while(numberOfPizzas>0 && !end)
            {
                printStat();
                if(makeMossa())
                {
                    evaluate(turnPlayer);
                    end = numberOfPizzas <= 0;
                    swapPlayers();
                }
                else
                {
                    end = true;
                }
            }
            return turnPlayer;
        }
        private void printStat()
        {
            Console.WriteLine($"Ora ci sono {numberOfPizzas} pizze !!!");
        }
        private void swapPlayers()
        {
            Player temp = turnPlayer;
            turnPlayer = notInTurnPlayer;
            notInTurnPlayer = temp;
        }
        private void evaluate(Player currentPlayer)
        {
            var tempValue = currentMossa.Value;
            numberOfPizzas = numberOfPizzas - currentMossa.Value;
            foreach(var m in mosseDisponibili)
            {
                if(m.Value != tempValue && m.Value<=numberOfPizzas)
                {
                    m.Available = true;
                }
                else
                {
                    m.Available = false;
                }
            }
        }
        private bool makeMossa()
        {
            if(mosseDisponibili.Any(m => m.Available))
            {
                currentMossa = askMossa(turnPlayer);
                return true;
            }
            Console.WriteLine($"{turnPlayer.PlayerName} Non hai mosse a tua disposizione, tocca al tuo avversario");
            return false;
        }
        private Mossa askMossa(Player player)
        {
            Console.WriteLine($"{player.PlayerName} Tocca a te, scegli la tua mossa");
            var mosseAttuali = mosseDisponibili.Where(m => m.Available);
            foreach(var m in mosseAttuali)
            {
                Console.WriteLine($"{m.Value}: {m.Name}");
            }
            var s = Console.ReadLine();
            if(int.TryParse(s, out int n) && n>0)
            {
                foreach (var m in mosseAttuali)
                {
                    if (m.Value == n)
                    {
                        return m;
                    }
                }
            }
            Console.WriteLine($"Il numero che hai scelto non va bene {turnPlayer.PlayerName}, figita il numero di una delle opzioni e premi invio");
            return askMossa(player);
        }
    }
    public class Mossa
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool Available { get; set; }
    }
    public class Player
    {
        public string PlayerName { get; }
        public Player(string name)
        {
            PlayerName = name;
        }
    }
}
