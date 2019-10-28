using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Model
{
    class Map
    {
        public Dictionary<(int, int), List<Block>> PointBlockPairs;     //For usage of tuple, refer to https://www.tutorialsteacher.com/csharp/valuetuple
    }
}
