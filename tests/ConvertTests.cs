using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Resistance
{
  [TestFixture]
  public class ConvertTests
  {
    [Test]
    public void CharToMorse ()
    {
      Assert.That ('a'.ToMorse (), Is.EquivalentTo (Convert.Seq (Morse.Dot, Morse.Dash)));
      Assert.That ('A'.ToMorse (), Is.EquivalentTo (Convert.Seq (Morse.Dot, Morse.Dash)));
      Assert.That ('b'.ToMorse (), Is.EquivalentTo (Convert.Seq (Morse.Dash, Morse.Dot, Morse.Dot, Morse.Dot)));
      Assert.That ('$'.ToMorse (), Is.Empty);
    }
    
    [Test]
    public void TextToMorse ()
    {
      var refseq = Convert.Seq (Morse.Dash, Morse.Dot, Morse.Dot, Morse.Dot, Morse.Dot, Morse.Dash, Morse.Dash, Morse.Dot, Morse.Dash, Morse.Dot);
      Assert.That ("Bac".ToMorse (), Is.EquivalentTo (refseq));
      Assert.That ("BaNN".ToMorse (), Is.EquivalentTo (refseq));
      Assert.That ("DUC".ToMorse (), Is.EquivalentTo (refseq));
      Assert.That ("BA".ToMorse (), Is.EquivalentTo (Convert.Seq (Morse.Dash, Morse.Dot, Morse.Dot, Morse.Dot, Morse.Dot, Morse.Dash)));
      Assert.That ("".ToMorse (), Is.Empty);
    }
    
    [Test]
    public void DotDashStringToMorse ()
    {
      Assert.That (Convert.DotDashStringToMorse ("."), Is.EquivalentTo (Convert.Seq (Morse.Dot)));
      Assert.That (Convert.DotDashStringToMorse ("-"), Is.EquivalentTo (Convert.Seq (Morse.Dash)));
      var refseq = Convert.Seq (Morse.Dash, Morse.Dot, Morse.Dot, Morse.Dot, Morse.Dot, Morse.Dash, Morse.Dash, Morse.Dot, Morse.Dash, Morse.Dot);
      Assert.That (Convert.DotDashStringToMorse ("-....--.-."), Is.EquivalentTo (refseq));
      Assert.That (Convert.DotDashStringToMorse (""), Is.Empty);
    }
    
    [Test]
    public void MorseToDotDashString ()
    {
      Assert.That (Convert.MorseToDotDashString (Convert.Seq (Morse.Dot)), Is.EqualTo ("."));
      Assert.That (Convert.MorseToDotDashString (Convert.Seq (Morse.Dash)), Is.EqualTo ("-"));
      var refseq = Convert.Seq (Morse.Dash, Morse.Dot, Morse.Dot, Morse.Dot, Morse.Dot, Morse.Dash, Morse.Dash, Morse.Dot, Morse.Dash, Morse.Dot);
      Assert.That (Convert.MorseToDotDashString (refseq), Is.EqualTo ("-....--.-."));
      Assert.That (Convert.MorseToDotDashString (Convert.Seq<Morse> ()), Is.EqualTo (""));
    }
  }
}

