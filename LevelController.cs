using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabaIsYou.Model;
using System.Windows.Input;
using System.Diagnostics;
namespace BabaIsYou.Controller
{
    class LevelController
    {

        public Model.Level CurrentLevel;

        public LevelController(int startlevel)
        {
            CurrentLevel = new Model.Level(startlevel);
        }
        private void ClearMap()
        {
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)] = new List<Block>();
                }
            }
        }

        public void LoadGame()
        {
            ClearMap();
            CurrentLevel.History = new List<Map>();
            //load blocks from data into CurrentMap
            switch (CurrentLevel.LevelNumber)
            {
                case 0: GetLevelDictionary.GetLevelZeroDictionary(CurrentLevel); break;
                case 1: GetLevelDictionary.GetLevelOneDictionary(CurrentLevel); break;
                case 2: GetLevelDictionary.GetLevelTwoDictionary(CurrentLevel); break;
                case 3: GetLevelDictionary.GetLevelThreeDictionary(CurrentLevel); break;
                case 4: GetLevelDictionary.GetLevelFourDictionary(CurrentLevel); break;
                case 5: GetLevelDictionary.GetLevelFiveDictionary(CurrentLevel); break;
                case 6: GetLevelDictionary.GetLevelSixDictionary(CurrentLevel); break;
                case 7: GetLevelDictionary.GetLevelSevenDictionary(CurrentLevel); break;
                case 8: GetLevelDictionary.GetLevelEightDictionary(CurrentLevel); break;
                case 9: GetLevelDictionary.GetLevelNineDictionary(CurrentLevel); break;
                case 10: GetLevelDictionary.GetLevelTenDictionary(CurrentLevel); break;
                case 11: GetLevelDictionary.GetLevelElevenDictionary(CurrentLevel); break;
                case 12: GetLevelDictionary.GetLevelTwelveDictionary(CurrentLevel); break;
                case 13: GetLevelDictionary.GetLevelThirteenDictionary(CurrentLevel); break;
                default: GetLevelDictionary.GetLevelZeroDictionary(CurrentLevel); CurrentLevel.LevelNumber = 0; break;
            }

            UpdateRules();//Update rules at the beginning
            AddToHistory();
        }
        public void KeyDown(Key InputKey)
        {
            if (InputKey == Key.Z)
            {
                GoBack();
            }
            else if (InputKey == Key.R)
            {
                Restart();
            }
            else if (InputKey == Key.Up || InputKey == Key.Down || InputKey == Key.Left || InputKey == Key.Right || InputKey == Key.Space)
            {
                //move isyou block, ismove block
                MoveUp(InputKey);
                MoveDown(InputKey);
                MoveLeft(InputKey);
                MoveRight(InputKey);
                UpdateRules();//Update rules by finding sentences
                UpdateBlocks();//Update blocks, like sink/defeat/kill
                AddToHistory();
                CheckWin();
            }
        }

        //when on slip, check if facing next place contains stop
        private bool OnSlipFacingContainStop(string Facing, int Column, int Row, Dictionary<(int, int), bool> ContainStop)
        {
            if (Facing == "up") return ContainStop[(Column, Row - 1)];
            else if (Facing == "down") return ContainStop[(Column, Row + 1)];
            else if (Facing == "left") return ContainStop[(Column - 1, Row)];
            else /*(Facing == "right")*/ return ContainStop[(Column + 1, Row)];
        }

        private void MoveLeft(Key InputKey)
        {
            //A new map to store data
            Map NewMap = new Map();
            //CanMove means other blocks can move into this place
            Dictionary<(int, int), bool> CanMove = new Dictionary<(int, int), bool>();
            //ShouldMove means block at this place should move, it's designed for ispush block, not isyou block
            Dictionary<(int, int), bool> ShouldMove = new Dictionary<(int, int), bool>();
            //ContainSlip means there's slip block at this place
            Dictionary<(int, int), bool> ContainSlip = new Dictionary<(int, int), bool>();
            //ContainStop means there's stop block at this place, this can stop isslip continuing
            Dictionary<(int, int), bool> ContainStop = new Dictionary<(int, int), bool>();

            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    NewMap.PointBlockPairs.Add((Column, Row), new List<Block>());
                    CanMove.Add((Column, Row), true);//default true
                    ShouldMove.Add((Column, Row), false);//default false
                    ContainSlip.Add((Column, Row), false);//default false
                    ContainStop.Add((Column, Row), false);//default false
                }
            }

            //modify CanMove
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth - 1; Column++) //don't need to check rightmost column, loop from left to right
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        //if someblock is not you/push and is stop, others cannot move into this place
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsStop && (!CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsYou && !CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush))
                        {
                            CanMove[(Column, Row)] = false;
                            //other block at this point should not change this result
                            break;
                        }
                        //if some block is push, whether it can move depend on its left neighbor or if it's on left most column
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush)
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

            //modify ContainSlip
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsSlip)
                        {
                            ContainSlip[(Column, Row)] = true;
                            break;
                        }
                    }
                }
            }

            //modify ContainStop
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsStop)
                        {
                            ContainStop[(Column, Row)] = true;
                            break;
                        }
                    }
                }
            }

            //modify should move and move blocks
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = CurrentLevel.MapWidth - 1; Column > 0; Column--) //don't need to check leftmost column, loop from right to left
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsYou && CanMove[(Column - 1, Row)])
                        {
                            if (ContainSlip[(Column, Row)] && OnSlipFacingContainStop(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing, Column, Row, ContainStop) && InputKey == Key.Left)
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing = "left";
                                NewMap.PointBlockPairs[(Column - 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column - 1, Row)] = true;//left block should be pushed(if any)
                            }
                            else if (ContainSlip[(Column, Row)] && OnSlipFacingContainStop(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing, Column, Row, ContainStop) == false && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing == "left")
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Slipped = true;
                                NewMap.PointBlockPairs[(Column - 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column - 1, Row)] = true;//left block should be pushed(if any)
                            }
                            //only when not slipped, isblock can move according to keydown
                            else if (ContainSlip[(Column, Row)] == false && InputKey == Key.Left && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Slipped == false)
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing = "left";
                                NewMap.PointBlockPairs[(Column - 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column - 1, Row)] = true;//left block should be pushed(if any)                               
                            }
                            //!!I forgot this else at first. Sigh
                            else
                            {
                                //copy the same element into the new map at same location
                                NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            }
                        }
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsMove)
                        {
                            if (CanMove[(Column - 1, Row)] && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing == "left")
                            {
                                NewMap.PointBlockPairs[(Column - 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column - 1, Row)] = true;//left block should be pushed(if any)
                            }
                            else if (CanMove[(Column - 1, Row)] == false && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing == "left")//cannot move change facing
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing = "right";
                                NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            }
                            //!!I forgot this else case at first. Sad
                            else //wrong facing ignore
                            {
                                NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            }
                        }
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush && ShouldMove[(Column, Row)])//Do we need to consider canmove?
                        {
                            NewMap.PointBlockPairs[(Column - 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            ShouldMove[(Column - 1, Row)] = true;//left block should be pushed(if any)
                        }
                        else
                        {
                            //copy the same element into the new map at same location
                            NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                        }
                    }
                }
            }

            //copy left most column into the new map
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(0, Row)].Count; i++)
                {
                    //copy the same element into the new map at same location
                    NewMap.PointBlockPairs[(0, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(0, Row)][i]);
                }
            }

            CurrentLevel.CurrentMap = NewMap;
        }
        private void MoveRight(Key InputKey)
        {
            //A new map to store data
            Map NewMap = new Map();
            //CanMove means other blocks can move into this place
            Dictionary<(int, int), bool> CanMove = new Dictionary<(int, int), bool>();
            //ShouldMove means block at this place should move, it's designed for ispush block, not isyou block
            Dictionary<(int, int), bool> ShouldMove = new Dictionary<(int, int), bool>();
            //ContainSlip means there's slip block at this place
            Dictionary<(int, int), bool> ContainSlip = new Dictionary<(int, int), bool>();
            //ContainStop means there's stop block at this place, this can stop isslip continuing
            Dictionary<(int, int), bool> ContainStop = new Dictionary<(int, int), bool>();


            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    NewMap.PointBlockPairs.Add((Column, Row), new List<Block>());
                    CanMove.Add((Column, Row), true);//default true
                    ShouldMove.Add((Column, Row), false);//default false
                    ContainSlip.Add((Column, Row), false);//default false
                    ContainStop.Add((Column, Row), false);//default false
                }
            }

            //modify CanMove
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = CurrentLevel.MapWidth - 1; Column > 0; Column--) //don't need to check leftmost column, loop from right to left
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        //if someblock is not you/push and is stop, others cannot move into this place
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsStop && (!CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsYou && !CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush))
                        {
                            CanMove[(Column, Row)] = false;
                            //other block at this point should not change this result
                            break;
                        }
                        //if some block is push, whether it can move depend on its right neighbor or it's at right most column
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush)
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

            //modify ContainSlip
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsSlip)
                        {
                            ContainSlip[(Column, Row)] = true;
                            break;
                        }
                    }
                }
            }

            //modify ContainStop
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsStop)
                        {
                            ContainStop[(Column, Row)] = true;
                            break;
                        }
                    }
                }
            }

            //modify should move and move blocks
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth - 1; Column++) //don't need to check rightmost column, loop from left to right
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsYou && CanMove[(Column + 1, Row)])
                        {
                            if (ContainSlip[(Column, Row)] && OnSlipFacingContainStop(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing, Column, Row, ContainStop) && InputKey == Key.Right)
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing = "right";
                                NewMap.PointBlockPairs[(Column + 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column + 1, Row)] = true;//right block should be pushed(if any)
                            }
                            else if (ContainSlip[(Column, Row)] && OnSlipFacingContainStop(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing, Column, Row, ContainStop) == false && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing == "right")
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Slipped = true;
                                NewMap.PointBlockPairs[(Column + 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column + 1, Row)] = true;//right block should be pushed(if any)
                            }
                            else if (ContainSlip[(Column, Row)] == false && InputKey == Key.Right && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Slipped == false)
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing = "right";
                                NewMap.PointBlockPairs[(Column + 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column + 1, Row)] = true;//right block should be pushed(if any)
                            }
                            else
                            {
                                //copy the same element into the new map at same location
                                NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            }
                        }
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsMove)
                        {
                            if (CanMove[(Column + 1, Row)] && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing == "right")
                            {
                                NewMap.PointBlockPairs[(Column + 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column + 1, Row)] = true;//right block should be pushed(if any)
                            }
                            else if (CanMove[(Column + 1, Row)] == false && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing == "right")//cannot move change facing
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing = "left";
                                NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            }
                            else //wrong facing ignore
                            {
                                NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            }
                        }
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush && ShouldMove[(Column, Row)])//Do we need to consider canmove?
                        {
                            NewMap.PointBlockPairs[(Column + 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            ShouldMove[(Column + 1, Row)] = true;//right block should be pushed(if any)
                        }
                        else
                        {
                            //copy the same element into the new map at same location
                            NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                        }
                    }
                }
            }

            //copy right most column into the new map
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(CurrentLevel.MapWidth - 1, Row)].Count; i++)
                {
                    //copy the same element into the new map at same location
                    NewMap.PointBlockPairs[(CurrentLevel.MapWidth - 1, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(CurrentLevel.MapWidth - 1, Row)][i]);
                }
            }

            CurrentLevel.CurrentMap = NewMap;
        }
        private void MoveUp(Key InputKey)
        {
            //A new map to store data
            Map NewMap = new Map();
            //CanMove means other blocks can move into this place
            Dictionary<(int, int), bool> CanMove = new Dictionary<(int, int), bool>();
            //ShouldMove means block at this place should move, it's designed for ispush block, not isyou block
            Dictionary<(int, int), bool> ShouldMove = new Dictionary<(int, int), bool>();
            //ContainSlip means there's slip block at this place
            Dictionary<(int, int), bool> ContainSlip = new Dictionary<(int, int), bool>();
            //ContainStop means there's stop block at this place, this can stop isslip continuing
            Dictionary<(int, int), bool> ContainStop = new Dictionary<(int, int), bool>();

            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    NewMap.PointBlockPairs.Add((Column, Row), new List<Block>());
                    CanMove.Add((Column, Row), true);//default true
                    ShouldMove.Add((Column, Row), false);//default false
                    ContainSlip.Add((Column, Row), false);//default false
                    ContainStop.Add((Column, Row), false);//default false
                }
            }

            //modify CanMove
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = 0; Row < CurrentLevel.MapHeight - 1; Row++) //don't need to check downmost column, loop from up to down
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        //if someblock is not you/push and is stop, others cannot move into this place
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsStop && (!CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsYou && !CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush))
                        {
                            CanMove[(Column, Row)] = false;
                            //other block at this point should not change this result
                            break;
                        }
                        //if some block is push, whether it can move depend on its up neighbor or it's at top row
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush)
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

            //modify ContainSlip
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsSlip)
                        {
                            ContainSlip[(Column, Row)] = true;
                            break;
                        }
                    }
                }
            }

            //modify ContainStop
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsStop)
                        {
                            ContainStop[(Column, Row)] = true;
                            break;
                        }
                    }
                }
            }

            //modify should move and move blocks
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = CurrentLevel.MapHeight - 1; Row > 0; Row--) //don't need to check upmost column, loop from down to up
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {

                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsYou && CanMove[(Column, Row - 1)])
                        {
                            if (ContainSlip[(Column, Row)] && OnSlipFacingContainStop(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing, Column, Row, ContainStop) && InputKey == Key.Up)
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing = "up";
                                NewMap.PointBlockPairs[(Column, Row - 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column, Row - 1)] = true;//up block should be pushed(if any)
                            }
                            else if (ContainSlip[(Column, Row)] && OnSlipFacingContainStop(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing, Column, Row, ContainStop) == false && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing == "up")
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Slipped = true;
                                NewMap.PointBlockPairs[(Column, Row - 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column, Row - 1)] = true;//up block should be pushed(if any)
                            }
                            else if (ContainSlip[(Column, Row)] == false && InputKey == Key.Up && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Slipped == false)
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing = "up";
                                NewMap.PointBlockPairs[(Column, Row - 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column, Row - 1)] = true;//up block should be pushed(if any)
                            }
                            else
                            {
                                //copy the same element into the new map at same location
                                NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            }
                        }
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsMove)
                        {
                            if (CanMove[(Column, Row - 1)] && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing == "up")
                            {

                                NewMap.PointBlockPairs[(Column, Row - 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column, Row - 1)] = true;//up block should be pushed(if any)
                            }
                            else if (CanMove[(Column, Row - 1)] == false && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing == "up")//cannot move change facing
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing = "down";
                                NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            }
                            else //wrong facing ignore
                            {
                                NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            }
                        }

                        else if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush && ShouldMove[(Column, Row)])//Do we need to consider canmove?
                        {
                            NewMap.PointBlockPairs[(Column, Row - 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            ShouldMove[(Column, Row - 1)] = true;//up block should be pushed(if any)
                        }
                        else
                        {
                            //copy the same element into the new map at same location
                            NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                        }
                    }

                }
            }

            //copy up most column into the new map
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, 0)].Count; i++)
                {
                    //copy the same element into the new map at same location
                    NewMap.PointBlockPairs[(Column, 0)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, 0)][i]);
                }
            }

            CurrentLevel.CurrentMap = NewMap;
        }
        private void MoveDown(Key InputKey)
        {
            //A new map to store data
            Map NewMap = new Map();
            //CanMove means other blocks can move into this place
            Dictionary<(int, int), bool> CanMove = new Dictionary<(int, int), bool>();
            //ShouldMove means block at this place should move, it's designed for ispush block, not isyou block
            Dictionary<(int, int), bool> ShouldMove = new Dictionary<(int, int), bool>();
            //ContainSlip means there's slip block at this place
            Dictionary<(int, int), bool> ContainSlip = new Dictionary<(int, int), bool>();
            //ContainStop means there's stop block at this place, this can stop isslip continuing
            Dictionary<(int, int), bool> ContainStop = new Dictionary<(int, int), bool>();

            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    NewMap.PointBlockPairs.Add((Column, Row), new List<Block>());
                    CanMove.Add((Column, Row), true);//default true
                    ShouldMove.Add((Column, Row), false);//default false
                    ContainSlip.Add((Column, Row), false);//default false
                    ContainStop.Add((Column, Row), false);//default false
                }
            }

            //modify CanMove
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = CurrentLevel.MapHeight - 1; Row > 0; Row--) //don't need to check downmost column, loop from down to up
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        //if someblock is not you/push and is stop, others cannot move into this place
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsStop && (!CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsYou && !CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush))
                        {
                            CanMove[(Column, Row)] = false;
                            //other block at this point should not change this result
                            break;
                        }
                        //if some block is push, whether it can move depend on its down neighbor or it's at down most row
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush)
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
            //modify ContainSlip
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsSlip)
                        {
                            ContainSlip[(Column, Row)] = true;
                            break;
                        }
                    }
                }
            }

            //modify ContainStop
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsStop)
                        {
                            ContainStop[(Column, Row)] = true;
                            break;
                        }
                    }
                }
            }


            //modify should move and move blocks
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = 0; Row < CurrentLevel.MapHeight - 1; Row++) //don't need to check downmost column, loop from up to down
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsYou && CanMove[(Column, Row + 1)])
                        {
                            if (ContainSlip[(Column, Row)] && OnSlipFacingContainStop(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing, Column, Row, ContainStop) && InputKey == Key.Down)
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing = "down";
                                NewMap.PointBlockPairs[(Column, Row + 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column, Row + 1)] = true;//down block should be pushed(if any)
                            }
                            else if (ContainSlip[(Column, Row)] && OnSlipFacingContainStop(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing, Column, Row, ContainStop) == false && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing == "down")
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Slipped = true;
                                NewMap.PointBlockPairs[(Column, Row + 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column, Row + 1)] = true;//down block should be pushed(if any)
                            }
                            else if (ContainSlip[(Column, Row)] == false && InputKey == Key.Down && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Slipped == false)
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing = "down";
                                NewMap.PointBlockPairs[(Column, Row + 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column, Row + 1)] = true;//down block should be pushed(if any)
                            }
                            else
                            {
                                //copy the same element into the new map at same location
                                NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            }
                        }
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsMove)
                        {
                            if (CanMove[(Column, Row + 1)] && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing == "down")
                            {
                                NewMap.PointBlockPairs[(Column, Row + 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                                ShouldMove[(Column, Row + 1)] = true;//down block should be pushed(if any)
                            }
                            else if (CanMove[(Column, Row + 1)] == false && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing == "down")//cannot move change facing
                            {
                                CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].Facing = "up";
                                NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            }
                            else //wrong facing ignore
                            {
                                NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            }
                        }
                        else if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].IsPush && ShouldMove[(Column, Row)])//Do we need to consider canmove?
                        {
                            NewMap.PointBlockPairs[(Column, Row + 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                            ShouldMove[(Column, Row + 1)] = true;//down block should be pushed(if any)
                        }
                        else
                        {
                            //copy the same element into the new map at same location
                            NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                        }
                    }

                }
            }

            //copy down most column into the new map
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, CurrentLevel.MapHeight - 1)].Count; i++)
                {
                    //copy the same element into the new map at same location
                    NewMap.PointBlockPairs[(Column, CurrentLevel.MapHeight - 1)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, CurrentLevel.MapHeight - 1)][i]);
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
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey((Column, Row)))
                    {
                        for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                        {
                            CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].ReturnToDefault();
                        }
                    }
                }
            }
            //find Thing A is Thing B in same row
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                //find is in [1,CurrentLevel.CurrentMap.MapWidth-2]
                for (int Column = 1; Column < CurrentLevel.MapWidth - 1; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        //is found
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i] is Block.SpecialText.TextIs)
                        {
                            for (int j = 0; j < CurrentLevel.CurrentMap.PointBlockPairs[(Column - 1, Row)].Count; j++)
                            {
                                //left side must be a ThingText
                                if (CurrentLevel.CurrentMap.PointBlockPairs[(Column - 1, Row)][j] is Block.ThingText)
                                {
                                    for (int k = 0; k < CurrentLevel.CurrentMap.PointBlockPairs[(Column + 1, Row)].Count; k++)
                                    {
                                        //both left side and right side are ThingText
                                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column + 1, Row)][k] is Block.ThingText)
                                        {
                                            //change onething to another
                                            //will this affect the outside for loop? probably not? as the order of the blocks doesn't change? but ChangeThingAToThingB creates a new Map?
                                            ChangeThingAToThingB(CurrentLevel.CurrentMap.PointBlockPairs[(Column - 1, Row)][j].GetType().Name, CurrentLevel.CurrentMap.PointBlockPairs[(Column + 1, Row)][k].GetType().Name);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //find Thing A is Thing B in same column
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                //find is in [1,CurrentLevel.CurrentMap.MapHeight-2]
                for (int Row = 1; Row < CurrentLevel.MapHeight - 1; Row++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        //is found
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i] is Block.SpecialText.TextIs)
                        {
                            for (int j = 0; j < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row - 1)].Count; j++)
                            {
                                //up side must be a ThingText
                                if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row - 1)][j] is Block.ThingText)
                                {
                                    for (int k = 0; k < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row + 1)].Count; k++)
                                    {
                                        //both up side and down side are ThingText
                                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row + 1)][k] is Block.ThingText)
                                        {
                                            //change onething to another
                                            //will this affect the outside for loop? probably not? as the order of the blocks doesn't change? but ChangeThingAToThingB creates a new Map?
                                            ChangeThingAToThingB(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row - 1)][j].GetType().Name, CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row + 1)][k].GetType().Name);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //find Thing A is some property in same row
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                //find is in [1,CurrentLevel.CurrentMap.MapWidth-2]
                for (int Column = 1; Column < CurrentLevel.MapWidth - 1; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        //is found
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i] is Block.SpecialText.TextIs)
                        {
                            for (int j = 0; j < CurrentLevel.CurrentMap.PointBlockPairs[(Column - 1, Row)].Count; j++)
                            {
                                //left side must be a ThingText
                                if (CurrentLevel.CurrentMap.PointBlockPairs[(Column - 1, Row)][j] is Block.ThingText)
                                {
                                    for (int k = 0; k < CurrentLevel.CurrentMap.PointBlockPairs[(Column + 1, Row)].Count; k++)
                                    {
                                        //left is thingtext right in proptext
                                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column + 1, Row)][k] is Block.SpecialText)
                                        {
                                            //change properties of certain blocks
                                            ChangeThingProperty(CurrentLevel.CurrentMap.PointBlockPairs[(Column - 1, Row)][j].GetType().Name, CurrentLevel.CurrentMap.PointBlockPairs[(Column + 1, Row)][k].GetType().Name);
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            //find Thing A is some property in same column
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                //find is in [1,CurrentLevel.CurrentMap.MapHeight-2]
                for (int Row = 1; Row < CurrentLevel.MapHeight - 1; Row++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        //is found
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i] is Block.SpecialText.TextIs)
                        {
                            for (int j = 0; j < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row - 1)].Count; j++)
                            {
                                //up side must be a ThingText
                                if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row - 1)][j] is Block.ThingText)
                                {
                                    for (int k = 0; k < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row + 1)].Count; k++)
                                    {
                                        //up is thingtext down in proptext
                                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row + 1)][k] is Block.SpecialText)
                                        {
                                            //change properties of certain blocks
                                            ChangeThingProperty(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row - 1)][j].GetType().Name, CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row + 1)][k].GetType().Name);
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
            Map NewMap = new Map();
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    NewMap.PointBlockPairs.Add((Column, Row), new List<Block> { });
                }
            }

            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        if (CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i].GetType().Name == ThingA.Replace("Text", ""))
                        {
                            //https://docs.microsoft.com/en-us/dotnet/api/system.activator.createinstance?view=netframework-4.8
                            //hard-coding is not very nice
                            //should be fixed in future
                            Block newblk = (BabaIsYou.Model.Block)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance("BabaIsYou.Model.Block+Thing+" + ThingB.Replace("Text", ""));
                            NewMap.PointBlockPairs[(Column, Row)].Add(newblk);
                        }
                        else
                        {
                            NewMap.PointBlockPairs[(Column, Row)].Add(CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i]);
                        }
                    }
                }
            }
            CurrentLevel.CurrentMap = NewMap;
        }
        private void ChangeThingProperty(string ThingText, string Property)
        {
            for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
            {
                for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
                {
                    for (int i = 0; i < CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count; i++)
                    {
                        Block block = CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][i];

                        if (block.GetType().Name == ThingText.Replace("Text", ""))//change TextRock to Rock
                        {
                            //Trace.WriteLine(Property);
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
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey((Column, Row)))
                    {
                        bool ContainsSinkBlock = false;
                        foreach (var SinkBlock in CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)])
                        {
                            if (SinkBlock.IsSink == true) ContainsSinkBlock = true;
                        }
                        //remove all blocks when there's block other than sink block
                        if (ContainsSinkBlock && CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count > 1)
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
            //hotblocks removes meltblocks
            for (int Column = 0; Column < CurrentLevel.MapWidth; Column++)
            {
                for (int Row = 0; Row < CurrentLevel.MapHeight; Row++)
                {
                    if (CurrentLevel.CurrentMap.PointBlockPairs.ContainsKey((Column, Row)))
                    {
                        bool ContainsHotBlock = false;
                        bool ContainsMeltBlock = false;
                        foreach (var HotBlock in CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)])
                        {
                            if (HotBlock.IsHot == true) ContainsHotBlock = true;
                        }
                        foreach (var MeltBlock in CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)])
                        {
                            if (MeltBlock.IsMelt == true) ContainsMeltBlock = true;
                        }
                        if (ContainsHotBlock && ContainsMeltBlock)//remove melt block
                        {
                            List<Block> NewList = new List<Block>();
                            foreach (var NotMeltBlock in CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)])
                            {
                                if (NotMeltBlock.IsMelt == false) NewList.Add(NotMeltBlock);
                            }
                            CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)] = NewList;
                        }
                    }
                }
            }
        }
        private void CheckWin()
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
                if (ContainsWinBlock && ContainsYouBlock)
                {
                    CurrentLevel.LevelNumber++;
                    LoadGame();
                    return;
                }
            }
        }
        private void AddToHistory()
        {
            CurrentLevel.History.Add(CurrentLevel.CurrentMap);
        }
        private void GoBack()
        {
            if (CurrentLevel.History.Count > 1)
            {
                CurrentLevel.History.RemoveAt(CurrentLevel.History.Count - 1);
                CurrentLevel.CurrentMap = CurrentLevel.History[CurrentLevel.History.Count - 1];
                UpdateRules();
            }
        }
        private void Restart()
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
            Trace.WriteLine("End Debug Draw");

        }

    }
}
