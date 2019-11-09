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

        public Model.Level CurrentLevel;

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

            //!!!for debug use only!!
            CurrentLevel.MapHeight = 20;
            CurrentLevel.MapWidth = 20;


            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    CurrentLevel.CurrentMap.PointBlockPairs.Add(Tuple.Create(Column, Row), new List<Block> { });
                }
            }

            void SafeAddDictionary(Dictionary<Tuple<int, int>, List<Model.Block>> _dict, Tuple<int, int> tuple, List<Model.Block> list)
            {
                if (_dict.ContainsKey(tuple))
                {
                    List<Model.Block> new_list = _dict[tuple];
                    foreach (Model.Block block in list)
                    {
                        new_list.Add(block);
                    }
                }
                else
                {
                    _dict[tuple] = list;
                }
            }

            Dictionary<Tuple<int, int>, List<Model.Block>> dict = CurrentLevel.CurrentMap.PointBlockPairs;

            // vertical columns of grass (left most and right most column)
            for (int i = 0; i < 20; i += 1)
            {
                SafeAddDictionary(dict, Tuple.Create(0, i), new List<Model.Block> { new Model.Block.Thing.Grass() });
                SafeAddDictionary(dict, Tuple.Create(19, i), new List<Model.Block> { new Model.Block.Thing.Grass() });
            }

            // horizontal row of grass (top most and bottom most row)
            for (int i = 0; i < 20; i += 1)
            {
                SafeAddDictionary(dict, Tuple.Create(i, 0), new List<Model.Block> { new Model.Block.Thing.Grass() });
                SafeAddDictionary(dict, Tuple.Create(i, 19), new List<Model.Block> { new Model.Block.Thing.Grass() });
            }

            // wall text
            SafeAddDictionary(dict, Tuple.Create(5, 3), new List<Model.Block> { new Model.Block.ThingText.TextWall() });

            // is text
            SafeAddDictionary(dict, Tuple.Create(6, 3), new List<Model.Block> { new Model.Block.SpecialText.TextIs() });

            // stop text
            SafeAddDictionary(dict, Tuple.Create(7, 3), new List<Model.Block> { new Model.Block.SpecialText.TextStop() });

            // rock text
            SafeAddDictionary(dict, Tuple.Create(12, 3), new List<Model.Block> { new Model.Block.ThingText.TextRock() });

            // is text
            SafeAddDictionary(dict, Tuple.Create(13, 3), new List<Model.Block> { new Model.Block.SpecialText.TextIs() });

            // push text
            SafeAddDictionary(dict, Tuple.Create(14, 3), new List<Model.Block> { new Model.Block.SpecialText.TextPush() });

            // wall thing (a long series of wall)
            for (int i = 4; i < 15; i += 1)
            {
                SafeAddDictionary(dict, Tuple.Create(i, 5), new List<Model.Block> { new Model.Block.Thing.Wall() });
                SafeAddDictionary(dict, Tuple.Create(i, 9), new List<Model.Block> { new Model.Block.Thing.Wall() });
            }

            // fake wall thing
            for (int i = 4; i < 15; i += 1)
            {
                SafeAddDictionary(dict, Tuple.Create(i, 6), new List<Model.Block> { new Model.Block.Thing.FakeWall() });
                if (i != 6 && i != 13)
                {
                    SafeAddDictionary(dict, Tuple.Create(i, 7), new List<Model.Block> { new Model.Block.Thing.FakeWall() });
                }
                SafeAddDictionary(dict, Tuple.Create(i, 8), new List<Model.Block> { new Model.Block.Thing.FakeWall() });
            }

            // baba thing
            SafeAddDictionary(dict, Tuple.Create(6, 7), new List<Model.Block> { new Model.Block.Thing.Baba() });

            // flag thing
            SafeAddDictionary(dict, Tuple.Create(13, 7), new List<Model.Block> { new Model.Block.Thing.Flag() });

            // rock thing
            for (int i = 6; i < 9; i += 1)
            {
                SafeAddDictionary(dict, Tuple.Create(9, i), new List<Model.Block> { new Model.Block.Thing.Rock() });
            }

            // baba text
            SafeAddDictionary(dict, Tuple.Create(5, 11), new List<Model.Block> { new Model.Block.ThingText.TextBaba() });

            // is text
            SafeAddDictionary(dict, Tuple.Create(6, 11), new List<Model.Block> { new Model.Block.SpecialText.TextIs() });

            // you text
            SafeAddDictionary(dict, Tuple.Create(7, 11), new List<Model.Block> { new Model.Block.SpecialText.TextYou() });

            // flag text
            SafeAddDictionary(dict, Tuple.Create(12, 11), new List<Model.Block> { new Model.Block.ThingText.TextFlag() });

            // is text
            SafeAddDictionary(dict, Tuple.Create(13, 11), new List<Model.Block> { new Model.Block.SpecialText.TextIs() });

            // win text
            SafeAddDictionary(dict, Tuple.Create(14, 11), new List<Model.Block> { new Model.Block.SpecialText.TextWin() });

            UpdateRules();//Update rules at the beginning
            AddToHistory();
        }

        public void ArrowKeyDown(string direction)
        {
            switch (direction)//Move blocks accroding to keydown
            {
                case "left": MoveLeft(); break;
                case "right": MoveRight(); break;
                case "up": MoveUp(); break;
                case "down": MoveDown(); break;

            }
            UpdateRules();//Update rules by finding sentences
            UpdateBlocks();//Update blocks, like sink/defeat/kill
            AddToHistory();
            if (CheckWin()) MessageBox.Show("You Win!");
        }

        private void MoveLeft()
        {
            //A new map to store data
            Map NewMap = new Map();
            //CanMove means other blocks can move into this place
            Dictionary<(int, int), bool> CanMove = new Dictionary<(int, int), bool>();
            //ShouldMove means block at this place should move, it's designed for ispush block, not isyou block
            Dictionary<(int, int), bool> ShouldMove = new Dictionary<(int, int), bool>();

            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    NewMap.PointBlockPairs.Add(Tuple.Create(Column, Row), new List<Block>());
                    CanMove.Add((Column, Row), true);//default true
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
                        //if some block is push, whether it can move depend on its left neighbor or if it's on left most column
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsPush)
                        {
                            if (Column == 0)
                            {
                                CanMove[(Column, Row)] = false;
                            }
                            else if (CanMove[(Column - 1, Row)] == false)
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

            //copy left most column into the new map
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(0, Row)].Count; i++)
                {
                    //copy the same element into the new map at same location
                    NewMap.PointBlockPairs[Tuple.Create(0, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(0, Row)][i]);
                }
            }

            CurrentLevel.CurrentMap = NewMap;
        }
        private void MoveRight()
        {
            //A new map to store data
            Map NewMap = new Map();
            //CanMove means other blocks can move into this place
            Dictionary<(int, int), bool> CanMove = new Dictionary<(int, int), bool>();
            //ShouldMove means block at this place should move, it's designed for ispush block, not isyou block
            Dictionary<(int, int), bool> ShouldMove = new Dictionary<(int, int), bool>();

            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    NewMap.PointBlockPairs.Add(Tuple.Create(Column, Row), new List<Block>());
                    CanMove.Add((Column, Row), true);//default true
                    ShouldMove.Add((Column, Row), false);//default false
                }
            }

            //modify CanMove
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = CurrentLevel.MapWidth - 1; Column > 0; Column--) //don't need to check leftmost column, loop from right to left
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
                        //if some block is push, whether it can move depend on its right neighbor or it's at right most column
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsPush)
                        {
                            if (Column == CurrentLevel.MapWidth - 1)
                            {
                                CanMove[(Column, Row)] = false;
                            }
                            else if (CanMove[(Column + 1, Row)] == false)
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
                for (int Column = 0; Column < CurrentLevel.MapWidth - 1; Column++) //don't need to check rightmost column, loop from left to right
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsYou && CanMove[(Column + 1, Row)])
                        {
                            NewMap.PointBlockPairs[Tuple.Create(Column + 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i]);
                            ShouldMove[(Column + 1, Row)] = true;//right block should be pushed(if any)
                        }
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsPush && ShouldMove[(Column, Row)])//Do we need to consider canmove?
                        {
                            NewMap.PointBlockPairs[Tuple.Create(Column + 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i]);
                            ShouldMove[(Column + 1, Row)] = true;//right block should be pushed(if any)
                        }
                        else
                        {
                            //copy the same element into the new map at same location
                            NewMap.PointBlockPairs[Tuple.Create(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i]);
                        }
                    }

                }
            }

            //copy right most column into the new map
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(CurrentLevel.MapWidth - 1, Row)].Count; i++)
                {
                    //copy the same element into the new map at same location
                    NewMap.PointBlockPairs[Tuple.Create(CurrentLevel.MapWidth - 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(CurrentLevel.MapWidth - 1, Row)][i]);
                }
            }

            CurrentLevel.CurrentMap = NewMap;
        }
        private void MoveUp()
        {
            //A new map to store data
            Map NewMap = new Map();
            //CanMove means other blocks can move into this place
            Dictionary<(int, int), bool> CanMove = new Dictionary<(int, int), bool>();
            //ShouldMove means block at this place should move, it's designed for ispush block, not isyou block
            Dictionary<(int, int), bool> ShouldMove = new Dictionary<(int, int), bool>();

            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    NewMap.PointBlockPairs.Add(Tuple.Create(Column, Row), new List<Block>());
                    CanMove.Add((Column, Row), true);//default true
                    ShouldMove.Add((Column, Row), false);//default false
                }
            }

            //modify CanMove
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = 0; Row < CurrentLevel.MapHeight - 1; Row++) //don't need to check downmost column, loop from up to down
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
                        //if some block is push, whether it can move depend on its up neighbor or it's at top row
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsPush)
                        {
                            if (Row == 0)
                            {
                                CanMove[(Column, Row)] = false;
                            }
                            else if (CanMove[(Column, Row - 1)] == false)
                            {
                                CanMove[(Column, Row)] = false;
                            }
                        }
                    }

                }
            }

            //modify should move and move blocks
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = CurrentLevel.MapHeight - 1; Row > 0; Row--) //don't need to check upmost column, loop from down to up
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsYou && CanMove[(Column, Row - 1)])
                        {
                            NewMap.PointBlockPairs[Tuple.Create(Column, Row - 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i]);
                            ShouldMove[(Column, Row - 1)] = true;//up block should be pushed(if any)
                        }
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsPush && ShouldMove[(Column, Row)])//Do we need to consider canmove?
                        {
                            NewMap.PointBlockPairs[Tuple.Create(Column, Row - 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i]);
                            ShouldMove[(Column, Row - 1)] = true;//up block should be pushed(if any)
                        }
                        else
                        {
                            //copy the same element into the new map at same location
                            NewMap.PointBlockPairs[Tuple.Create(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i]);
                        }
                    }

                }
            }

            //copy up most column into the new map
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, 0)].Count; i++)
                {
                    //copy the same element into the new map at same location
                    NewMap.PointBlockPairs[Tuple.Create(Column, 0)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, 0)][i]);
                }
            }

            CurrentLevel.CurrentMap = NewMap;
        }
        private void MoveDown()
        {
            //A new map to store data
            Map NewMap = new Map();
            //CanMove means other blocks can move into this place
            Dictionary<(int, int), bool> CanMove = new Dictionary<(int, int), bool>();
            //ShouldMove means block at this place should move, it's designed for ispush block, not isyou block
            Dictionary<(int, int), bool> ShouldMove = new Dictionary<(int, int), bool>();

            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    NewMap.PointBlockPairs.Add(Tuple.Create(Column, Row), new List<Block>());
                    CanMove.Add((Column, Row), true);//default true
                    ShouldMove.Add((Column, Row), false);//default false
                }
            }

            //modify CanMove
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = CurrentLevel.MapHeight - 1; Row > 0; Row--) //don't need to check downmost column, loop from down to up
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
                        //if some block is push, whether it can move depend on its down neighbor or it's at down most row
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsPush)
                        {
                            if (Row == CurrentLevel.MapHeight - 1)
                            {
                                CanMove[(Column, Row)] = false;
                            }
                            else if (CanMove[(Column, Row + 1)] == false)
                            {
                                CanMove[(Column, Row)] = false;
                            }
                        }
                    }

                }
            }

            //modify should move and move blocks
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = 0; Row < CurrentLevel.MapWidth - 1; Row++) //don't need to check downmost column, loop from up to down
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsYou && CanMove[(Column, Row + 1)])
                        {
                            NewMap.PointBlockPairs[Tuple.Create(Column, Row + 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i]);
                            ShouldMove[(Column, Row + 1)] = true;//down block should be pushed(if any)
                        }
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].IsPush && ShouldMove[(Column, Row)])//Do we need to consider canmove?
                        {
                            NewMap.PointBlockPairs[Tuple.Create(Column, Row + 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i]);
                            ShouldMove[(Column, Row + 1)] = true;//down block should be pushed(if any)
                        }
                        else
                        {
                            //copy the same element into the new map at same location
                            NewMap.PointBlockPairs[Tuple.Create(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i]);
                        }
                    }

                }
            }

            //copy down most column into the new map
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, CurrentLevel.MapHeight - 1)].Count; i++)
                {
                    //copy the same element into the new map at same location
                    NewMap.PointBlockPairs[Tuple.Create(Column, CurrentLevel.MapHeight - 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, CurrentLevel.MapHeight - 1)][i]);
                }
            }

            CurrentLevel.CurrentMap = NewMap;
        }

        private void UpdateRules()//first reset the rules and then find the new rules and apply them
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
                                for (int j = 0; j < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column - 1, Row)].Count; j++)
                                {
                                    //left side must be a ThingText
                                    if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column - 1, Row)][j] is Block.ThingText)
                                    {
                                        for (int k = 0; k < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column + 1, Row)].Count; k++)
                                        {
                                            //both left side and right side are ThingText
                                            if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column + 1, Row)][k] is Block.ThingText)
                                            {
                                                //change onething to another
                                                //will this affect the outside for loop? probably not? as the order of the blocks doesn't change? but ChangeThingAToThingB creates a new Map?
                                                ChangeThingAToThingB(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column - 1, Row)][j].GetType().Name, CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column + 1, Row)][k].GetType().Name);
                                            }
                                            //left is thingtext right in proptext
                                            if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column + 1, Row)][k] is Block.SpecialText)
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
                }
            }
            //find rules in same column
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                //find is in [1,CurrentLevel.CurrentMap.MapHeight-2]
                for (int Row = 1; Row < CurrentLevel.MapHeight - 1; Row++)
                {
                    //must have three words to form a sentence
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey(Tuple.Create(Column, Row)) && CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey(Tuple.Create(Column, Row - 1)) && CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey(Tuple.Create(Column, Row + 1)))
                    {
                        for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)].Count; i++)
                        {
                            //is found
                            if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i] is Block.SpecialText.TextIs)
                            {
                                for (int j = 0; j < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row - 1)].Count; j++)
                                {
                                    //up side must be a ThingText
                                    if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row - 1)][j] is Block.ThingText)
                                    {
                                        for (int k = 0; k < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row + 1)].Count; k++)
                                        {
                                            //both up side and down side are ThingText
                                            if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row + 1)][k] is Block.ThingText)
                                            {
                                                //change onething to another
                                                //will this affect the outside for loop? probably not? as the order of the blocks doesn't change? but ChangeThingAToThingB creates a new Map?
                                                ChangeThingAToThingB(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row - 1)][j].GetType().Name, CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row + 1)][k].GetType().Name);
                                            }
                                            //up is thingtext down in proptext
                                            if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row + 1)][k] is Block.SpecialText)
                                            {
                                                //change properties of certain blocks
                                                ChangeThingProperty(CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row - 1)][j].GetType().Name, CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row + 1)][k].GetType().Name);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
        private void ChangeThingAToThingB(string ThingA, string ThingB)
        {
            MessageBox.Show("to be completed");

            //remember first change thinga to thing b then apply properties need to fix this in update rules


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
                        if (CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i].GetType().Name == ThingA)//wait! one is RockThing,one is RockText,need to figure this out
                        {
                            MessageBox.Show("to be completed");
                            //https://docs.microsoft.com/en-us/dotnet/api/system.activator.createinstance?view=netframework-4.8
                            //NewMap.PointBlockPairs[(Column, Row)].Add(new ThingB);
                        }
                    }
                }
            }


        }
        private void ChangeThingProperty(string ThingText, string Property)
        {
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)].Count; i++)
                    {
                        Block block = CurrentLevel.CurrentMap.PointBlockPairs[Tuple.Create(Column, Row)][i];

                        if (block.GetType().Name == ThingText.Replace("Text", ""))//change TextRock to Rock
                        {
                            Trace.WriteLine(Property);
                            //https://stackoverflow.com/questions/619767/set-object-property-using-reflection/619778

                            //change TextStop to IsStop  
                            var prop = block.GetType().GetProperty(Property.Replace("Text", "Is"));
                            //textis will be isis so need to check if prop exists
                            if (prop != null)
                            {
                                prop.SetValue(block, true, null);//why need null?
                            }
                        }
                    }
                }
            }
        }
        private void UpdateBlocks()//like sink/kill
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
        private bool CheckWin()
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
        private void AddToHistory()
        {
            CurrentLevel.History.Add(CurrentLevel.CurrentMap);
        }
        public void GoBack()
        {
            if (CurrentLevel.History.Count > 1)
            {
                CurrentLevel.History.RemoveAt(CurrentLevel.History.Count - 1);
                CurrentLevel.CurrentMap = CurrentLevel.History[CurrentLevel.History.Count - 1];
                UpdateRules();
            }
        }
        public void Restart()
        {
            CurrentLevel.CurrentMap = CurrentLevel.History[0];
            AddToHistory();
            UpdateRules();
        }
        private void DrawInTrace()
        {
            Trace.WriteLine("Begin Debug Draw");
            Trace.WriteLine(CurrentLevel.MapHeight);
            Trace.WriteLine(CurrentLevel.MapWidth);
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
            Trace.WriteLine("End Debug Draw");

        }

    }
}
