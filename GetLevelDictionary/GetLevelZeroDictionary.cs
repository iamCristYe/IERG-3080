using BabaIsYou.Model;
using System.Collections.Generic;

partial class GetLevelDictionary
{
    public static void GetLevelZeroDictionary(Level currentLevel)
    {
        currentLevel.LevelContainsEmpty = false;
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        Tools.AddBorder(dict);

        // wall text
        SafeDictionary.Add(dict, (5, 5), new List<Block> { new Block.ThingText.TextWall() });

        // is text
        SafeDictionary.Add(dict, (6, 5), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (7, 5), new List<Block> { new Block.SpecialText.TextStop() });

        // rock text
        SafeDictionary.Add(dict, (12, 5), new List<Block> { new Block.ThingText.TextRock() });

        // is text
        SafeDictionary.Add(dict, (13, 5), new List<Block> { new Block.SpecialText.TextIs() });

        // push text
        SafeDictionary.Add(dict, (14, 5), new List<Block> { new Block.SpecialText.TextPush() });

        // rock thing
        for (int i = 8; i < 11; i += 1)
        {
            SafeDictionary.Add(dict, (9, i), new List<Block> { new Block.Thing.Rock() });
        }

        // wall thing (a long series of wall)
        for (int i = 4; i < 15; i += 1)
        {
            SafeDictionary.Add(dict, (i, 7), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (i, 11), new List<Block> { new Block.Thing.Wall() });
        }

        // fake wall thing
        for (int i = 4; i < 15; i += 1)
        {
            SafeDictionary.Add(dict, (i, 8), new List<Block> { new Block.Thing.FakeWall() });
            if (i != 6 && i != 13)
            {
                SafeDictionary.Add(dict, (i, 9), new List<Block> { new Block.Thing.FakeWall() });
            }
            SafeDictionary.Add(dict, (i, 10), new List<Block> { new Block.Thing.FakeWall() });
        }

        // baba thing
        SafeDictionary.Add(dict, (6, 9), new List<Block> { new Block.Thing.Baba() });

        // flag thing
        SafeDictionary.Add(dict, (13, 9), new List<Block> { new Block.Thing.Flag() });

        // baba text
        SafeDictionary.Add(dict, (5, 13), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (6, 13), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (7, 13), new List<Block> { new Block.SpecialText.TextYou() });

        // flag text
        SafeDictionary.Add(dict, (12, 13), new List<Block> { new Block.ThingText.TextFlag() });

        // is text
        SafeDictionary.Add(dict, (13, 13), new List<Block> { new Block.SpecialText.TextIs() });

        // win text
        SafeDictionary.Add(dict, (14, 13), new List<Block> { new Block.SpecialText.TextWin() });

    }
}
