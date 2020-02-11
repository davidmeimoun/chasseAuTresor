using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Montain : ElementsOfMap
    {
        private string letter = "M";
        private int width;
        private int height;

        public Montain(int width, int height)
        {
            this.width = width;
            this.height = height;
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
