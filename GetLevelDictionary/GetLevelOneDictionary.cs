using BabaIsYou.Model;
using System.Collections.Generic;

partial class GetLevelDictionary
{
    public static void GetLevelOneDictionary(Level currentLevel)
    {
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        GetGrassBorder.AddGrass(dict);

        // wall thing
        for (int i = 8; i < 16; i += 1)
        {
            SafeDictionary.Add(dict, (i, 4), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 5; i < 8; i += 1)
        {
            SafeDictionary.Add(dict, (8, i), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 5; i < 12; i += 1)
        {
            SafeDictionary.Add(dict, (15, i), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 4; i < 8; i += 1)
        {
            SafeDictionary.Add(dict, (i, 7), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 4; i < 15; i += 1)
        {
            SafeDictionary.Add(dict, (i, 11), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 8; i < 11; i += 1)
        {
            SafeDictionary.Add(dict, (4, i), new List<Block> { new Block.Thing.Wall() });
        }

        // fake wall thing
        for (int i = 8; i < 11; i += 1)
        {
            for (int j = 5; j < 9; j += 1)
            {
                if (i != 9 || j != 6)
                {
                    SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.FakeWall() });
                }
            }
        }

        // flag text
        SafeDictionary.Add(dict, (6, 9), new List<Block> { new Block.ThingText.TextFlag() });

        // is text
        SafeDictionary.Add(dict, (10, 6), new List<Block> { new Block.SpecialText.TextIs() });

        // win text
        SafeDictionary.Add(dict, (13, 7), new List<Block> { new Block.SpecialText.TextWin() });

        // flag thing
        SafeDictionary.Add(dict, (10, 9), new List<Block> { new Block.Thing.Flag() });

        // baba thing
        SafeDictionary.Add(dict, (13, 9), new List<Block> { new Block.Thing.Baba() });

        // baba text
        SafeDictionary.Add(dict, (5, 13), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (5, 14), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (5, 15), new List<Block> { new Block.SpecialText.TextYou() });

        // wall text
        SafeDictionary.Add(dict, (8, 13), new List<Block> { new Block.ThingText.TextWall() });

        // is text
        SafeDictionary.Add(dict, (8, 14), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (8, 15), new List<Block> { new Block.SpecialText.TextStop() });
    }
}
