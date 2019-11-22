using BabaIsYou.Model;
using SafeDictionary;

namespace GetLevelDictionary {
  public void GetLevelZeroDictionary(Model.Level currentLevel) {
    Dictionary<(int, int), List<Model.Block>> dict = currentLevel.CurrentMap.PointBlockPairs;

    // vertical columns of grass (left most and right most column)
    for (int i = 0; i < 20; i += 1)
    {
        SafeDictionary.Add(dict, (0, i), new List<Model.Block> { new Model.Block.Thing.Grass() });
        SafeDictionary.Add(dict, (19, i), new List<Model.Block> { new Model.Block.Thing.Grass() });
    }

    // horizontal row of grass (top most and bottom most row)
    for (int i = 0; i < 20; i += 1)
    {
        SafeDictionary.Add(dict, (i, 0), new List<Model.Block> { new Model.Block.Thing.Grass() });
        SafeDictionary.Add(dict, (i, 19), new List<Model.Block> { new Model.Block.Thing.Grass() });
    }

    // wall text
    SafeDictionary.Add(dict, (5, 3), new List<Model.Block> { new Model.Block.ThingText.TextWall() });

    // is text
    SafeDictionary.Add(dict, (6, 3), new List<Model.Block> { new Model.Block.SpecialText.TextIs() });

    // stop text
    SafeDictionary.Add(dict, (7, 3), new List<Model.Block> { new Model.Block.SpecialText.TextStop() });

    // rock text
    SafeDictionary.Add(dict, (12, 3), new List<Model.Block> { new Model.Block.ThingText.TextRock() });

    // is text
    SafeDictionary.Add(dict, (13, 3), new List<Model.Block> { new Model.Block.SpecialText.TextIs() });

    // push text
    SafeDictionary.Add(dict, (14, 3), new List<Model.Block> { new Model.Block.SpecialText.TextPush() });

    // wall thing (a long series of wall)
    for (int i = 4; i < 15; i += 1)
    {
        SafeDictionary.Add(dict, (i, 5), new List<Model.Block> { new Model.Block.Thing.Wall() });
        SafeDictionary.Add(dict, (i, 9), new List<Model.Block> { new Model.Block.Thing.Wall() });
    }

    // fake wall thing
    for (int i = 4; i < 15; i += 1)
    {
        SafeDictionary.Add(dict, (i, 6), new List<Model.Block> { new Model.Block.Thing.FakeWall() });
        if (i != 6 && i != 13)
        {
            SafeDictionary.Add(dict, (i, 7), new List<Model.Block> { new Model.Block.Thing.FakeWall() });
        }
        SafeDictionary.Add(dict, (i, 8), new List<Model.Block> { new Model.Block.Thing.FakeWall() });
    }

    // baba thing
    SafeDictionary.Add(dict, (6, 7), new List<Model.Block> { new Model.Block.Thing.Baba() });

    // flag thing
    SafeDictionary.Add(dict, (13, 7), new List<Model.Block> { new Model.Block.Thing.Flag() });

    // rock thing
    for (int i = 6; i < 9; i += 1)
    {
        SafeDictionary.Add(dict, (9, i), new List<Model.Block> { new Model.Block.Thing.Rock() });
    }

    // baba text
    SafeDictionary.Add(dict, (5, 11), new List<Model.Block> { new Model.Block.ThingText.TextBaba() });

    // is text
    SafeDictionary.Add(dict, (6, 11), new List<Model.Block> { new Model.Block.SpecialText.TextIs() });

    // you text
    SafeDictionary.Add(dict, (7, 11), new List<Model.Block> { new Model.Block.SpecialText.TextYou() });

    // flag text
    SafeDictionary.Add(dict, (12, 11), new List<Model.Block> { new Model.Block.ThingText.TextFlag() });

    // is text
    SafeDictionary.Add(dict, (13, 11), new List<Model.Block> { new Model.Block.SpecialText.TextIs() });

    // win text
    SafeDictionary.Add(dict, (14, 11), new List<Model.Block> { new Model.Block.SpecialText.TextWin() });

  }
}