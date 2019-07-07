using InversalTranslatorTemp;
using NUnit.Framework;
using System.IO;
using static InversalTranslatorTemp.Converter;

namespace Test
{   
    [TestFixture]
    public class Class1
    {
        [Test]
        public void TestNoFile()
        {
            Assert.Throws<NoFile>(() => new Converter("ExampleTest"));
        }
        [Test]
        public void InvalidGrade()
        {   
            Assert.Throws<InvalidGrade>(() => new Converter("ExampleTestInvalid.txt"));
        }
        [Test]
        public void CtoF()
        {
            Converter conv = new Converter("ExampleTest.txt");
            Assert.AreEqual(conv.Temps[0].FinalTemp,185);
        }
        [Test]
        public void KtoC()
        {
            Converter conv = new Converter("ExampleTest.txt");
            Assert.AreEqual(conv.Temps[1].FinalTemp, -73.15);
        }
        [Test]
        public void FtoC()
        {
            Converter conv = new Converter("ExampleTest.txt");
            Assert.AreEqual(conv.Temps[2].FinalTemp, 35.56);
        }
        [Test]
        public void CtoK()
        {
            Converter conv = new Converter("ExampleTest.txt");
            Assert.AreEqual(conv.Temps[3].FinalTemp, 352.15);
        }
        [Test]
        public void ctoC()
        {
            Converter conv = new Converter("ExampleTest.txt");
            Assert.AreEqual(conv.Temps[4].FinalTemp, 85);
        }
        [Test]
        public void KtoK()
        {
            Converter conv = new Converter("ExampleTest.txt");
            Assert.AreEqual(conv.Temps[5].FinalTemp, 16);
        }
        [Test]
        public void FtoF()
        {
            Converter conv = new Converter(@"ExampleTest.txt");
            Assert.AreEqual(conv.Temps[6].FinalTemp, 12);
        }
    }
}
