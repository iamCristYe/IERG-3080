using BabaIsYou.Model;
using System.Collections.Generic;

namespace SafeDictionary {
  public void Add(Dictionary<(int, int), List<Model.Block>> _dict, (int, int) tuple, List<Model.Block> list)
  {
      if (_dict.ContainsKey(tuple))
      {
          List<Model.Block> new_list = _dict[tuple];
          foreach (Model.Block block in list)
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