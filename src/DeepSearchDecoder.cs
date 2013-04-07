using System;
using System.Linq;
using System.Collections.Generic;

namespace Resistance
{
  public class DeepSearchDecoder : DeepSearchStrategy
  {
    protected override T SearchDeeper<T> (Explorer<T> explorer, int depth)
    {
      var result = explorer.InitSearchResult ();
      var possibleWordsAtBeginningOfSequenceForCurrentDepth = explorer.Thesaurus.GetWordsStarting (explorer.Input.Skip (depth));
      foreach (var wayToGo in possibleWordsAtBeginningOfSequenceForCurrentDepth) {
        var nextDepth = depth + wayToGo.MorseLength;
        ExploreWay (wayToGo, nextDepth, explorer, result);
      }
      return result;
    }

    void ExploreWay<T> (Starting way, int depth, Explorer<T> explorer, T result)
    {
      if (explorer.HasReachedBottom (way, depth, result))
        return;
     
      var possibleNextPath = SearchDeeper (explorer, depth); 
      explorer.OnSearchResult (result, way, possibleNextPath);
    }
  }
  
}

