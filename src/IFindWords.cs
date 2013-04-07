using System;
using System.Collections.Generic;

namespace Resistance
{
  public struct Starting
  {
    public string Word;
    public int MorseLength;
  }
  
  public interface IFindWords
  {
    IEnumerable<Starting> GetWordsStarting (IEnumerable<Morse> sequence);
  }
}

