using System;
using System.Linq;
using System.Collections.Generic;

namespace Resistance
{
  public class TreeThesaurus : IFindWords
  {
    public TreeThesaurus (IEnumerable<string> words)
    {
      root = new Node ();
      foreach (var word in words) {
        AddWord (word);
      }
    }

    public IEnumerable<Starting> GetWordsStarting (IEnumerable<Morse> sequence)
    {
      var node = root;
      var length = 0;
      foreach (var morse in sequence) {
        node = node.Child (morse);
        if (node == null)
          break;
        length++;
        foreach (var word in node.Words) {
          yield return new Starting { Word=word, MorseLength=length };
        }
      }
    }

    private void AddWord (string word)
    {
      var node = root;
      var sequence = word.ToMorse ();
      foreach (var morse in sequence) {
        node = node.ChildAndCreateIfNeeded (morse);
      }
      node.Words.Add (word);
    }
    
    Node root;
    
    class Node
    {
      public Node ()
      {
        Words = new List<string> ();
      }
      
      public IList<string> Words { get; private set; }

      Node dot = null;
      Node dash = null;
      
      public Node Child (Morse morse)
      {
        return morse == Morse.Dot ? dot : dash;
      }
      
      public Node ChildAndCreateIfNeeded (Morse morse)
      {
        if (morse == Morse.Dot) {
          if (dot == null)
            dot = new Node ();
          return dot;
        } else {
          if (dash == null)
            dash = new Node ();
          return dash;
        }
      }
    }
  }
}

