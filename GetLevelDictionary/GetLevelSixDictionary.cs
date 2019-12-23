using BabaIsYou.Model;
using System.Collections.Generic;

partial class GetLevelDictionary
{
    public static void GetLevelSixDictionary(Level currentLevel)
    {
        currentLevel.LevelContainsEmpty = false;
        Dictionary<(int, int), List<Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

        Tools.AddBorder(dict);

        // grass text
        SafeDictionary.Add(dict, (9, 1), new List<Block> { new Block.ThingText.TextGrass() });

        // is text
        SafeDictionary.Add(dict, (10, 1), new List<Block> { new Block.SpecialText.TextIs() });

        // stop text
        SafeDictionary.Add(dict, (11, 1), new List<Block> { new Block.SpecialText.TextStop() });

        // grass thing
        List<(int, int)> coordinates = new List<(int, int)>()
        {
            (10, 8), (11, 9), (10, 10), (11, 11), (13, 11), (9, 12), (12, 12), (14, 12), (9, 13),
            (9, 14), (12, 14), (7, 15), (10,  15)
        };

        foreach((int, int) coord in coordinates)
        {
            SafeDictionary.Add(dict, coord, new List<Block> { new Block.Thing.Grass() });
        }

        // wall thing
        for (int i = 2; i < 16; i += 1)
        {
            SafeDictionary.Add(dict, (i, 7), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (i, 16), new List<Block> { new Block.Thing.Wall() });
        }

        for (int i = 8; i < 16; i += 1)
        {
            SafeDictionary.Add(dict, (2, i), new List<Block> { new Block.Thing.Wall() });
            SafeDictionary.Add(dict, (15, i), new List<Block> { new Block.Thing.Wall() });
        }

        // baba text
        SafeDictionary.Add(dict, (4, 9), new List<Block> { new Block.ThingText.TextBaba() });

        // is text
        SafeDictionary.Add(dict, (5, 9), new List<Block> { new Block.SpecialText.TextIs() });

        // you text
        SafeDictionary.Add(dict, (6, 9), new List<Block> { new Block.SpecialText.TextYou() });

        // flag thing
        SafeDictionary.Add(dict, (13, 9), new List<Block> { new Block.Thing.Flag() });

        // flag text
        SafeDictionary.Add(dict, (11, 13), new List<Block> { new Block.ThingText.TextFlag() });

        // win text
        SafeDictionary.Add(dict, (13, 13), new List<Block> { new Block.SpecialText.TextWin() });

        // baba thing
        SafeDictionary.Add(dict, (6, 12), new List<Block> { new Block.Thing.Baba() });
    }
}
