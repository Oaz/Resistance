using System;
using System.Linq;
using System.Collections.Generic;

namespace Resistance
{
  public class DumbThesaurus : IFindWords
  {
    public DumbThesaurus (IEnumerable<string> words)
    {
      MorseWords = new List<Item> ();
      foreach (var word in words) {
        var morse = Convert.MorseToDotDashString (word.ToMorse ());
        MorseWords.Add (new Item { Morse=morse, Word=word });
      }
    }
      
    public IEnumerable<Starting> GetWordsStarting (IEnumerable<Morse> sequence)
    {
      var input = Convert.MorseToDotDashString (sequence);
      foreach (var item in MorseWords) {
        if (input.StartsWith (item.Morse))
          yield return new Starting {
            Word=item.Word,
            MorseLength=item.Morse.Length
          };
      }
    }
      
    List<Item> MorseWords;
    
    struct Item
    {
      public string Morse;
      public string Word;
    }
  }
}

