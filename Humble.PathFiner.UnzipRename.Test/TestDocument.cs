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

            FolderName = "PathfinderAdvancedPlayersGuidePDF-FilePerChapter.zip";
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
        public void TestDocumentConstructor()
        {
            var file = "PZO90174E.pdf";
            var folder = "PathfinderAdventurePath174ShadowsOfTheAncientsStrengthOfThousands6Of6PDF-SingleFile";
            var doc = new Document(file, folder);
            Assert.AreEqual(doc.OriginalName, file);
            Assert.AreEqual(doc.NewName, "Pathfinder Adventure Path 174.pdf");

            file = "PZO90174E Interactive Maps.pdf";
            folder = "PathfinderAdventurePath174ShadowsOfTheAncientsStrengthOfThousands6Of6PDF-SingleFile";
            doc = new Document(file, folder);
            Assert.AreEqual(doc.NewName, "Pathfinder Adventure Path 174 - Interactive Maps.pdf");

            file = "PZO2105 APG 000 Cover-2nd Printing.pdf";
            folder = "PathfinderAdvancedPlayersGuidePDF-FilePerChapter";
            doc = new Document(file, folder);
            Assert.AreEqual(doc.NewName, "Pathfinder Advanced Players Guide - APG p0 Cover.pdf");

            file = "PZO2105 APG 194-199-2nd Printing.pdf";
            doc = new Document(file, folder);
            Assert.AreEqual(doc.NewName, "Pathfinder Advanced Players Guide - APG p194-199.pdf");
        }
    }
}