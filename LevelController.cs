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
        public void MoveBlocks(string direction)
        {


        }
        public void UpdateRules()//first reset the rules and then find the new rules and apply them
        {

        }
        public void UpdateBlocks()//like sink/kill
        {
            //sink
            for (int Row = 1; Row < level.MapWidth; Row++)
            {
                for (int Column = 1; Column < level.MapHeight; Column++)
                {
                    bool ContainsSinkBlock = false;
                    foreach (var SinkBlock in level.CurrentMap.PointBlockPairs[(Row, Column)])
                    {
                        if (SinkBlock.IsSink == true) ContainsSinkBlock = true;
                    }
                    if (ContainsSinkBlock)//remove all blocks
                    {
                        level.CurrentMap.PointBlockPairs[(Row, Column)] = new List<Block>();
                    }
                }
            }
            //killblock removes youblock
            for (int Row = 1; Row < level.MapWidth; Row++)
            {
                for (int Column = 1; Column < level.MapHeight; Column++)
                {
                    bool ContainsKillBlock = false;
                    bool ContainsYouBlock = false;
                    foreach (var KillBlock in level.CurrentMap.PointBlockPairs[(Row, Column)])
                    {
                        if (KillBlock.IsKill == true) ContainsKillBlock = true;
                    }
                    foreach (var YouBlock in level.CurrentMap.PointBlockPairs[(Row, Column)])
                    {
                        if (YouBlock.IsYou == true) ContainsYouBlock = true;
                    }
                    if (ContainsKillBlock && ContainsYouBlock)//remove you block
                    {
                        List<Block> NewList = new List<Block>();
                        foreach (var NotYouBlock in level.CurrentMap.PointBlockPairs[(Row, Column)])
                        {
                            if (NotYouBlock.IsYou == false) NewList.Add(NotYouBlock);
                        }
                        level.CurrentMap.PointBlockPairs[(Row, Column)] = NewList;
                    }
                }
            }
        }
        public bool CheckWin()
        {
            for (int Row = 1; Row < level.MapWidth; Row++)
            {
                for (int Column = 1; Column < level.MapHeight; Column++)
                {
                    bool ContainsWinBlock = false;
                    bool ContainsYouBlock = false;
                    foreach (var WinBlock in level.CurrentMap.PointBlockPairs[(Row, Column)])
                    {
                        if (WinBlock.IsWin == true) ContainsWinBlock = true;
                    }
                    foreach (var YouBlock in level.CurrentMap.PointBlockPairs[(Row, Column)])
                    {
                        if (YouBlock.IsYou == true) ContainsYouBlock = true;
                    }
                    if (ContainsWinBlock && ContainsYouBlock) return true;
                }
            }
            return false;
        }

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
