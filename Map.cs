using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Model
{
    class Map
    {
        public Dictionary<Tuple<int, int>, List<Block>> PointBlockPairs = new Dictionary<Tuple<int, int>, List<Block>>();     //For usage of tuple, refer to https://www.tutorialsteacher.com/csharp/valuetuple
    }
}
