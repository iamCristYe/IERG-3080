using BabaIsYou.Model;
using System.Collections.Generic;

partial class GetLevelDictionary
{
    public static void GetLevelSevenDictionary(Level currentLevel)
    {
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        Tools.AddBorder(dict);

        // bone text
        SafeDictionary.Add(dict, (10, 1), new List<Block> { new Block.ThingText.TextBone() });

        // is text
        SafeDictionary.Add(dict, (11, 1), new List<Block> { new Block.SpecialText.TextIs() });

        // kill text
        SafeDictionary.Add(dict, (12, 1), new List<Block> { new Block.SpecialText.TextKill() });


        // 3x3 walls

        // top left
        Tools.Add3x3FakeWallBorder(dict, (3, 3), true);

        // mid left
        for (int i = 1; i < 3; i += 1)
        {
            SafeDictionary.Add(dict, (i, 7), new List<Block> { new Block.Thing.FakeWall() });
            SafeDictionary.Add(dict, (i, 9), new List<Block> { new Block.Thing.FakeWall() });
        }
        SafeDictionary.Add(dict, (2, 8), new List<Block> { new Block.Thing.FakeWall() });
        SafeDictionary.Add(dict, (1, 8), new List<Block> { new Block.Thing.Wall() });

        // baba in center
        Tools.Add3x3FakeWallBorder(dict, (7, 8), false);
        SafeDictionary.Add(dict, (9, 8), new List<Block> { new Block.Thing.Baba() });

        // best in center
        Tools.Add3x3FakeWallBorder(dict, (2, 13), false);
        SafeDictionary.Add(dict, (14, 3), new List<Block> { new Block.ThingText.TextBest() });

        // bottom left
        Tools.Add3x3FakeWallBorder(dict, (15, 1), true);

        // baba text
        SafeDictionary.Add(dict, (4, 12), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (4, 13), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (4, 14), new List<Block> { new Block.SpecialText.TextYou() });

        // keke text
        SafeDictionary.Add(dict, (8, 17), new List<Block> { new Block.ThingText.TextKeke() });

        // is text
        SafeDictionary.Add(dict, (9, 17), new List<Block> { new Block.SpecialText.TextIs() });

        // move text
        SafeDictionary.Add(dict, (10, 17), new List<Block> { new Block.SpecialText.TextMove() });

        // bone thing
        for (int i = 12; i < 19; i += 1)
        {
            if (i != 15)
            {
                SafeDictionary.Add(dict, (i, 10), new List<Block> { new Block.Thing.Bone() });
            }
        }

        for (int i = 11; i < 19; i += 1)
        {
            SafeDictionary.Add(dict, (12, i), new List<Block> { new Block.Thing.Bone() });
        }

        // grass
        for (int i = 11; i < 19; i += 1)
        {
            for (int j = 13; j < 19; j += 1)
            {
                if (!((i == 12 && j == 15) || (i == 14 && j == 16)))
                {
                    SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.Grass() });
                }
            }
        }

        // win text
        SafeDictionary.Add(dict, (15, 10), new List<Block> { new Block.SpecialText.TextWin() });

        // wall thing
        SafeDictionary.Add(dict, (15, 12), new List<Block> { new Block.Thing.Wall() });

        // keke thing
        Block keke = new Block.Thing.Keke();
        keke.Facing = "left";
        SafeDictionary.Add(dict, (16, 14), new List<Block> { keke });

        // wall text
        SafeDictionary.Add(dict, (18, 2), new List<Block> { new Block.ThingText.TextWall() });

        // is text
        SafeDictionary.Add(dict, (18, 3), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (18, 4), new List<Block> { new Block.SpecialText.TextStop() });
    }
}
