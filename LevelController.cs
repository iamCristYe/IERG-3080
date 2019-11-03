using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabaIsYou.Model;
using System.Diagnostics;

namespace BabaIsYou.Controller
{
    class LevelController
    {

        Model.Level CurrentLevel;

        public LevelController(int levelNumber)
        {
            CurrentLevel = new Model.Level(levelNumber);
        }
        public void loadGame()
        {
            //load blocks from data into CurrentMap
            //... 
            //MapHeight=
            //MapWidth=
            CurrentLevel.MapHeight = 18;
            CurrentLevel.MapWidth = 18;
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    CurrentLevel.CurrentMap.PointBlockPairs.Add((Column, Row), new List<Block> { });
                }
            }
            CurrentLevel.CurrentMap.PointBlockPairs[(4, 5)].Add(new Block.ThingText.TextBaba());
            CurrentLevel.CurrentMap.PointBlockPairs[(6, 6)].Add(new Block.SpecialText.TextIs());
            CurrentLevel.CurrentMap.PointBlockPairs[(7, 6)].Add(new Block.SpecialText.TextYou());
            CurrentLevel.CurrentMap.PointBlockPairs[(12, 6)].Add(new Block.ThingText.TextFlag());
            CurrentLevel.CurrentMap.PointBlockPairs[(13, 6)].Add(new Block.SpecialText.TextIs());
            CurrentLevel.CurrentMap.PointBlockPairs[(14, 6)].Add(new Block.SpecialText.TextWin());
            CurrentLevel.CurrentMap.PointBlockPairs[(9, 9)].Add(new Block.Thing.Rock());
            CurrentLevel.CurrentMap.PointBlockPairs[(6, 10)].Add(new Block.Thing.Baba());

            AddToHistory();
        }
        public void MoveBlocks(string direction)
        {
            //left
            //A new map to store data
            Map NewMap = new Map();
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    NewMap.PointBlockPairs.Add((Column, Row), new List<Block> { });
                }
            }

            //this dict stores CanMove 
            //CanMove means other blocks can move into this place
            Dictionary<(int, int), bool> CanMove = new Dictionary<(int, int), bool>();
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    CanMove.Add((Column, Row), true);//default true
                }
            }

            //this dict stores ShouldMove
            //ShouldMove means block at this place should move
            Dictionary<(int, int), bool> ShouldMove = new Dictionary<(int, int), bool>();
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    ShouldMove.Add((Column, Row), false);
                }
            }

            //modify CanMove
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth - 1; Column++) //don't need to check rightmost column
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        //if someblock is not you/push and is stop, others cannot move into this place
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsStop && (!CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsYou && !CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush))
                        {
                            CanMove[(Column, Row)] = false;
                        }
                    }

                }
            }

            //modify should move and move blocks
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 1; Column < CurrentLevel.MapWidth; Column++) //don't need to check leftmost column
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        //if someblock is not you/push and is stop, others cannot move into this place
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsStop && (!CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsYou && !CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush))
                        {
                            MessageBox.Show("to be completed");
                        }
                    }

                }
            }

        }
        public void UpdateRules()//first reset the rules and then find the new rules and apply them
        {
            //reset rules
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
                {
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey((Column, Row)))
                    {
                        for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                        {
                            CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].ReturnToDefault();
                        }
                    }
                }
            }
            //find new rules and apply them
            //find rules in same row
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                //find is in [1,CurrentLevel.CurrentMap.MapWidth-2]
                for (int Column = 1; Column < CurrentLevel.MapWidth - 1; Column++)
                {
                    //must have three words to form a sentence
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey((Column, Row)) && CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey((Column - 1, Row)) && CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey((Column + 1, Row)))
                    {
                        for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                        {
                            //is found
                            if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i] is Block.SpecialText.TextIs)
                            {
                                //a for loop needed here
                                //left side must be a ThingText
                                if (CurrentLevel.CurrentMap.PointBlockPairs[(Column - 1, Row)][i] is Block.ThingText)
                                {
                                    //a for loop needed here
                                    //both left side and right side are things
                                    if (CurrentLevel.CurrentMap.PointBlockPairs[(Column + 1, Row)][i] is Block.ThingText)
                                    {
                                        //change onething to another
                                        MessageBox.Show("to be completed");
                                    }
                                    if (CurrentLevel.CurrentMap.PointBlockPairs[(Column + 1, Row)][i] is Block.SpecialText)
                                    {
                                        //change properties of certain blocks
                                        MessageBox.Show("to be completed");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //also for rules in a column
            MessageBox.Show("to be completed");

        }
        public void UpdateBlocks()//like sink/kill
        {
            //sink
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
                {
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey((Column, Row)))
                    {
                        bool ContainsSinkBlock = false;
                        foreach (var SinkBlock in CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)])
                        {
                            if (SinkBlock.IsSink == true) ContainsSinkBlock = true;
                        }
                        if (ContainsSinkBlock)//remove all blocks
                        {
                            CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)] = new List<Block>();
                        }
                    }
                }
            }
            //killblock removes youblock
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
                {
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey((Column, Row)))
                    {
                        bool ContainsKillBlock = false;
                        bool ContainsYouBlock = false;
                        foreach (var KillBlock in CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)])
                        {
                            if (KillBlock.IsKill == true) ContainsKillBlock = true;
                        }
                        foreach (var YouBlock in CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)])
                        {
                            if (YouBlock.IsYou == true) ContainsYouBlock = true;
                        }
                        if (ContainsKillBlock && ContainsYouBlock)//remove you block
                        {
                            List<Block> NewList = new List<Block>();
                            foreach (var NotYouBlock in CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)])
                            {
                                if (NotYouBlock.IsYou == false) NewList.Add(NotYouBlock);
                            }
                            CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)] = NewList;
                        }
                    }
                }
            }
        }
        public bool CheckWin()
        {
            foreach (var PointBlockPair in CurrentLevel.CurrentMap.PointBlockPairs)
            {
                bool ContainsWinBlock = false;
                bool ContainsYouBlock = false;
                foreach (var WinBlock in PointBlockPair.Value)
                {
                    if (WinBlock.IsWin == true) ContainsWinBlock = true;
                }
                foreach (var YouBlock in PointBlockPair.Value)
                {
                    if (YouBlock.IsYou == true) ContainsYouBlock = true;
                }
                if (ContainsWinBlock && ContainsYouBlock) return true;
            }
            return false;
        }

        public void AddToHistory()
        {
            CurrentLevel.History.Add(CurrentLevel.CurrentMap);
        }
        public void GoBack()
        {
            if (CurrentLevel.History.Count > 1)
            {
                CurrentLevel.History.RemoveAt(CurrentLevel.History.Count - 1);
                CurrentLevel.CurrentMap = CurrentLevel.History[CurrentLevel.History.Count - 1];
            }
        }
        public void Draw()
        {
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey((Column, Row)) == false)
                    {
                        Trace.Write(";");
                    }
                    else
                    {
                        foreach (var block in CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)])
                        {
                            Trace.Write(block.GetType());
                        }
                        Trace.Write(";");
                    }
                }
                Trace.WriteLine("");
            }
        }

    }
}
