using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FootballOptimizer;

namespace FootballTester
{
    [TestClass]
    public class UnitTest1
    {
        [DataTestMethod]
        [DataRow("4 9 5", "3 2")]
        [DataRow("13 10 7", "0 3")]
        [DataRow("3 3 3 3", "2 2 2")]
        public void GivenFootballersWithEnergy_WhenGameIsPlayed_FootballersScoreCorrectly(string energyLevels, string expectedResults)
        {
            List<Striker> footballers = new List<Striker>();

            // Last energy level is always for the goalie. All other energy levels are for footballers in order of seniority descending.
            // parse input string based on delimiter
            string[] inputs = energyLevels.Split(" ");

            // Retrieve our goalie's energy level and then remove him from the list.
            Goalie goalie = new Goalie(int.Parse(inputs.Last()));
            Array.Resize(ref inputs, inputs.Length - 1);

            foreach (string energy in inputs)
            {
                footballers.Add(new Striker(int.Parse(energy)));
            }

            // now that we have our footballers, let's play the game
            Program.PlayTheGame(ref footballers, goalie);

            Assert.AreEqual(expectedResults, string.Join(" ", footballers.Select(f => f._goals.ToString()).ToList()));
        }

        [DataTestMethod]
        [DataRow("4 9 5", "3 2")]
        [DataRow("13 10 7", "0 3")]
        [DataRow("3 3 3 3", "2 2 2")]
        public void GivenFootballersWithEnergy_WhenGameIsPlayedRecursively_FootballersScoreCorrectly(string energyLevels, string expectedResults)
        {
            List<Striker> footballers = new List<Striker>();

            // Last energy level is always for the goalie. All other energy levels are for footballers in order of seniority descending.
            // parse input string based on delimiter
            string[] inputs = energyLevels.Split(" ");

            // Retrieve our goalie's energy level and then remove him from the list.
            Goalie goalie = new Goalie(int.Parse(inputs.Last()));
            Array.Resize(ref inputs, inputs.Length - 1);

            foreach (string energy in inputs)
            {
                footballers.Add(new Striker(int.Parse(energy)));
            }

            // now that we have our footballers, let's play the game
            Program.PlayTheGameRecursively(ref footballers, goalie);

            Assert.AreEqual(expectedResults, string.Join(" ", footballers.Select(f => f._goals.ToString()).ToList()));
        }
    }
}
