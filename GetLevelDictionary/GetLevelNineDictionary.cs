using BabaIsYou.Model;
using System.Collections.Generic;

partial class GetLevelDictionary
{
    public static void GetLevelNineDictionary(Level currentLevel)
    {
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        Tools.AddBorder(dict);

        Tools.AddBlockOfWall(dict, (1, 1), 4);
        Tools.AddBlockOfWall(dict, (15, 1), 4);
        Tools.AddBlockOfWall(dict, (1, 15), 4);
        Tools.AddBlockOfWall(dict, (15, 15), 4);

        // empty text
        SafeDictionary.Add(dict, (7, 3), new List<Block> { new Block.ThingText.TextEmpty() });

        // is text
        SafeDictionary.Add(dict, (8, 3), new List<Block> { new Block.SpecialText.TextIs() });

        // best text
        SafeDictionary.Add(dict, (3, 8), new List<Block> { new Block.ThingText.TextBest() });

        // wall text
        SafeDictionary.Add(dict, (18, 8), new List<Block> { new Block.ThingText.TextWall() });

        // is text
        SafeDictionary.Add(dict, (18, 9), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (18, 10), new List<Block> { new Block.SpecialText.TextStop() });

        // baba text
        SafeDictionary.Add(dict, (4, 12), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (5, 12), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (6, 12), new List<Block> { new Block.SpecialText.TextYou() });

        // baba thing
        SafeDictionary.Add(dict, (6, 11), new List<Block> { new Block.Thing.Baba() });

        // ice text
        SafeDictionary.Add(dict, (10, 14), new List<Block> { new Block.ThingText.TextIce() });

        // is text
        SafeDictionary.Add(dict, (11, 14), new List<Block> { new Block.SpecialText.TextIs() });

        // slip text
        SafeDictionary.Add(dict, (12, 14), new List<Block> { new Block.SpecialText.TextSlip() });

        // flag text
        SafeDictionary.Add(dict, (7, 18), new List<Block> { new Block.ThingText.TextFlag() });

        // is text
        SafeDictionary.Add(dict, (8, 18), new List<Block> { new Block.SpecialText.TextIs() });

        // win text
        SafeDictionary.Add(dict, (9, 18), new List<Block> { new Block.SpecialText.TextWin() });

        // flag thing
        SafeDictionary.Add(dict, (12, 8), new List<Block> { new Block.Thing.Flag() });

        // wall thing (middle of canvas)
        for (int i = 10; i < 14; i += 1)
        {
            SafeDictionary.Add(dict, (i, 6), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (i, 13), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 7; i < 10; i += 1)
        {
            SafeDictionary.Add(dict, (10, i), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (13, i), new List<Block> { new Block.Thing.Wall() });
        }

        SafeDictionary.Add(dict, (13, 14), new List<Block> { new Block.Thing.Wall() });

        // fake wall
        for (int i = 7; i < 10; i += 1)
        {
            SafeDictionary.Add(dict, (11, i), new List<Block> { new Block.Thing.FakeWall() });
            if (i != 8)
            {
                SafeDictionary.Add(dict, (12, i), new List<Block> { new Block.Thing.FakeWall() });
            }
        }

        // ice thing
        for (int i = 10; i < 13; i += 1)
        {
            for (int j = 10; j < 14; j += 1)
            {
                SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.Ice() });
            }
        }
    }
}
