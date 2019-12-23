using BabaIsYou.Model;
using System.Collections.Generic;

partial class GetLevelDictionary
{
    public static void GetLevelTwoDictionary(Level currentLevel)
    {
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        Tools.AddBorder(dict);

        // wall thing
        for (int i = 11; i < 16; i += 1)
        {
            SafeDictionary.Add(dict, (i, 4), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 5; i < 8; i += 1)
        {
            SafeDictionary.Add(dict, (11, i), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (15, i), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 5; i < 16; i += 1)
        {
            SafeDictionary.Add(dict, (i, 8), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (i, 16), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 9; i < 16; i += 1)
        {
            SafeDictionary.Add(dict, (5, i), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (15, i), new List<Block> { new Block.Thing.Wall() });
        }

        // fake wall thing
        for (int i = 5; i < 8; i += 1)
        {
            for (int j = 12; j < 15; j += 1)
            {
                if (i != 6 || j != 13)
                {
                    SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.FakeWall() });
                }
            }
        }

        // best text
        SafeDictionary.Add(dict, (3, 3), new List<Block> { new Block.ThingText.TextBest() });

        // flag text
        SafeDictionary.Add(dict, (12, 3), new List<Block> { new Block.ThingText.TextFlag() });

        // is text
        SafeDictionary.Add(dict, (13, 3), new List<Block> { new Block.SpecialText.TextIs() });

        // win text
        SafeDictionary.Add(dict, (14, 3), new List<Block> { new Block.SpecialText.TextWin() });


        // flag thing
        SafeDictionary.Add(dict, (13, 6), new List<Block> { new Block.Thing.Flag() });

        // wall text
        SafeDictionary.Add(dict, (7, 10), new List<Block> { new Block.ThingText.TextWall() });

        // is text
        SafeDictionary.Add(dict, (8, 10), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (9, 10), new List<Block> { new Block.SpecialText.TextStop() });

        // baba thing
        SafeDictionary.Add(dict, (7, 13), new List<Block> { new Block.Thing.Baba() });

        // baba text
        SafeDictionary.Add(dict, (6, 17), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (7, 17), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (8, 17), new List<Block> { new Block.SpecialText.TextYou() });

    }
}
