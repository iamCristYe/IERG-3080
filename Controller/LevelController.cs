using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabaIsYou.Model;

namespace BabaIsYou.Controller
{
    class LevelController
    {

        Model.Level level;

        public LevelController(int levelNumber)
        {
            level = new Model.Level(levelNumber);
        }
        public void loadGame()
        {
            //load blocks from data into CurrentMap
            //... 
            //MapHeight=
            //MapWidth=
            level.MapHeight = 18;
            level.MapWidth = 18;
            level.CurrentMap.PointBlockPairs.Add((5, 6), new List<Block> { new Block.Text.TextBaba() });
            level.CurrentMap.PointBlockPairs.Add((6, 6), new List<Block> { new Block.Text.TextIs() });
            level.CurrentMap.PointBlockPairs.Add((7, 6), new List<Block> { new Block.Text.TextYou() });
            level.CurrentMap.PointBlockPairs.Add((12, 6), new List<Block> { new Block.Text.TextFlag() });
            level.CurrentMap.PointBlockPairs.Add((13, 6), new List<Block> { new Block.Text.TextIs() });
            level.CurrentMap.PointBlockPairs.Add((14, 6), new List<Block> { new Block.Text.TextWin() });

            level.CurrentMap.PointBlockPairs.Add((9, 9), new List<Block> { new Block.Thing.Rock() });


            level.CurrentMap.PointBlockPairs.Add((6, 10), new List<Block> { new Block.Thing.Baba() });

            AddToHistory();
        }
        public bool CheckWin()
        {
            //...   
            return false;
        }
        public void MoveBlocks(string direction) { }
        public void UpdateRules()
        {
            //first reset the rules and then find the new rules and apply them
        }
        public void UpdateBlocks() { }
        public void AddToHistory()
        {
            level.History.Add(level.CurrentMap);
        }
        public void GoBack()
        {
            if (level.History.Count > 1)
            {
                level.History.RemoveAt(level.History.Count - 1);
                level.CurrentMap = level.History[level.History.Count - 1];
            }
        }
        public void Draw()
        {
            // MessageBox.Show("Test");
        }
    }
}
