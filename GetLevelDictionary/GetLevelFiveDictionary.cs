using BabaIsYou.Model;
using System.Collections.Generic;

partial class GetLevelDictionary
{
    public static void GetLevelFiveDictionary(Level currentLevel)
    {
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        Tools.AddBorder(dict);

        // rock text
        SafeDictionary.Add(dict, (1, 1), new List<Block> { new Block.ThingText.TextRock() });

        // is text
        SafeDictionary.Add(dict, (2, 1), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (3, 1), new List<Block> { new Block.SpecialText.TextStop() });

        // bone text
        SafeDictionary.Add(dict, (1, 3), new List<Block> { new Block.ThingText.TextBone() });

        // is text
        SafeDictionary.Add(dict, (2, 3), new List<Block> { new Block.SpecialText.TextIs() });

        // kill text
        SafeDictionary.Add(dict, (3, 3), new List<Block> { new Block.SpecialText.TextKill() });

        // flag text
        SafeDictionary.Add(dict, (1, 5), new List<Block> { new Block.ThingText.TextFlag() });

        // is text
        SafeDictionary.Add(dict, (2, 5), new List<Block> { new Block.SpecialText.TextIs() });

        // win text
        SafeDictionary.Add(dict, (3, 5), new List<Block> { new Block.SpecialText.TextWin() });

        // rock thing
        for (int i = 1; i < 5; i += 1)
        {
            SafeDictionary.Add(dict, (i, 6), new List<Block> { new Block.Thing.Rock() });
        }

        for (int i = 1; i < 6; i += 1)
        {
            SafeDictionary.Add(dict, (4, i), new List<Block> { new Block.Thing.Rock() });
        }

        // TODO: wall thing (main wall)
        for (int i = 8; i < 13; i += 1)
        {
            SafeDictionary.Add(dict, (4, i), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 5; i < 8; i += 1)
        {
            SafeDictionary.Add(dict, (i, 8), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (i, 12), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 3; i < 16; i += 1)
        {
            if (i != 10)
            {
                SafeDictionary.Add(dict, (8, i), new List<Block> { new Block.Thing.Wall() });
            }
            if (i != 5 && i != 9)
            {
                SafeDictionary.Add(dict, (13, i), new List<Block> { new Block.Thing.Wall() });
            }
        }

        // TO DELETE COMMENT: row
        for (int i = 9; i < 13; i += 1)
        {
            if (i != 10)
            {
                SafeDictionary.Add(dict, (i, 3), new List<Block> { new Block.Thing.Wall() });
            }
            if (i > 10)
            {
                SafeDictionary.Add(dict, (i, 8), new List<Block> { new Block.Thing.Wall() });
            }
            SafeDictionary.Add(dict, (i, 15), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 14; i < 18; i += 1)
        {
            SafeDictionary.Add(dict, (i, 3), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (i, 7), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 4; i < 7; i += 1)
        {
            SafeDictionary.Add(dict, (17, i), new List<Block> { new Block.Thing.Wall() });
        }

        // fake wall thing
        for (int i = 9; i < 12; i += 1)
        {
            for (int j = 5; j < 8; j += 1)
            {
                if (i != 10 || j != 6)
                {
                    SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.FakeWall() });
                }
            }
        }

        // baba thing
        SafeDictionary.Add(dict, (6, 10), new List<Block> { new Block.Thing.Baba() });

        // fake wall thing
        for (int i = 4; i < 7; i += 1)
        {
            for (int j = 14; j < 17; j += 1)
            {
                if (i != 5 || j != 15)
                {
                    SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.FakeWall() });
                }
            }
        }

        // flag thing
        SafeDictionary.Add(dict, (15, 5), new List<Block> { new Block.Thing.Flag() });

        // bone thing
        for (int i = 1; i < 10; i += 1)
        {
            SafeDictionary.Add(dict, (10, i), new List<Block> { new Block.Thing.Bone() });
        }

        for (int i = 11; i < 19; i += 1)
        {
            SafeDictionary.Add(dict, (i, 9), new List<Block> { new Block.Thing.Bone() });
        }

        // baba text
        SafeDictionary.Add(dict, (5, 13), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (6, 13), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (7, 13), new List<Block> { new Block.SpecialText.TextYou() });

        // wall text
        SafeDictionary.Add(dict, (10, 13), new List<Block> { new Block.ThingText.TextWall() });

        // is text
        SafeDictionary.Add(dict, (11, 13), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (12, 13), new List<Block> { new Block.SpecialText.TextStop() });


        /*
        // rock thing (all rocks)
        SafeDictionary.Add(dict, (4, 3), new List<Block> { new Block.Thing.Rock() });
        SafeDictionary.Add(dict, (8, 3), new List<Block> { new Block.Thing.Rock() });
        SafeDictionary.Add(dict, (10, 4), new List<Block> { new Block.Thing.Rock() });
        SafeDictionary.Add(dict, (16, 2), new List<Block> { new Block.Thing.Rock() });
        SafeDictionary.Add(dict, (6, 8), new List<Block> { new Block.Thing.Rock() });
        SafeDictionary.Add(dict, (13, 16), new List<Block> { new Block.Thing.Rock() });

        // wall (main wall)
        for (int i = 2; i < 7; i += 1)
        {
            SafeDictionary.Add(dict, (i, 6), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (i, 10), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 7; i < 10; i += 1)
        {
            SafeDictionary.Add(dict, (2, i), new List<Block> { new Block.Thing.Wall() });
            if (i != 8)
            {
                SafeDictionary.Add(dict, (6, i), new List<Block> { new Block.Thing.Wall() });
            }
           
        }

        // fake wall thing
        for (int i = 7; i < 10; i += 1)
        {
            for (int j = 3; j < 6; j += 1)
            {
                if (i != 8 || j != 4)
                {
                    SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.FakeWall() });
                }
            }
        }


        // lava thing
        for (int i = 11; i < 16; i += 1)
        {
            SafeDictionary.Add(dict, (i, 6), new List<Block> { new Block.Thing.Lava() });
            SafeDictionary.Add(dict, (i, 10), new List<Block> { new Block.Thing.Lava() });
        }

        for (int i = 7; i < 10; i += 1)
        {
            SafeDictionary.Add(dict, (11, i), new List<Block> { new Block.Thing.Lava() });
            SafeDictionary.Add(dict, (15, i), new List<Block> { new Block.Thing.Lava() });
        }

        // grass thing
        for (int i = 7; i < 10; i += 1)
        {
            for (int j = 12; j < 15; j += 1)
            {
                if (i != 8 || j != 13)
                {
                    SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.Grass() });
                }
            }
        }

        // wall thing
        SafeDictionary.Add(dict, (2, 13), new List<Block> { new Block.Thing.Wall() });
        SafeDictionary.Add(dict, (6, 13), new List<Block> { new Block.Thing.Wall() });

        // baba text
        SafeDictionary.Add(dict, (3, 13), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (4, 13), new List<Block> { new Block.SpecialText.TextIs() });



        // push text
        SafeDictionary.Add(dict, (13, 15), new List<Block> { new Block.SpecialText.TextPush() });

        // lava text
        SafeDictionary.Add(dict, (2, 18), new List<Block> { new Block.ThingText.TextLava() });

        /*
        // wall thing
        for (int i = 1; i < 5; i += 1)
        {
            SafeDictionary.Add(dict, (3, i), new List<Block> { new Block.Thing.Wall() });
        }

        // wall thing
        for (int i = 1; i < 3; i += 1)
        {
            SafeDictionary.Add(dict, (i, 4), new List<Block> { new Block.Thing.Wall() });
        }

        // goop text
        SafeDictionary.Add(dict, (8, 2), new List<Block> { new Block.ThingText.TextGoop() });

        // is text
        SafeDictionary.Add(dict, (9, 2), new List<Block> { new Block.SpecialText.TextIs() });

        // sink text
        SafeDictionary.Add(dict, (10, 2), new List<Block> { new Block.SpecialText.TextSink() });

        
        // wall (main wall)
        for (int i = 6; i < 14; i += 1)
        {
            SafeDictionary.Add(dict, (i, 3), new List<Block> { new Block.Thing.Wall() });
        }
        
        for (int i = 4; i < 9; i += 1)
        {
            SafeDictionary.Add(dict, (6, i), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (13, i), new List<Block> { new Block.Thing.Wall() });
        }

        
        for (int i = 3; i < 17; i += 1)
        {
            if (i < 8 || i > 11)
            {
                SafeDictionary.Add(dict, (i, 9), new List<Block> { new Block.Thing.Wall() });
            }
            SafeDictionary.Add(dict, (i, 16), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 10; i < 16; i += 1)
        {
            SafeDictionary.Add(dict, (3, i), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (16, i), new List<Block> { new Block.Thing.Wall() });
        }

        
        // fake wall thing
        for (int i = 4; i < 9; i += 1)
        {
            for (int j = 7; j < 13; j += 1)
            {
                if (i == 5)
                {
                    if (j < 9 || j > 11)
                    {
                        SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.FakeWall() });
                    }
                } 
                else if (i == 7)
                {
                    if (j != 8 || j != 10 || j != 11)
                    {
                        SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.FakeWall() });
                    }
                } 
                else
                {
                    SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.FakeWall() });
                }
            }
        }

        // rock thing
        for (int i = 10; i < 12; i += 1)
        {
            SafeDictionary.Add(dict, (i, 7), new List<Block> { new Block.Thing.Rock() });
        }

        // goop thing (row)
        for (int i = 8; i < 12; i += 1)
        {
            SafeDictionary.Add(dict, (i, 9), new List<Block> { new Block.Thing.Goop() });
        }

        // goop thing (bottom left)
        for (int i = 13; i < 16; i += 1)
        {
            for (int j = 4; j < 7; j += 1)
            {
                if (i != 15 || j != 4)
                {
                    SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.Goop() });
                }
            }
        }

        // flag thing
        SafeDictionary.Add(dict, (4, 15), new List<Block> { new Block.Thing.Flag() });

        */
    }
}
