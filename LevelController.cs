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
                    CurrentLevel.CurrentMap.PointBlockPairs.Add(Tuple.Create(Column, Row), new List<Block> { });
                }
            }

            CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(4, 5)].Add(new Block.ThingText.TextBaba());
            CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(6, 6)].Add(new Block.SpecialText.TextIs());
            CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(7, 6)].Add(new Block.SpecialText.TextYou());
            CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(12, 6)].Add(new Block.ThingText.TextFlag());
            CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(13, 6)].Add(new Block.SpecialText.TextIs());
            CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(14, 6)].Add(new Block.SpecialText.TextWin());
            CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(9, 9)].Add(new Block.Thing.Rock());
            CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(6, 10)].Add(new Block.Thing.Baba());

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
                    NewMap.PointBlockPairs.Add(Tuple.Create(Column, Row), new List<Block> { });
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
            //ShouldMove means block at this place should move, it's designed for ispush block, not isyou block
            Dictionary<(int, int), bool> ShouldMove = new Dictionary<(int, int), bool>();
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    ShouldMove.Add((Column, Row), false);//default false
                }
            }

            //modify CanMove
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth - 1; Column++) //don't need to check rightmost column, loop from left to right
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)].Count; i++)
                    {
                        //if someblock is not you/push and is stop, others cannot move into this place
                        if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsStop && (!CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsYou && !CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsPush))
                        {
                            CanMove[(Column, Row)] = false;
                            //other block at this point should not change this result
                            break;
                        }
                        //if some block is push, whether it can move depend on its left point
                        else if (Column > 0 && CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsPush)
                        {
                            if (CanMove[(Column - 1, Row)] == false)
                            {
                                CanMove[(Column, Row)] = false;
                            }
                        }
                    }

                }
            }

            //modify should move and move blocks
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = CurrentLevel.MapWidth - 1; Column > 0; Column--) //don't need to check leftmost column, loop from right to left
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsYou && CanMove[(Column - 1, Row)])
                        {
                            NewMap.PointBlockPairs[Tuple.Create(Column - 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i]);
                            ShouldMove[(Column - 1, Row)] = true;//left block should be pushed(if any)
                        }
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsPush && ShouldMove[(Column, Row)])//Do we need to consider canmove?
                        {
                            NewMap.PointBlockPairs[Tuple.Create(Column - 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i]);
                            ShouldMove[(Column - 1, Row)] = true;//left block should be pushed(if any)
                        }
                        else
                        {
                            //copy the same element into the new map at same location
                            NewMap.PointBlockPairs[Tuple.Create(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i]);
                        }
                    }

                }
            }

            CurrentLevel.CurrentMap = NewMap;

        }
        public void UpdateRules()//first reset the rules and then find the new rules and apply them
        {
            //reset rules
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
                {
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey(Tuple.Create(Column, Row)))
                    {
                        for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)].Count; i++)
                        {
                            CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].ReturnToDefault();
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
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey(Tuple.Create(Column, Row)) && CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey(Tuple.Create(Column - 1, Row)) && CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey(Tuple.Create(Column + 1, Row)))
                    {
                        for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)].Count; i++)
                        {
                            //is found
                            if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i] is Block.SpecialText.TextIs)
                            {
                                //a for loop needed here  
                                int j = 0; MessageBox.Show("to be completed");
                                //left side must be a ThingText
                                if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column - 1, Row)][j] is Block.ThingText)
                                {
                                    //a for loop needed here  
                                    int k = 0; MessageBox.Show("to be completed");
                                    //both left side and right side are ThingText
                                    if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column + 1, Row)][k] is Block.ThingText)
                                    {
                                        //change onething to another
                                        //will this affect the outside for loop? probably not? as the order of the blocks doesn't change? but ChangeThingAToThingB creates a new Map?
                                        ChangeThingAToThingB(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column - 1, Row)][j].GetType().Name, CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column + 1, Row)][k].GetType().Name);
                                    }
                                    if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column + 1, Row)][i] is Block.SpecialText)
                                    {
                                        //change properties of certain blocks
                                        ChangeThingProperty(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column - 1, Row)][j].GetType().Name, CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column + 1, Row)][k].GetType().Name);
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
        private void ChangeThingAToThingB(string ThingA, string ThingB)
        {
            MessageBox.Show("to be completed");
            Map NewMap = new Map();
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    NewMap.PointBlockPairs.Add(Tuple.Create(Column, Row), new List<Block> { });
                }
            }

            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)].Count; i++)
                    {
                        if(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].GetType().Name== ThingA)//wait! one is RockThing,one is RockText,need to figure this out
                        {
                            MessageBox.Show("to be completed");
                            //https://docs.microsoft.com/en-us/dotnet/api/system.activator.createinstance?view=netframework-4.8
                            //NewMap.PointBlockPairs[(Column, Row)].Add(new ThingB);
                        }
                    }
                }
            }


        }
        private void ChangeThingProperty(string Thing, string Property)
        {
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].GetType().Name == Thing)//wait! one is RockThing,one is RockText,need to figure this out
                        {
                            MessageBox.Show("to be completed");
                            //https://docs.microsoft.com/en-us/dotnet/api/system.activator.createinstance?view=netframework-4.8
                            //CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Property = true;
                        }
                    }
                }
            }
        }

        public void UpdateBlocks()//like sink/kill
        {
            //sink
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
                {
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey(Tuple.Create(Column, Row)))
                    {
                        bool ContainsSinkBlock = false;
                        foreach (var SinkBlock in CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)])
                        {
                            if (SinkBlock.IsSink == true) ContainsSinkBlock = true;
                        }
                        if (ContainsSinkBlock)//remove all blocks
                        {
                            CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)] = new List<Block>();
                        }
                    }
                }
            }
            //killblock removes youblock
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
                {
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey(Tuple.Create(Column, Row)))
                    {
                        bool ContainsKillBlock = false;
                        bool ContainsYouBlock = false;
                        foreach (var KillBlock in CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)])
                        {
                            if (KillBlock.IsKill == true) ContainsKillBlock = true;
                        }
                        foreach (var YouBlock in CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)])
                        {
                            if (YouBlock.IsYou == true) ContainsYouBlock = true;
                        }
                        if (ContainsKillBlock && ContainsYouBlock)//remove you block
                        {
                            List<Block> NewList = new List<Block>();
                            foreach (var NotYouBlock in CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)])
                            {
                                if (NotYouBlock.IsYou == false) NewList.Add(NotYouBlock);
                            }
                            CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)] = NewList;
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
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey(Tuple.Create(Column, Row)) == false)
                    {
                        Trace.Write(";");
                    }
                    else
                    {
                        foreach (var block in CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)])
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
