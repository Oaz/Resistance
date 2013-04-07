using System;
using System.Collections.Generic;

namespace Resistance
{
  public interface IDecodeMessage
  {
    IEnumerable<IEnumerable<string>> Decode (IEnumerable<Morse> message, IFindWords thesaurus);
    
    long Count (IEnumerable<Morse> message, IFindWords thesaurus);
  }
}

