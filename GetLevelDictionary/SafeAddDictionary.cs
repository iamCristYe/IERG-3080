using BabaIsYou.Model;
using System.Collections.Generic;

class SafeDictionary
{
    public static void Add(Dictionary<(int, int), List<Block>> _dict, (int, int) tuple, List<Block> list)
    {
        if (_dict.ContainsKey(tuple))
        {
            List<Block> new_list = _dict[tuple];
            foreach (Block block in list)
            {
                new_list.Add(block);
            }
        }
        else
        {
            _dict[tuple] = list;
        }
    }
}
