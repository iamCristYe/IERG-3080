using BabaIsYou.Model;
using System.Collections.Generic;

partial class GetLevelDictionary
{
    public static void GetLevelElevenDictionary(Level currentLevel)
    {
        currentLevel.LevelContainsEmpty = true;
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        Tools.AddBorder(dict);

        // anni text
        SafeDictionary.Add(dict, (1, 3), new List<Block> { new Block.ThingText.TextAnni() });

        // is text
        SafeDictionary.Add(dict, (2, 3), new List<Block> { new Block.SpecialText.TextIs() });

        // best text
        SafeDictionary.Add(dict, (3, 3), new List<Block> { new Block.ThingText.TextBest() });

        // TODO: anni thing
        // SafeDictionary.Add(dict, (2, 2), new List<Block> { new Block.Thing.Anni() });

        // love thing
        SafeDictionary.Add(dict, (2, 1), new List<Block> { new Block.Thing.Love() });

        // flag text
        SafeDictionary.Add(dict, (7, 1), new List<Block> { new Block.ThingText.TextFlag() });

        // is text
        SafeDictionary.Add(dict, (8, 1), new List<Block> { new Block.SpecialText.TextIs() });

        // win text
        SafeDictionary.Add(dict, (9, 1), new List<Block> { new Block.SpecialText.TextWin() });

        // wall thing (top left)
        for (int i = 1; i < 5; i += 1)
        {
            SafeDictionary.Add(dict, (i, 4), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 1; i < 4; i += 1)
        {
            SafeDictionary.Add(dict, (4, i), new List<Block> { new Block.Thing.Wall() });
        }

        // wall thing
        for (int i = 12; i < 17; i += 1)
        {
            SafeDictionary.Add(dict, (i, 3), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (i, 7), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 4; i < 7; i += 1)
        {
            SafeDictionary.Add(dict, (12, i), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (16, i), new List<Block> { new Block.Thing.Wall() });
        }

        // flag thing
        SafeDictionary.Add(dict, (14, 5), new List<Block> { new Block.Thing.Flag() });

        // baba thing
        SafeDictionary.Add(dict, (6, 10), new List<Block> { new Block.Thing.Baba() });

        // ice thing
        for (int i = 10; i < 15; i += 1)
        {
            for (int j = 7; j < 12; j += 1)
            {
                if (i != 12 || j != 9)
                {
                    SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.Ice() });
                }
            }
        }

        // empty thing
        SafeDictionary.Add(dict, (9, 12), new List<Block> { new Block.ThingText.TextEmpty() });

        // ice text
        SafeDictionary.Add(dict, (3, 12), new List<Block> { new Block.ThingText.TextIce() });

        // is text
        SafeDictionary.Add(dict, (3, 13), new List<Block> { new Block.SpecialText.TextIs() });

        // slip text
        SafeDictionary.Add(dict, (3, 14), new List<Block> { new Block.SpecialText.TextSlip() });

        // wall text
        SafeDictionary.Add(dict, (18, 12), new List<Block> { new Block.ThingText.TextWall() });

        // is text
        SafeDictionary.Add(dict, (18, 13), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (18, 14), new List<Block> { new Block.SpecialText.TextStop() });

        // baba text
        SafeDictionary.Add(dict, (4, 18), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (5, 18), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (6, 18), new List<Block> { new Block.SpecialText.TextYou() });

        // wall thing
        for (int i = 15; i < 19; i += 1)
        {
            SafeDictionary.Add(dict, (i, 16), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 17; i < 19; i += 1)
        {
            SafeDictionary.Add(dict, (15, i), new List<Block> { new Block.Thing.Wall() });
        }

        // love text
        SafeDictionary.Add(dict, (16, 17), new List<Block> { new Block.ThingText.TextLove() });

        // is text
        SafeDictionary.Add(dict, (17, 17), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (18, 17), new List<Block> { new Block.SpecialText.TextStop() });

        // anni text
        SafeDictionary.Add(dict, (16, 18), new List<Block> { new Block.ThingText.TextAnni() });

        // is text
        SafeDictionary.Add(dict, (17, 18), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (18, 18), new List<Block> { new Block.SpecialText.TextStop() });
    }
}
