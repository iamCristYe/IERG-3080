using BabaIsYou.Model;
using System.Collections.Generic;

partial class GetLevelDictionary
{
    public static void GetLevelThreeDictionary(Level currentLevel)
    {
        currentLevel.LevelContainsEmpty = false;
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        Tools.AddBorder(dict);

        // baba text
        SafeDictionary.Add(dict, (1, 1), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (1, 2), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (1, 3), new List<Block> { new Block.SpecialText.TextYou() });

        // wall text
        SafeDictionary.Add(dict, (2, 1), new List<Block> { new Block.ThingText.TextWall() });

        // is text
        SafeDictionary.Add(dict, (2, 2), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (2, 3), new List<Block> { new Block.SpecialText.TextStop() });

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


        // rock text
        SafeDictionary.Add(dict, (9, 5), new List<Block> { new Block.ThingText.TextRock() });

        // is text
        SafeDictionary.Add(dict, (10, 5), new List<Block> { new Block.SpecialText.TextIs() });

        // push text
        SafeDictionary.Add(dict, (11, 5), new List<Block> { new Block.SpecialText.TextPush() });

        // baba thing
        SafeDictionary.Add(dict, (8, 7), new List<Block> { new Block.Thing.Baba() });

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

        // flag text
        SafeDictionary.Add(dict, (12, 14), new List<Block> { new Block.ThingText.TextFlag() });

        // is text
        SafeDictionary.Add(dict, (13, 14), new List<Block> { new Block.SpecialText.TextIs() });

        // win text
        SafeDictionary.Add(dict, (14, 14), new List<Block> { new Block.SpecialText.TextWin() });

    }
}
