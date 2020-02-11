using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class EmptyElement : ElementsOfMap
    {
        public override string getLetter()
        {
            return ".";
        }
    }
}
