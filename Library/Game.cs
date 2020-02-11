using System;
using System.IO;
using log4net;

namespace Library
{
    public class Game
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Game));
        private Map map;
        private Adventurer adventurer;

        public Adventurer Adventurer
        {
            get => adventurer; set => adventurer = value;
        }
        public Map Map
        {
            get => map;
            set => map = value;
        }

        public void Fill(String path)
        {
            try
            {
                log.Info($"Start Init with path {path}");
                if (File.Exists(path))
                {
                    if (Path.GetExtension(path).Equals(".txt"))
                    {
                        string[] lines = File.ReadAllLines(path);
                        foreach (string line in lines)
                        {
                            ChooseAction(line);
                        }
                    }
                    else
                    {
                        string errorMessage = "Please choose txt extention";
                        log.Error($"{errorMessage}");
                        throw new Exception(errorMessage);
                    }
                }
                else
                {
                    string errorMessage = "The path doesn't exist";
                    log.Error($"{errorMessage}");
                    throw new Exception(errorMessage);
                }
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}");
                throw e;
            }
        }


        private void ChooseAction(string line)
        {
            try
            {
                if (IfAdventurer(line))
                {
                    Adventurer adventurer = SplitAdventurerLine(line);
                    AddAdventurerInTheMap(adventurer);
                }
                else if (IfMap(line))
                {
                    Map map = SplitMapLine(line);
                    AddMapInTheGame(map);
                }
                else if (IfMontain(line))
                {
                    Montain montain = SplitMontainLine(line);
                    AddMontainInTheMap(montain);
                }
                else if (IfTreasure(line))
                {
                    Treasure treasure = SplitTreasureLine(line);
                    AddTreasureInTheMap(treasure);
                }
            }
            catch (Exception e)
            {
                string errorMessage = "Please check content of the file";
                log.Error($"{errorMessage}");
                throw new Exception(errorMessage);
            }
        }
        private Treasure SplitTreasureLine(string line)
        {
            line.Replace(" ", "");
            string[] instructions = line.Split('-');
            int width = int.Parse(instructions[1]);
            int height = int.Parse(instructions[2]);
            int numberOfTreasure = int.Parse(instructions[3]);
            return new Treasure(height, width, numberOfTreasure);
        }




        private Montain SplitMontainLine(string line)
        {

            line.Replace(" ", "");
            string[] instructions = line.Split('-');
            int width = int.Parse(instructions[1]);
            int height = int.Parse(instructions[2]);
            return new Montain(width, height);
        }
        private Adventurer SplitAdventurerLine(string line)
        {
            line.Replace(" ", "");
            string[] instructions = line.Split('-');
            string name = instructions[1];
            int width = int.Parse(instructions[2]);
            int height = int.Parse(instructions[3]);
            string orientation = instructions[4];
            string moves = instructions[5];
            Adventurer adventurer = new Adventurer(name, orientation, moves, width, height);
            this.adventurer = adventurer;
            return adventurer;
        }

        public void AddTreasureInTheMap(Treasure treasure)
        {
            log.Info($"Add Treasure in the map with with : {treasure.Width} ; height : {treasure.Height} ; number of treasure : {treasure.NumberOfTreasure}");
            map.TravelerMap[treasure.Height, treasure.Width] = treasure;
        }

        public void RunOneMove()
        {
            adventurer.RunOneMove(map.TravelerMap);
        }

        public void RunAllMove()
        {
            adventurer.RunAllMoves(map.TravelerMap);
        }


        public void AddMontainInTheMap(Montain montain)
        {
            log.Info($"Add Montain in the map with width : {montain.Width} ; height : {montain.Height}");
            map.TravelerMap[montain.Height, montain.Width] = montain;

        }

        public void AddMapInTheGame(Map map)
        {
            log.Info($"create  map with width : {map.Width} ; height : {map.Height}");
           this.map = map;

        }

        private Map SplitMapLine(string line)
        {

            line.Replace(" ", "");
            string[] instructions = line.Split('-');
            int width = int.Parse(instructions[1]);
            int height = int.Parse(instructions[2]);
            return new Map(width, height);
        }

       

        public void AddAdventurerInTheMap(Adventurer adventurer)
        {
            log.Info($"Add Adveznturer in the map with name : {adventurer.Name} ; width : {adventurer.Width} ; height : {adventurer.Height} ; orientation : {adventurer.Orientation} ; moves : {adventurer.Moves}");
            map.TravelerMap[adventurer.Height, adventurer.Width] = adventurer;
        }

        private bool IfAdventurer(string line)
        {
            return line.StartsWith("A");
        }

        private bool IfMap(string line)
        {
            return line.StartsWith("C");
        }


        private bool IfMontain(string line)
        {
            return line.StartsWith("M");
        }

        private bool IfTreasure(string line)
        {
            return line.StartsWith("T");
        }


    }
}
