using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Humble.PathFinder.UnzipRename
{
    /// <summary>
    /// Main unzipper execution class. 
    /// </summary>
    public static class Unzipper
    {
        /// <summary>
        /// Paizo Archvie unzipper
        /// </summary>
        /// <param name="args">
        /// Command line arguments 
        /// </param>
        public static void Main(string[] args)
        {
            string unzipFolder = Environment.CurrentDirectory;
            if (args.Length == 1)
            {
                unzipFolder = args[0];
                Environment.CurrentDirectory = unzipFolder;
            }
            string destination = unzipFolder + "\\complete";
            if (args.Length >= 2)
                destination = args[1];
            if (Directory.Exists(destination))
                Directory.Delete(destination, true);
            Directory.CreateDirectory(destination);

            Console.WriteLine("Unzipping zip files in : " + unzipFolder);
            Console.WriteLine("TO : " + destination);
            Console.WriteLine();

            // unzip our files.
            string[] zips = Directory.GetFiles(unzipFolder, "*.zip");
            foreach(var zip in zips)
            {
                var tempDir = GetUnzipLocation(zip);
                var docs = Unzip(zip, tempDir);
                
                // rename the files 
                foreach (var doc in docs)
                {
                    string origName = tempDir + "\\" + doc.OriginalName;
                    string destName = destination + "\\" + doc.NewName;
                    while (File.Exists(destName))
                        destName = destName.Substring(0, destName.LastIndexOf(".")) + "-copy"
                            + destName.Substring(destName.LastIndexOf("."));

                    Console.WriteLine("Moving " + doc.OriginalName + " to " + destName);

                    File.Move(origName, destName);
                }

                if (Directory.Exists(tempDir))
                    Directory.Delete(tempDir, true);
            }
        }

        /// <summary>
        /// unzips the files and add them to the documents list
        /// </summary>
        /// <param name="zipFile">File path to the zip file</param>
        /// <param name="dest">Destination to unzip the zip file</param>
        /// <returns>Array of documents within the zip file </returns>
        internal static Document[] Unzip(string zipFile, string dest)
        {
            if (Directory.Exists (dest))
                Directory.Delete (dest, true);

            var last = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Unzipping " + zipFile);
            Console.ForegroundColor = last;

            ZipFile.ExtractToDirectory(zipFile, dest);
            Document[] docs = GetDocuments(zipFile, dest, dest);
            return docs;
        }

        /// <summary>
        /// Gets a temp name for unzipping the documents
        /// </summary>
        /// <param name="zipFile">File Path to the zip file</param>
        /// <returns>Temp directory for the zip file </returns>
        internal static string GetUnzipLocation(string zipFile)
        {
            return zipFile.Substring(0, zipFile.LastIndexOf('.'));
        }

        /// <summary>
        /// Gets and flattens out the zip file.
        /// </summary>
        /// <param name="zipFile">File path to the zip file</param>
        /// <param name="folder">Current directory location</param>
        /// <param name="orig">Top level directory location</param>
        /// <returns></returns>
        internal static Document[] GetDocuments(string zipFile, string folder, string orig)
        {
            List<Document> docs = new List<Document>();
            string[] subDir = Directory.GetDirectories(folder);
            foreach (string dir in subDir)
            {
                string[] subFiles = Directory.GetFiles(dir);
                foreach (string subFile in subFiles)
                {
                    string newName = subFile.Substring(subFile.LastIndexOf('\\'));
                    newName = orig + newName;
                    while (File.Exists(newName))
                        newName = newName.Substring(0, newName.LastIndexOf(".")) + "-copy"
                            + newName.Substring(newName.LastIndexOf("."));
                    File.Move(subFile, newName);
                };                        
                docs.AddRange(GetDocuments(zipFile, dir, orig));
                
            }
            string[] files = Directory.GetFiles(folder);
            for (int i = 0; i < files.Length; i++)
            {
                docs.Add(new Document(files[i], zipFile));
            }
            return docs.ToArray();
        }
    }
}
