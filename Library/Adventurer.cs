using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Adventurer : ElementsOfMap
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(Adventurer));
        private string name;
        private string orientation;
        private string moves;
        private int width;
        private int height;
        private string letter = "A";
        private int numberOfTreasureCollected = 0;
        private Queue<Treasure> treasures = new Queue<Treasure>();

        public Adventurer(string name, string orientation, string moves, int width, int height)
        {
            this.name = name;
            this.orientation = orientation;
            this.moves = moves;
            this.width = width;
            this.height = height;
        }

        public string Name
        {
            get => name; set => name = value;
        }
        public string Orientation
        {
            get => orientation; set => orientation = value;
        }
        public string Moves
        {
            get => moves; set => moves = value;
        }
        public int Width
        {
            get => width; set => width = value;
        }
        public int Height
        {
            get => height; set => height = value;
        }
        public string Letter
        {
            get => letter; set => letter = value;
        }
        public int NumberOfTreasureCollected
        {
            get => numberOfTreasureCollected; set => numberOfTreasureCollected = value;
        }

        public override string getLetter()
        {
            return this.letter;
        }

        public void RunOneMove(ElementsOfMap[,] map)
        {
            try
            {
                char action = moves[0];

                if (IfMoveAction(action))
                {
                    DoMove(map);
                }
                else if (IfTurnLeftAction(action))
                {
                    TurnLeft(map);
                }
                else if (IfTurnRightAction(action))
                {
                    TurnRight(map);
                }

                moves = moves.Remove(0, 1);
            }
            catch (Exception e)
            {
                log.Error($"An error has occured {e.Message}");
                throw e;
            }

        }

        public void RunAllMoves(ElementsOfMap[,] map)
        {
            try
            {
                foreach (char action in moves)
                {
                    if (IfMoveAction(action))
                    {
                        DoMove(map);
                    }
                    else if (IfTurnLeftAction(action))
                    {
                        TurnLeft(map);
                    }
                    else if (IfTurnRightAction(action))
                    {
                        TurnRight(map);
                    }

                    moves = moves.Remove(0, 1);
                }
            }
            catch (Exception e)
            {
                log.Error($"An error has occured {e.Message}");
                throw e;
            }

        }

        private static bool IfTurnRightAction(char action)
        {
            return 'D'.Equals(action);
        }

        private static bool IfTurnLeftAction(char action)
        {
            return 'G'.Equals(action);
        }

        private static bool IfMoveAction(char action)
        {
            return 'A'.Equals(action);
        }

        private void TurnLeft(ElementsOfMap[,] map)
        {
            if (orientation.Equals("E"))
            {
                orientation = "N";
            }
            else if (orientation.Equals("O"))
            {
                orientation = "S";
            }
            else if (orientation.Equals("N"))
            {
                orientation = "O";
            }
            else if (orientation.Equals("S"))
            {
                orientation = "E";
            }
        }


        public int DecreaseOrIncreaseWidth(int width)
        {

            if (orientation.Equals("E"))
            {
                return width + 1;
            }
            if (orientation.Equals("O"))
            {
                return width - 1;
            }
            return width;
        }

        public int DecreaseOrIncreaseHeight(int height)
        {


            if (orientation.Equals("N"))
            {
                return height - 1;
            }
            if (orientation.Equals("S"))
            {
                return height + 1;
            }
            return height;
        }
        private void DoMove(ElementsOfMap[,] map)
        {
            int localWidth = DecreaseOrIncreaseWidth(width);
            int localHeight = DecreaseOrIncreaseHeight(height);


            if (IfElementOfMapIsNotMontain(map, localWidth, localHeight))
            {

                if (IFElementOfMapIsTreasure(map, localWidth, localHeight))
                {
                    Treasure treasureTemp = ((Treasure)map[localHeight, localWidth]);
                    treasureTemp.NumberOfTreasure--;
                    treasures.Enqueue(treasureTemp);
                    if (treasures.Count > 1)
                    {
                        map[localHeight, localWidth] = this;
                        map[height, width] = treasures.Dequeue();
                    }
                    else
                    {
                        map[localHeight, localWidth] = this;
                        map[height, width] = new EmptyElement();
                        /*
                                                if (treasures.Count > 0)
                                                {
                                                    //          map[height, width] = treasures.Dequeue();
                                                }
                                                */
                    }

                    this.Width = DecreaseOrIncreaseWidth(width);
                    this.Height = DecreaseOrIncreaseHeight(height);
                    numberOfTreasureCollected++;

                }
                else
                {
                    if (treasures.Count > 0)
                    {
                        map[localHeight, localWidth] = this;
                        map[height, width] = treasures.Dequeue();
                    }
                    else
                    {
                        map[localHeight, localWidth] = this;
                        map[height, width] = new EmptyElement();
                    }



                    this.Width = DecreaseOrIncreaseWidth(width);
                    this.Height = DecreaseOrIncreaseHeight(height);

                }
            }
        }

        private static bool IFElementOfMapIsTreasure(ElementsOfMap[,] map, int localWidth, int localHeight)
        {
            return (map[localHeight, localWidth] is Treasure);
        }

        private static bool IfElementOfMapIsNotMontain(ElementsOfMap[,] map, int localWidth, int localHeight)
        {
            return !(map[localHeight, localWidth] is Montain);
        }

        private void TurnRight(ElementsOfMap[,] map)
        {
            if (orientation.Equals("O"))
            {
                orientation = "N";
            }
            else if (orientation.Equals("E"))
            {
                orientation = "S";
            }
            else if (orientation.Equals("S"))
            {
                orientation = "O";
            }
            else if (orientation.Equals("N"))
            {
                orientation = "E";
            }
        }



    }
}
