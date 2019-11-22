using BabaIsYou.Model;
using System.Collections.Generic;

class GetLevelDictionary
{
    public static void GetLevelZeroDictionary(Level currentLevel)
    {
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        // vertical columns of grass (left most and right most column)
        for (int i = 0; i < 20; i += 1)
        {
            SafeDictionary.Add(dict, (0, i), new List<Block> { new Block.Thing.Grass() });
            SafeDictionary.Add(dict, (19, i), new List<Block> { new Block.Thing.Grass() });
        }

        // horizontal row of grass (top most and bottom most row)
        for (int i = 0; i < 20; i += 1)
        {
            SafeDictionary.Add(dict, (i, 0), new List<Block> { new Block.Thing.Grass() });
            SafeDictionary.Add(dict, (i, 19), new List<Block> { new Block.Thing.Grass() });
        }

        // wall text
        SafeDictionary.Add(dict, (5, 3), new List<Block> { new Block.ThingText.TextWall() });

        // is text
        SafeDictionary.Add(dict, (6, 3), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (7, 3), new List<Block> { new Block.SpecialText.TextStop() });

        // rock text
        SafeDictionary.Add(dict, (12, 3), new List<Block> { new Block.ThingText.TextRock() });

        // is text
        SafeDictionary.Add(dict, (13, 3), new List<Block> { new Block.SpecialText.TextIs() });

        // push text
        SafeDictionary.Add(dict, (14, 3), new List<Block> { new Block.SpecialText.TextPush() });

        // wall thing (a long series of wall)
        for (int i = 4; i < 15; i += 1)
        {
            SafeDictionary.Add(dict, (i, 5), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (i, 9), new List<Block> { new Block.Thing.Wall() });
        }

        // fake wall thing
        for (int i = 4; i < 15; i += 1)
        {
            SafeDictionary.Add(dict, (i, 6), new List<Block> { new Block.Thing.FakeWall() });
            if (i != 6 && i != 13)
            {
                SafeDictionary.Add(dict, (i, 7), new List<Block> { new Block.Thing.FakeWall() });
            }
            SafeDictionary.Add(dict, (i, 8), new List<Block> { new Block.Thing.FakeWall() });
        }

        // baba thing
        SafeDictionary.Add(dict, (6, 7), new List<Block> { new Block.Thing.Baba() });

        // flag thing
        SafeDictionary.Add(dict, (13, 7), new List<Block> { new Block.Thing.Flag() });

        // rock thing
        for (int i = 6; i < 9; i += 1)
        {
            SafeDictionary.Add(dict, (9, i), new List<Block> { new Block.Thing.Rock() });
        }

        // baba text
        SafeDictionary.Add(dict, (5, 11), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (6, 11), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (7, 11), new List<Block> { new Block.SpecialText.TextYou() });

        // flag text
        SafeDictionary.Add(dict, (12, 11), new List<Block> { new Block.ThingText.TextFlag() });

        // is text
        SafeDictionary.Add(dict, (13, 11), new List<Block> { new Block.SpecialText.TextIs() });

        // win text
        SafeDictionary.Add(dict, (14, 11), new List<Block> { new Block.SpecialText.TextWin() });

    }
}
