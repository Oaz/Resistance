using System;
using System.Linq;
using System.Collections.Generic;

namespace Resistance
{
  public enum Morse
  {
    Dot,
    Dash
  }

  public static class Convert
  {
    public static IEnumerable<Morse> ToMorse (this string text)
    {
      return text.SelectMany (character => character.ToMorse ());
    }
    
    public static IEnumerable<Morse> ToMorse (this char character)
    {
      var upperChar = character.ToString ().ToUpper ().First ();
      return charsToMorse.ContainsKey (upperChar) ? charsToMorse [upperChar] : Enumerable.Empty<Morse> ();
    }

    public static IEnumerable<Morse> DotDashStringToMorse (string strDotDash)
    {
      return strDotDash.Select (c => c == '.' ? Morse.Dot : Morse.Dash);
    }

    public static string MorseToDotDashString (IEnumerable<Morse> morse)
    {
      return new string (morse.Select (m => m == Morse.Dot ? '.' : '-').ToArray ());
    }
    
    public static IEnumerable<T> Seq<T> (params T[] items)
    {
      return items;
    }

    static Convert ()
    {
      charsToMorse = new Dictionary<char,IEnumerable<Morse>> ();
      charsToMorse ['A'] = Seq (Morse.Dot, Morse.Dash);
      charsToMorse ['B'] = Seq (Morse.Dash, Morse.Dot, Morse.Dot, Morse.Dot);
      charsToMorse ['C'] = Seq (Morse.Dash, Morse.Dot, Morse.Dash, Morse.Dot);
      charsToMorse ['D'] = Seq (Morse.Dash, Morse.Dot, Morse.Dot);
      charsToMorse ['E'] = Seq (Morse.Dot);
      charsToMorse ['F'] = Seq (Morse.Dot, Morse.Dot, Morse.Dash, Morse.Dot);
      charsToMorse ['G'] = Seq (Morse.Dash, Morse.Dash, Morse.Dot);
      charsToMorse ['H'] = Seq (Morse.Dot, Morse.Dot, Morse.Dot, Morse.Dot);
      charsToMorse ['I'] = Seq (Morse.Dot, Morse.Dot);
      charsToMorse ['J'] = Seq (Morse.Dot, Morse.Dash, Morse.Dash, Morse.Dash);
      charsToMorse ['K'] = Seq (Morse.Dash, Morse.Dot, Morse.Dash);
      charsToMorse ['L'] = Seq (Morse.Dot, Morse.Dash, Morse.Dot, Morse.Dot);
      charsToMorse ['M'] = Seq (Morse.Dash, Morse.Dash);
      charsToMorse ['N'] = Seq (Morse.Dash, Morse.Dot);
      charsToMorse ['O'] = Seq (Morse.Dash, Morse.Dash, Morse.Dash);
      charsToMorse ['P'] = Seq (Morse.Dot, Morse.Dash, Morse.Dash, Morse.Dot);
      charsToMorse ['Q'] = Seq (Morse.Dash, Morse.Dash, Morse.Dot, Morse.Dash);
      charsToMorse ['R'] = Seq (Morse.Dot, Morse.Dash, Morse.Dot);
      charsToMorse ['S'] = Seq (Morse.Dot, Morse.Dot, Morse.Dot);
      charsToMorse ['T'] = Seq (Morse.Dash);
      charsToMorse ['U'] = Seq (Morse.Dot, Morse.Dot, Morse.Dash);
      charsToMorse ['V'] = Seq (Morse.Dot, Morse.Dot, Morse.Dot, Morse.Dash);
      charsToMorse ['W'] = Seq (Morse.Dot, Morse.Dash, Morse.Dash);
      charsToMorse ['X'] = Seq (Morse.Dash, Morse.Dot, Morse.Dot, Morse.Dash);
      charsToMorse ['Y'] = Seq (Morse.Dash, Morse.Dot, Morse.Dash, Morse.Dash);
      charsToMorse ['Z'] = Seq (Morse.Dash, Morse.Dash, Morse.Dot, Morse.Dot);
    }
    
    static Dictionary<char,IEnumerable<Morse>> charsToMorse;
  }
}

