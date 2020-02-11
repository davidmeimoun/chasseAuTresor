using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Map : ElementsOfMap
    {
        private ElementsOfMap[,] elementsOfMap;
        private int width;
        private int height;

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.elementsOfMap = new ElementsOfMap[height,width];
            FillElementOfMapEmptyElement(this.elementsOfMap);
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
        public ElementsOfMap[,] TravelerMap
        {
            get => elementsOfMap;
            set => elementsOfMap = value;
        }

        public override string getLetter()
        {
            return "C";
        }

        public void FillElementOfMapEmptyElement(ElementsOfMap[,] elementOfMaps)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    elementOfMaps[i, j] = new EmptyElement();
                }
            }
        }

    }
}
