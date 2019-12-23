using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Model
{
    class Level
    {
        public int LevelNumber { get; set; }
        //Height and Width same for all levels
        public int MapHeight { get; set; } = 20;
        public int MapWidth { get; set; } = 20;
        public bool LevelContainsEmpty { get; set; } = false;

        //We store data in History->Map->Block
        //Map is a Dictionary of Point and Blocks on that point, indicating all current Blocks on the map
        public Map CurrentMap { get; set; } = new Map();
        //History is a List of Map, allow us to go backward in time
        internal List<Map> History { get; set; } = new List<Map>();

        public Level(int LevelNumber)
        {
            this.LevelNumber = LevelNumber;
        }
    }
}