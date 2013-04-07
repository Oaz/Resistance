using System;
using NUnit.Framework;

namespace Resistance
{
  [TestFixture]
  public class CachedDeepSearchDecoderTests : IDecodeMessageTests
  {
    protected override IDecodeMessage CreateDecoder ()
    {
      return new CachedDeepSearchDecoder ();
    }

    [Test]
    public void Test1_Dumb ()
    {
      Test1 (w => new DumbThesaurus (w));
    }
    
    [Test]
    public void Test1_Tree ()
    {
      Test1 (w => new TreeThesaurus (w));
    }
      
    [Test]
    public void Test2_Dumb ()
    {
      Test2 (w => new DumbThesaurus (w));
    }
    
    [Test]
    public void Test2_Tree ()
    {
      Test2 (w => new TreeThesaurus (w));
    }
       
    [Test]
    public void Test3_Dumb ()
    {
      Test3 (w => new DumbThesaurus (w));
    }
    
    [Test]
    public void Test3_Tree ()
    {
      Test3 (w => new TreeThesaurus (w));
    }
       
    [Test]
    public void Test3b_Dumb ()
    {
      Test3b (w => new DumbThesaurus (w));
    }
    
    [Test]
    public void Test3b_Tree ()
    {
      Test3b (w => new TreeThesaurus (w));
    }
       
    [Test]
    public void Test3c_Dumb ()
    {
      Test3c (w => new DumbThesaurus (w));
    }
    
    [Test]
    public void Test3c_Tree ()
    {
      Test3c (w => new TreeThesaurus (w));
    }
       
    [Test]
    public void Test3d_Dumb ()
    {
      Test3d (w => new DumbThesaurus (w));
    }
    
    [Test]
    public void Test3d_Tree ()
    {
      Test3d (w => new TreeThesaurus (w));
    }
    
    [Test]
    public void Test4_Dumb ()
    {
      Test4 (w => new DumbThesaurus (w));
    }
    
    [Test]
    public void Test4_Tree ()
    {
      Test4 (w => new TreeThesaurus (w));
    }
    
  }
}

