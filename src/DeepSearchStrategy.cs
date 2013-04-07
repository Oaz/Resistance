using System;
using System.Linq;
using System.Collections.Generic;

namespace Resistance
{
  public abstract class DeepSearchStrategy : IDecodeMessage
  {
    public IEnumerable<IEnumerable<string>> Decode (IEnumerable<Morse> message, IFindWords thesaurus)
    {
      var explorer = new ExplorerForDecoding { Input=message.ToArray (), Thesaurus=thesaurus };
      return SearchDeeper (explorer, 0);
    }
    
    public long Count (IEnumerable<Morse> message, IFindWords thesaurus)
    {   
      var explorer = new ExplorerForCounting { Input=message.ToArray (), Thesaurus=thesaurus };
      return SearchDeeper (explorer, 0) [0];
    }

    protected abstract T SearchDeeper<T> (Explorer<T> explorer, int shift);
    
    protected abstract class Explorer<T>
    {
      public IEnumerable<Morse> Input;

      public bool HasReachedBottom (Starting way, int depth, T result)
      {
        if (depth < Input.Count ())
          return false;
        OnBottom (result, way);
        return true;
      }

      public IFindWords Thesaurus;

      public abstract T InitSearchResult ();

      protected abstract void OnBottom (T result, Starting way);

      public abstract void OnSearchResult (T result, Starting way, T following);
    }
    
    protected class ExplorerForDecoding : Explorer<List<IEnumerable<string>>>
    {
      public override List<IEnumerable<string>> InitSearchResult ()
      {
        return new List<IEnumerable<string>> ();
      }

      protected override void OnBottom (List<IEnumerable<string>> result, Starting way)
      {
        result.Add (Convert.Seq (way.Word));
      }

      public override void OnSearchResult (List<IEnumerable<string>> result, Starting way, List<IEnumerable<string>> possibleNextPath)
      {
        foreach (var nextPath in possibleNextPath)
          result.Add (Convert.Seq (way.Word).Concat (nextPath));
      }
    }
    
    protected class ExplorerForCounting : Explorer<long[]>
    {
      public override long[] InitSearchResult ()
      {
        return new long[1];
      }

      protected override void OnBottom (long[] result, Starting way)
      {
        result [0]++;
      }

      public override void OnSearchResult (long[] result, Starting way, long[] possibleNextPath)
      {
        result [0] += possibleNextPath [0];
      }
    }
  }
}

