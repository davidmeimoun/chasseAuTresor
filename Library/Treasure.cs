using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Treasure : ElementsOfMap
    {


        private string letter = "T";
        private int width;
        private int height;
        private int numberOfTreasure;

        public Treasure(int width, int height, int numberOfTreasure)
        {
            this.width = width;
            this.height = height;
            this.numberOfTreasure = numberOfTreasure;
        }

        public int NumberOfTreasure
        {
            get => numberOfTreasure; set => numberOfTreasure = value;
        }
        public string Letter
        {
            get => letter; set => letter = value;
        }
        public int Width
        {
            get => width;
            set => width = value;
        }
        public int Height
        {
            get => height;
            set => height = value;
        }

        public override string getLetter()
        {
            return this.letter;
        }
    }
}
