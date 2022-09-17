using Microsoft.VisualStudio.TestTools.UnitTesting;
using Humble.PathFinder.UnzipRename;

namespace Humble.PathFinder.UnzipRename.Test
{
    [TestClass]
    public class TestDocument
    {
        [TestMethod]
        public void TestFolderNameParsing()
        {
            var FolderName = "PathfinderAdventurePath174ShadowsOfTheAncientsStrengthOfThousands6Of6PDF-SingleFile";
            var newName = Document.ParseFolderName(FolderName);
            Assert.AreEqual(newName, "Pathfinder Adventure Path 174");

            FolderName = "PathfinderAdvancedPlayersGuidePDF-FilePerChapter";
            newName = Document.ParseFolderName(FolderName);
            Assert.AreEqual(newName, "Pathfinder Advanced Players Guide");

            FolderName = "PathfinderAdvancedPlayersGuidePDF-SingleFile";
            newName = Document.ParseFolderName(FolderName);
            Assert.AreEqual(newName, "Pathfinder Advanced Players Guide");
        }

        [TestMethod]
        public void TestFileNameParsing()
        {
            var fileName = "PZO2105 APG 000 Cover-2nd Printing.pdf";
            var parseNaem = Document.ParseFileName(fileName);
            Assert.AreEqual(parseNaem, "APG p0 Cover.pdf");

            fileName = "PZO2105 APG 194-199-2nd Printing.pdf";
            parseNaem = Document.ParseFileName(fileName);
            Assert.AreEqual(parseNaem, "APG p194-199.pdf");

            fileName = "PZO90169E.pdf";
            parseNaem = Document.ParseFileName(fileName);
            Assert.AreEqual(parseNaem, ".pdf");

            fileName = "PZO90169E Interactive Maps.pdf";
            parseNaem = Document.ParseFileName(fileName);
            Assert.AreEqual(parseNaem, "Interactive Maps.pdf");
        }



        [TestMethod]
        [DataRow("PZO90174E.pdf", "PathfinderAdventurePath174ShadowsOfTheAncientsStrengthOfThousands6Of6PDF-SingleFile")]
        [DataRow("PZO90174E Interactive Maps.pdf", "PathfinderAdventurePath174ShadowsOfTheAncientsStrengthOfThousands6Of6PDF-SingleFile")]
        public void TestDocumentConstructor(string file, string folder)
        {

        }
    }
}