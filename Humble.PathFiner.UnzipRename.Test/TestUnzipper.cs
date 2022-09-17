using Microsoft.VisualStudio.TestTools.UnitTesting;
using Humble.PathFinder.UnzipRename;

namespace Humble.PathFinder.UnzipRename.Test
{
    [TestClass]
    public class TestUnzipper
    {
        [TestMethod]
        public void TestZipName()
        {
            string file = @"D:\Temp\pathfinder\PathfinderAdvancedPlayersGuidePDF-FilePerChapter.zip";
            string name = @"D:\Temp\pathfinder\PathfinderAdvancedPlayersGuidePDF-FilePerChapter";
            string result = Unzipper.GetUnzipLocation(file);
            Assert.AreEqual(result, name);
        }
    }
}
