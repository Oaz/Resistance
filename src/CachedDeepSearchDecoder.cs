using System;
using System.Linq;
using System.Collections.Generic;

namespace Resistance
{
  public class CachedDeepSearchDecoder : DeepSearchStrategy
  {
    protected override T SearchDeeper<T> (Explorer<T> explorer, int shift)
    {
      return SearchDeeperWithCache (explorer, shift, new Dictionary<int,T> ());
    }

    private T SearchDeeperWithCache<T> (Explorer<T> explorer, int depth, Dictionary<int,T> cache)
    {
      var result = explorer.InitSearchResult ();
      var possibleWordsAtBeginningOfSequenceForCurrentDepth = explorer.Thesaurus.GetWordsStarting (explorer.Input.Skip (depth));
      foreach (var wayToGo in possibleWordsAtBeginningOfSequenceForCurrentDepth) {        
        var nextDepth = depth + wayToGo.MorseLength;
        ExploreWayWithCache (wayToGo, nextDepth, explorer, result, cache);
      }
      return result;
    }

    void ExploreWayWithCache<T> (Starting way, int depth, Explorer<T> explorer, T result, Dictionary<int, T> cache)
    {
      if (explorer.HasReachedBottom (way, depth, result))
        return;
     
      T cachedNextPath;
      if (cache.TryGetValue (depth, out cachedNextPath)) {
        explorer.OnSearchResult (result, way, cachedNextPath);
        return;
      }
     
      var possibleNextPath = SearchDeeperWithCache (explorer, depth, cache); 
      cache [depth] = possibleNextPath;
      explorer.OnSearchResult (result, way, possibleNextPath);
    }
  }
}

