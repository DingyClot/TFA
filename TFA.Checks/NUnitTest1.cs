using System.Collections.Generic;
using NUnit.Framework;
using Should;

namespace TFA.Checks
{
    struct Player
    {
        public string Name;

        public Player(string name)
        {
            Name = name;
        }
    }

    class AddARegular
    {
        public void Do(Player player)
        {
            Repositories.RegularPlayers.Add(player);
        }
    }

    class ManagePlayers
    {
        static AddARegular addARegular = new AddARegular();

        static public AddARegular AddARegular { get { return addARegular; } }
    }

    class Repository
    {
        List<Player> players = new List<Player>();

        public int Size { get { return players.Count; } }

        public void Add(Player player)
        {
            players.Add(player);
        }
    }

    class Repositories
    {
        private static List<Repository> repositories = new List<Repository>() { new Repository() };

        public static Repository RegularPlayers { get { return repositories[0]; } }
    }

    [TestFixture]
    public class ManagePlayers_AddARegular
    {
        [Test]
        public void Adds_one_regular_player_when_none_exist()
        {
            ManagePlayers.AddARegular.Do(new Player("Dave"));

            Repositories.RegularPlayers.Size.ShouldEqual(1);
        }

        [Test]
        public void Adds_one_regular_player_when_some_already_exist()
        {
            Repositories.RegularPlayers.Add(new Player("Colin"));

            ManagePlayers.AddARegular.Do(new Player("Dave"));

            Repositories.RegularPlayers.Size.ShouldEqual(2);
        }
    }
}