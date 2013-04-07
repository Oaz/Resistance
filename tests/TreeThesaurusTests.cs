using System;
using NUnit.Framework;

namespace Resistance
{
  public class TreeThesaurusTests : IFindWordsTests
  {
    protected override IFindWords CreateThesaurus (params string[] words)
    {
      return new TreeThesaurus (words);
    }
    
    [Test]
    public override void Test1 ()
    {
      base.Test1 ();
    }

    [Test]
    public override void Test2 ()
    {
      base.Test2 ();
    }

    [Test]
    public override void Test3 ()
    {
      base.Test3 ();
    }

    [Test]
    public override void Test4 ()
    {
      base.Test4 ();
    }
  }
}


