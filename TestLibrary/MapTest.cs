using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestLibrary
{
    [TestClass]
    public class MapTest
    {
        [TestMethod]
        public void map()
        {

        }

        public Game InitMapForTest()
        {
            Game game = new Game();
            Adventurer adventurer = new Adventurer("Lara","S", "AADADAGGA",1,1);
            Treasure treasure1 = new Treasure(0,3,2);
            Treasure treasure2 = new Treasure(1,3,3);
            Montain montain1 = new Montain(1, 0);
            Montain montain2 = new Montain(2, 1);
            Map map = new Map(3, 4);
            game.AddAdventurerInTheMap(adventurer);
            game.AddMontainInTheMap(montain1);
            game.AddMontainInTheMap(montain2);
            game.AddTreasureInTheMap(treasure1);
            game.AddTreasureInTheMap(treasure2);
            game.AddMapInTheGame(map);
            return game;
        }
    }
}
