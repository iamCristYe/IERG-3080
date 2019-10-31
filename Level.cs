using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Model
{
    class Level
    {
        private int levelNumber;
        public Level(int LevelNumber)
        {
            this.levelNumber = LevelNumber;
        }

        //We store data in History->Map->Block
        //Map is a Dictionary of Point and Blocks on that point, indicating all current Blocks on the map
        //History is a List of Map, allow us to go backward in time
        private List<Map> history = new List<Map>();
        private Map currentMap = new Map();

        public int LevelNumber { get => levelNumber; set => levelNumber = value; }
        internal List<Map> History { get => history; set => history = value; }
        internal Map CurrentMap { get => currentMap; set => currentMap = value; }
    }
}
