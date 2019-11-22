using BabaIsYou.Model;
using System.Collections.Generic;

class GetGrassBorder
{
    public static void AddGrass(Dictionary<(int, int), List<Block>> dict)
    {
      // vertical columns of grass (left most and right most column)
      for (int i = 0; i < 20; i += 1)
      {
          SafeDictionary.Add(dict, (0, i), new List<Block> { new Block.Thing.Grass() });
          SafeDictionary.Add(dict, (19, i), new List<Block> { new Block.Thing.Grass() });
      }

      // horizontal row of grass (top most and bottom most row)
      for (int i = 0; i < 20; i += 1)
      {
          SafeDictionary.Add(dict, (i, 0), new List<Block> { new Block.Thing.Grass() });
          SafeDictionary.Add(dict, (i, 19), new List<Block> { new Block.Thing.Grass() });
      }
    }
}
