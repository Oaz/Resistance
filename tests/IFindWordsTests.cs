using System;
using System.Linq;
using NUnit.Framework;

namespace Resistance
{
  [TestFixture]
  public abstract class IFindWordsTests
  {
    protected abstract IFindWords CreateThesaurus (params string[] words);
    
    public virtual void Test1 ()
    {
      var thesaurus = CreateThesaurus ("A", "B", "C", "HELLO", "K", "WORLD");
      
      CheckWords (thesaurus, "-.-", "K");
      CheckLengths (thesaurus, "-.-", 3);
    }

    public virtual void Test2 ()
    {
      var thesaurus = CreateThesaurus ("GOD", "GOOD", "MORNING", "G", "HELLO");
      
      CheckWords (thesaurus, "--.-------..", "GOOD", "G");
      CheckLengths (thesaurus, "--.-------..", 12, 3);
      
      CheckWords (thesaurus, "-------..");
      CheckLengths (thesaurus, "-------..");
    }

    public virtual void Test3 ()
    {
      var thesaurus = CreateThesaurus ("HELL", "HELLO", "OWORLD", "WORLD", "TEST");
      
      CheckWords (thesaurus, "......-...-..---.-----.-..-..-..", "HELL", "HELLO");
      CheckLengths (thesaurus, "......-...-..---.-----.-..-..-..", 13, 16);
      
      CheckWords (thesaurus, "---.-----.-..-..-..", "OWORLD");
      CheckLengths (thesaurus, "---.-----.-..-..-..", 19);
      
      CheckWords (thesaurus, ".-----.-..-..-..", "WORLD");
      CheckLengths (thesaurus, ".-----.-..-..-..", 16);
    }

    public virtual void Test4 ()
    {
      var lines = System.IO.File.ReadAllLines ("tests/Test_4_input.txt");
      var words = lines.Skip (2).ToArray ();
      var thesaurus = new TreeThesaurus (words);
      foreach (var word in words) {
        var morse = word.ToMorse ();
        var startingWords = thesaurus.GetWordsStarting (morse);
        Assert.That (startingWords.Count (), Is.GreaterThanOrEqualTo (1));
      }
    }

    static void CheckWords (IFindWords thesaurus, string morseInput, params string[] expectedWords)
    {
      var words = thesaurus.GetWordsStarting (Convert.DotDashStringToMorse (morseInput));
      Assert.That (words.Select (r => r.Word).ToList (), Is.EquivalentTo (expectedWords));
    }

    static void CheckLengths (IFindWords thesaurus, string morseInput, params int[] expectedLengths)
    {
      var words = thesaurus.GetWordsStarting (Convert.DotDashStringToMorse (morseInput));
      Assert.That (words.Select (r => r.MorseLength).ToList (), Is.EquivalentTo (expectedLengths));
    }
  }
}

