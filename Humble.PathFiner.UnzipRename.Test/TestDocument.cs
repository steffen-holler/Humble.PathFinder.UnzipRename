using Microsoft.VisualStudio.TestTools.UnitTesting;
using Humble.PathFiner.UnzipRename;

namespace Humble.PathFiner.UnzipRename.Test
{
    [TestClass]
    public class TestDocument
    {
        [TestMethod]
        public void TestFolderNameParsing(string FolderName)
        {
            
        }

        [TestMethod]
        [DataRow("PZO90174E.pdf", "PathfinderAdventurePath174ShadowsOfTheAncientsStrengthOfThousands6Of6PDF-SingleFile")]
        [DataRow("PZO90174E Interactive Maps.pdf", "PathfinderAdventurePath174ShadowsOfTheAncientsStrengthOfThousands6Of6PDF-SingleFile")]
        public void TestDocumentConstructor(string file, string folder)
        {

        }
    }
}