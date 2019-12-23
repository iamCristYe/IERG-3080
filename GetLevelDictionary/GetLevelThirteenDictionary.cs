using BabaIsYou.Model;
using System.Collections.Generic;

partial class GetLevelDictionary
{
    public static void GetLevelThirteenDictionary(Level currentLevel)
    {
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        Tools.AddBorder(dict);

        HashSet<(int, int)> CoordsWithoutGrass = new HashSet<(int, int)>()
        {
            (9, 2), (14, 3), (18, 10), (18, 11), (10, 12), (18, 12),
            (4, 13), (7, 13), (4, 14), (7, 14), (4, 15), (5, 15), (6, 15),
            (18, 16), (18, 17), (18, 18)
        };

        for (int i = 1; i < 19; i += 1)
        {
            for (int j = 1; j < 19; j += 1)
            {
                if (!CoordsWithoutGrass.Contains((j, i)))
                {
                    SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.Grass() });
                }
            }
        }

        // win text
        SafeDictionary.Add(dict, (9, 2), new List<Block> { new Block.SpecialText.TextWin() });

        // keke thing
        SafeDictionary.Add(dict, (14, 3), new List<Block> { new Block.Thing.Keke() });

        // keke text
        SafeDictionary.Add(dict, (18, 10), new List<Block> { new Block.ThingText.TextKeke() });

        // is text
        SafeDictionary.Add(dict, (18, 11), new List<Block> { new Block.SpecialText.TextIs() });

        // melt text
        SafeDictionary.Add(dict, (18, 12), new List<Block> { new Block.SpecialText.TextMelt() });

        // keke text
        SafeDictionary.Add(dict, (7, 13), new List<Block> { new Block.ThingText.TextKeke() });

        // is text
        SafeDictionary.Add(dict, (7, 14), new List<Block> { new Block.SpecialText.TextIs() });

        // empty text
        SafeDictionary.Add(dict, (4, 13), new List<Block> { new Block.ThingText.TextEmpty() });

        // is text
        SafeDictionary.Add(dict, (4, 14), new List<Block> { new Block.SpecialText.TextIs() });

        // baba thing
        SafeDictionary.Add(dict, (10, 12), new List<Block> { new Block.Thing.Baba() });

        // baba text
        SafeDictionary.Add(dict, (4, 15), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (5, 15), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (6, 15), new List<Block> { new Block.SpecialText.TextYou() });

        // baba text
        SafeDictionary.Add(dict, (18, 16), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (18, 17), new List<Block> { new Block.SpecialText.TextIs() });

        // hot text
        SafeDictionary.Add(dict, (18, 18), new List<Block> { new Block.SpecialText.TextHot() });
    }
}
