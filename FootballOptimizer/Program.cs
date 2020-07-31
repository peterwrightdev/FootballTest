using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FootballOptimizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Get footballer's energy levels as inputs
            // Note that as problem does not fundamentally change with more than 2 strikers, I have written a solution to cater for any number of strikers.
            int testCases = int.Parse(Console.ReadLine());
            List<string> results = new List<string>();

            for (int i = 0; i < testCases; i++)
            {
                string input = Console.ReadLine();
                List<Striker> footballers = new List<Striker>();

                string[] inputs = input.Split(" ");

                // Assume last energy level is always for the goalie.
                Goalie goalie = new Goalie(int.Parse(inputs.Last()));
                Array.Resize(ref inputs, inputs.Length - 1);

                foreach (string energy in inputs)
                {
                    footballers.Add(new Striker(int.Parse(energy)));
                }

                // now that we have our footballers, let's play the game
                Program.PlayTheGame(ref footballers, goalie);

                // get all footballers scores and write them to our result
                results.Add(string.Join(" ", footballers.Select(f => f._goals.ToString()).ToList()));
            }

            foreach (string result in results)
            {
                Console.WriteLine(result);
            }
        }

        public static void PlayTheGame(ref List<Striker> strikers, Goalie goalie)
        {
            // Loop through the strikers to find who can score. Have them "score" by incrementing their goals and decrementing their energy
            // Problem is simplified as the goalie's energy only changes on a failed attempt to score, so just keep scoring until no strikers can score anymore.
            Striker lastSlastStriker = strikers.Last();
            while (goalie._energy > 1)
            {
                foreach (Striker striker in strikers)
                {
                    // While the current striker can still score, keep scoring. This only effects that striker's energy level, so does not change the situation for other strikers.
                    while(striker._energy != 0 && striker._energy % goalie._energy == 0)
                    {
                        striker.Score();
                    }

                }

                // Once everyone who can score has scored, have "someone" shoot and miss so the goalie's energy drops. Now strikers MAY be able to score again.
                goalie.Save();
            }
        }

        public static void PlayTheGameRecursively(ref List<Striker> footballers, Goalie goalie)
        {
            // LINQ and rescursion based solution - Output is the same, but method of processing is a little different.
            // get all strikers who can score.
            // If there are any, increment their goals and decrement their energy. Repeat until there are none who can score.
            // When no one can score, decrement the goalie's energy and repeat.
            if (goalie._energy == 1)
            {
                return;
            }

            if (footballers.Where(f => f._energy != 0 && f._energy % goalie._energy == 0).Select(f => { f.Score(); return f; }).ToList().Count > 0)
            {
                Program.PlayTheGameRecursively(ref footballers, goalie);
            }
            else
            {
                goalie.Save();
                Program.PlayTheGameRecursively(ref footballers, goalie);
            }
        }
    }

    // Basic class definitions for our entities. Both strikers and goalies have "energy", but only strikers are expected to score goals.
    public class Striker : Footballer
    {
        public Striker(int energy) : base(energy) { }

        public int _goals { get; private set; } = 0;

        public void Score()
        {
            this._energy--;
            this._goals++;
        }
    }

    public class Goalie : Footballer
    {
        public Goalie(int energy) : base(energy) { }

        public void Save()
        {
            this._energy--;
        }
    }

    public class Footballer
    {
        public int _energy { get; protected set; } = 0;

        public Footballer(int energy)
        {
            this._energy = energy;
        }
    }
}
