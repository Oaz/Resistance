using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Resistance
{
  [TestFixture]
  public abstract class IDecodeMessageTests
  {
    protected abstract IDecodeMessage CreateDecoder ();
      
    private void CheckMessageDecoding (
      IFindWords thesaurus,
      string input,
      IEnumerable<IEnumerable<string>> expectedOutput
      )
    {
      var decoder = CreateDecoder ();
      var candidatesCount = decoder.Count (Convert.DotDashStringToMorse (input), thesaurus);
      Assert.That (candidatesCount, Is.EqualTo (expectedOutput.Count ()));
      
      var candidates = decoder.Decode (Convert.DotDashStringToMorse (input), thesaurus);
      Assert.That (NormalizeResults (candidates), Is.EquivalentTo (NormalizeResults (expectedOutput)));
    }
    
    List<string> NormalizeResults (IEnumerable<IEnumerable<string>> results)
    {
      return results.Select (x => string.Join ("-", x)).OrderBy (s => s).ToList ();
    }

    protected void Test1 (Func<IEnumerable<string>,IFindWords> getThesaurus)
    {
      CheckMessageDecoding (
        getThesaurus (Convert.Seq ("A", "B", "C", "HELLO", "K", "WORLD")),
        "-.-",
        Convert.Seq (Convert.Seq ("K"))
      );
    }
    
    protected void Test2 (Func<IEnumerable<string>,IFindWords> getThesaurus)
    {
      CheckMessageDecoding (
        getThesaurus (Convert.Seq ("GOD", "G", "GOOD", "MORNING", "HELLO")),
        "--.-------..",
        Convert.Seq (Convert.Seq ("GOOD"))
      );
    }
    
    protected void Test3 (Func<IEnumerable<string>,IFindWords> getThesaurus)
    {
      CheckMessageDecoding (
        getThesaurus (Convert.Seq ("HELL", "HELLO", "OWORLD", "WORLD", "TEST")),
        "......-...-..---.-----.-..-..-..",
        Convert.Seq (Convert.Seq ("HELL", "OWORLD"), Convert.Seq ("HELLO", "WORLD"))
      );
    }
    
    protected void Test3b (Func<IEnumerable<string>,IFindWords> getThesaurus)
    {
      CheckMessageDecoding (
        getThesaurus (Convert.Seq ("HELL", "HELLO", "OWORLD", "WORLD", "TEST")),
        "......-...-..---.-----.-..-..-........-...-..---.-----.-..-..-........-...-..---.-----.-..-..-..",
        Convert.Seq (
          Convert.Seq ("HELL", "OWORLD", "HELL", "OWORLD", "HELL", "OWORLD"),
          Convert.Seq ("HELL", "OWORLD", "HELL", "OWORLD", "HELLO", "WORLD"),
          Convert.Seq ("HELL", "OWORLD", "HELLO", "WORLD", "HELL", "OWORLD"),
          Convert.Seq ("HELL", "OWORLD", "HELLO", "WORLD", "HELLO", "WORLD"),
          Convert.Seq ("HELLO", "WORLD", "HELL", "OWORLD", "HELL", "OWORLD"),
          Convert.Seq ("HELLO", "WORLD", "HELL", "OWORLD", "HELLO", "WORLD"),
          Convert.Seq ("HELLO", "WORLD", "HELLO", "WORLD", "HELL", "OWORLD"),
          Convert.Seq ("HELLO", "WORLD", "HELLO", "WORLD", "HELLO", "WORLD")
        )
      );
    }
    
    protected void Test3c (Func<IEnumerable<string>,IFindWords> getThesaurus)
    {
      var longSequence = Convert.DotDashStringToMorse ("xxxxxxxxxxxxxxxx".Replace ("x", "......-...-..---.-----.-..-..-..")).ToArray ();
      var thesaurus = getThesaurus (Convert.Seq ("HELL", "HELLO", "OWORLD", "WORLD", "TEST"));
      var decoder = CreateDecoder ();
      Assert.That (decoder.Count (longSequence, thesaurus), Is.EqualTo (65536));
    }
    
    protected void Test3d (Func<IEnumerable<string>,IFindWords> getThesaurus)
    {
      CheckMessageDecoding (
        getThesaurus (Convert.Seq ("HELL", "HELLO", "OWORLD", "OWORL", "WORL", "DHELL", "DHELLO", "WORLD", "TEST")),
        "......-...-..---.-----.-..-..-........-...-..---.-----.-..-..-..",
        Convert.Seq (
          Convert.Seq ("HELL", "OWORLD", "HELL", "OWORLD"),
          Convert.Seq ("HELL", "OWORL", "DHELL", "OWORLD"),
          Convert.Seq ("HELL", "OWORL", "DHELLO", "WORLD"),
          Convert.Seq ("HELL", "OWORLD", "HELLO", "WORLD"),
          Convert.Seq ("HELLO", "WORL", "DHELL", "OWORLD"),
          Convert.Seq ("HELLO", "WORL", "DHELLO", "WORLD"),
          Convert.Seq ("HELLO", "WORLD", "HELL", "OWORLD"),
          Convert.Seq ("HELLO", "WORLD", "HELLO", "WORLD")
        )
      );
    }
    
    protected void Test4 (Func<IEnumerable<string>,IFindWords> getThesaurus)
    {
      var lines = System.IO.File.ReadAllLines ("tests/Test_4_input.txt");
      var thesaurus = getThesaurus (lines.Skip (2).ToArray ());
      var decoder = CreateDecoder ();
      var count = decoder.Count (Convert.DotDashStringToMorse (lines [0]), thesaurus);
      Assert.That (count, Is.EqualTo (57330892800));
    }


  }
}

