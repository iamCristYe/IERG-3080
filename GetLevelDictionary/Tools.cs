using BabaIsYou.Model;
using System.Collections.Generic;

class Tools
{
    public static void AddBorder(Dictionary<(int, int), List<Block>> dict)
    {
      // vertical columns of grass (left most and right most column)
      for (int i = 0; i < 20; i += 1)
      {
          SafeDictionary.Add(dict, (0, i), new List<Block> { new Block.Thing.Border() });
          SafeDictionary.Add(dict, (19, i), new List<Block> { new Block.Thing.Border() });
      }

      // horizontal row of grass (top most and bottom most row)
      for (int i = 0; i < 20; i += 1)
      {
          SafeDictionary.Add(dict, (i, 0), new List<Block> { new Block.Thing.Border() });
          SafeDictionary.Add(dict, (i, 19), new List<Block> { new Block.Thing.Border() });
      }
    }

    public static void Add3x3FakeWallBorder(Dictionary<(int, int), List<Block>> dict, (int, int) startPos, bool centerIsWall)
    {
        int midRow = startPos.Item1 + 1;
        int midCol = startPos.Item2 + 1;

        for (int i = startPos.Item1; i < startPos.Item1 + 3; i += 1)
        {
            for (int j = startPos.Item2; j < startPos.Item2 + 3; j += 1)
            {
                if (i != midRow || j != midCol)
                {
                    SafeDictionary.Add(dict, (j, i), new List<Block> { new Block.Thing.FakeWall() });
                }
            }
        }

        if (centerIsWall)
        {
            SafeDictionary.Add(dict, (midCol, midRow), new List<Block> { new Block.Thing.Wall() });
        }
    }
}
