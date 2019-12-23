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
        //Height and Width same for all levels
        private int mapHeight = 20;
        private int mapWidth = 20;

        //We store data in History->Map->Block
        //Map is a Dictionary of Point and Blocks on that point, indicating all current Blocks on the map
        //History is a List of Map, allow us to go backward in time
        private List<Map> history = new List<Map>();
        private Map currentMap = new Map();

        public Level(int LevelNumber)
        {
            this.levelNumber = LevelNumber;
        }

        public int MapHeight { get => mapHeight; set => mapHeight = value; }
        public int MapWidth { get => mapWidth; set => mapWidth = value; }
        public int LevelNumber { get => levelNumber; set => levelNumber = value; }
        internal List<Map> History { get => history; set => history = value; }
        public Map CurrentMap { get => currentMap; set => currentMap = value; }
    }
}