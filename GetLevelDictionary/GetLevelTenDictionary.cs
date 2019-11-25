using BabaIsYou.Model;
using System.Collections.Generic;

partial class GetLevelDictionary
{
    public static void GetLevelTenDictionary(Level currentLevel)
    {
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        Tools.AddBorder(dict);

        // wall text
        SafeDictionary.Add(dict, (3, 1), new List<Block> { new Block.ThingText.TextWall() });

        // is text
        SafeDictionary.Add(dict, (4, 1), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (5, 1), new List<Block> { new Block.SpecialText.TextStop() });

        // lava text
        SafeDictionary.Add(dict, (12, 1), new List<Block> { new Block.ThingText.TextLava() });

        // is text
        SafeDictionary.Add(dict, (13, 1), new List<Block> { new Block.SpecialText.TextIs() });

        // kill text
        SafeDictionary.Add(dict, (14, 1), new List<Block> { new Block.SpecialText.TextKill() });

        // love text
        SafeDictionary.Add(dict, (11, 6), new List<Block> { new Block.ThingText.TextLove() });

        // is text
        SafeDictionary.Add(dict, (11, 7), new List<Block> { new Block.SpecialText.TextIs() });

        // win text
        SafeDictionary.Add(dict, (11, 8), new List<Block> { new Block.SpecialText.TextWin() });

        // list of keke tuples 
        List<(int, int)> kekeCoords = new List<(int, int)>()
        {
            (6, 2), (3, 4), (15, 6), (13, 12), (3, 14), (10, 15), (17, 17)
        };

        foreach((int, int) coord in kekeCoords)
        {
            SafeDictionary.Add(dict, coord, new List<Block> { new Block.Thing.Keke() });
        }

        // baba thing
        SafeDictionary.Add(dict, (6, 12), new List<Block> { new Block.Thing.Baba() });

        // baba text
        SafeDictionary.Add(dict, (5, 15), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (6, 15), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (7, 15), new List<Block> { new Block.SpecialText.TextYou() });

        // keke text
        SafeDictionary.Add(dict, (12, 15), new List<Block> { new Block.ThingText.TextKeke() });

        // is text
        SafeDictionary.Add(dict, (13, 15), new List<Block> { new Block.SpecialText.TextIs() });

        // move text
        SafeDictionary.Add(dict, (14, 15), new List<Block> { new Block.SpecialText.TextMove() });

        Tools.AddBlockOfFakeWall(dict, (16, 5), 3);
        Tools.AddBlockOfFakeWall(dict, (16, 12), 3);

        // wall thing
        for (int i = 8; i < 11; i += 1)
        {
            SafeDictionary.Add(dict, (i, 4), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 5; i < 12; i += 1)
        {
            SafeDictionary.Add(dict, (8, i), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (10, i), new List<Block> { new Block.Thing.Wall() });
        }

        SafeDictionary.Add(dict, (11, 9), new List<Block> { new Block.Thing.Wall() });

        // fake wall thing
        SafeDictionary.Add(dict, (9, 6), new List<Block> { new Block.Thing.FakeWall() });
        SafeDictionary.Add(dict, (9, 11), new List<Block> { new Block.Thing.FakeWall() });

        // lava thing
        for (int i = 7; i < 11; i += 1)
        {
            SafeDictionary.Add(dict, (9, i), new List<Block> { new Block.Thing.Lava() });
        }

        // love thing
        SafeDictionary.Add(dict, (9, 5), new List<Block> { new Block.Thing.Love() });
    }
}
