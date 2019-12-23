using BabaIsYou.Model;
using System.Collections.Generic;

partial class GetLevelDictionary
{
    public static void GetLevelTwelveDictionary(Level currentLevel)
    {
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        Tools.AddBorder(dict);

        // lava text
        SafeDictionary.Add(dict, (5, 1), new List<Block> { new Block.ThingText.TextLava() });

        // is text
        SafeDictionary.Add(dict, (6, 1), new List<Block> { new Block.SpecialText.TextIs() });

        // hot text
        SafeDictionary.Add(dict, (7, 1), new List<Block> { new Block.SpecialText.TextHot() });

        // lava thing
        for (int i = 1; i < 19; i += 1)
        {
            SafeDictionary.Add(dict, (9, i), new List<Block> { new Block.Thing.Lava() });
        }

        // empty text
        SafeDictionary.Add(dict, (14, 2), new List<Block> { new Block.ThingText.TextEmpty() });

        // wall text
        SafeDictionary.Add(dict, (1, 9), new List<Block> { new Block.ThingText.TextWall() });

        // is text
        SafeDictionary.Add(dict, (1, 10), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (1, 11), new List<Block> { new Block.SpecialText.TextStop() });

        // goop text
        SafeDictionary.Add(dict, (18, 3), new List<Block> { new Block.ThingText.TextGoop() });

        // is text
        SafeDictionary.Add(dict, (18, 4), new List<Block> { new Block.SpecialText.TextIs() });

        // sink text
        SafeDictionary.Add(dict, (18, 5), new List<Block> { new Block.SpecialText.TextSink() });

        // baba thing
        SafeDictionary.Add(dict, (6, 11), new List<Block> { new Block.Thing.Baba() });

        // baba text
        SafeDictionary.Add(dict, (5, 14), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (6, 14), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (7, 14), new List<Block> { new Block.SpecialText.TextYou() });

        // keke text
        SafeDictionary.Add(dict, (12, 13), new List<Block> { new Block.ThingText.TextKeke() });

        // is text
        SafeDictionary.Add(dict, (13, 13), new List<Block> { new Block.SpecialText.TextIs() });

        // push text
        SafeDictionary.Add(dict, (14, 13), new List<Block> { new Block.SpecialText.TextPush() });

        // keke thing
        SafeDictionary.Add(dict, (14, 9), new List<Block> { new Block.Thing.Keke() });

        // wall thing
        SafeDictionary.Add(dict, (16, 9), new List<Block> { new Block.Thing.Wall() });

        // baba text
        SafeDictionary.Add(dict, (18, 9), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (18, 10), new List<Block> { new Block.SpecialText.TextIs() });

        // melt text
        SafeDictionary.Add(dict, (18, 11), new List<Block> { new Block.SpecialText.TextMelt() });

        // goop thing
        for (int i = 15; i < 19; i += 1)
        {
            SafeDictionary.Add(dict, (i, 15), new List<Block> { new Block.Thing.Goop() });
        }

        for (int i = 16; i < 19; i += 1)
        {
            SafeDictionary.Add(dict, (15, i), new List<Block> { new Block.Thing.Goop() });
        }

        // win text
        SafeDictionary.Add(dict, (18, 18), new List<Block> { new Block.SpecialText.TextWin() });
    }
}
