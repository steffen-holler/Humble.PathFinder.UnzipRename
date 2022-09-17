using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Humble.PathFiner.UnzipRename.Test
{
    [TestClass]
    public class TestDocument
    {
        [TestMethod]
        public void TestFolderNameParsing(string FolderName)
        {
            var doc = 
            Assert.IsTrue()
        }

        [TestMethod]
        [DataRow("PZO90174E.pdf", "PathfinderAdventurePath174ShadowsOfTheAncientsStrengthOfThousands6Of6PDF-SingleFile")]
        [DataRow("PZO90174E Interactive Maps.pdf", "PathfinderAdventurePath174ShadowsOfTheAncientsStrengthOfThousands6Of6PDF-SingleFile")]
        public void TestDocument(string file, string folder)
        {

        }
    }
}