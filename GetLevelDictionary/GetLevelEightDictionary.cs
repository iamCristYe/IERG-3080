using BabaIsYou.Model;
using System.Collections.Generic;

partial class GetLevelDictionary
{
    public static void GetLevelEightDictionary(Level currentLevel)
    {
        currentLevel.LevelContainsEmpty = false;
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        Tools.AddBorder(dict);

        // wall thing
        for (int i = 7; i < 16; i += 1)
        {
            SafeDictionary.Add(dict, (i, 4), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (i, 13), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 5; i < 13; i += 1)
        {
            SafeDictionary.Add(dict, (7, i), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (15, i), new List<Block> { new Block.Thing.Wall() });
        }

        // fake wall thing
        HashSet<(int, int)> exclude = new HashSet<(int, int)>()
        {
            (5, 8), (6, 8), (6, 10), (6, 13), (7, 8), (7, 9), (7, 10), (7, 13),
            (8, 13), (10, 9), (10, 10), (10, 11)
        };

        for (int i = 5; i < 13; i += 1)
        {
            for (int j = 8; j < 15; j += 1)
            {
                (int, int) coor = (i, j);
                if (!exclude.Contains(coor))
                {
                    SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.FakeWall() });
                }
            }
        }

        // bone text
        SafeDictionary.Add(dict, (9, 10), new List<Block> { new Block.ThingText.TextBone() });

        // is text
        SafeDictionary.Add(dict, (10, 10), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (11, 10), new List<Block> { new Block.SpecialText.TextStop() });

        // wall text
        SafeDictionary.Add(dict, (13, 6), new List<Block> { new Block.ThingText.TextWall() });

        // is text
        SafeDictionary.Add(dict, (13, 7), new List<Block> { new Block.SpecialText.TextIs() });

        // kill text
        SafeDictionary.Add(dict, (13, 8), new List<Block> { new Block.SpecialText.TextKill() });

        // TODO: bone thing
        for (int i = 8; i < 11; i += 1)
        {
            SafeDictionary.Add(dict, (i, 7), new List<Block> { new Block.Thing.Bone() });
        }
        SafeDictionary.Add(dict, (10, 6), new List<Block> { new Block.Thing.Bone() });

        // baba thing
        SafeDictionary.Add(dict, (8, 6), new List<Block> { new Block.Thing.Baba() });

        // flag text
        SafeDictionary.Add(dict, (6, 15), new List<Block> { new Block.ThingText.TextFlag() });

        // is text
        SafeDictionary.Add(dict, (7, 15), new List<Block> { new Block.SpecialText.TextIs() });

        // win text
        SafeDictionary.Add(dict, (8, 15), new List<Block> { new Block.SpecialText.TextWin() });

        // wall text
        SafeDictionary.Add(dict, (3, 18), new List<Block> { new Block.ThingText.TextWall() });

        // is text
        SafeDictionary.Add(dict, (4, 18), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (5, 18), new List<Block> { new Block.SpecialText.TextStop() });

        // baba text
        SafeDictionary.Add(dict, (9, 18), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (10, 18), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (11, 18), new List<Block> { new Block.SpecialText.TextYou() });
    }
}
