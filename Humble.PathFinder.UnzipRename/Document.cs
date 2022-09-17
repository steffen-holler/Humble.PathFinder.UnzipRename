using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Humble.PathFinder.UnzipRename
{
    /// <summary>
    /// Internal values for the zip folder, these are typically pdf 
    /// but can also be PNG/JPGs.
    /// </summary>
    internal class Document
    {
        private string baseName = "";

        /// <summary>
        /// Gets the original name of the file assigned by Paizo. 
        /// </summary>
        public string OriginalName { get; private set; } = "";
        
        /// <summary>
        /// Gets the more human readable name for the file.
        /// </summary>
        public string NewName 
        {
            get
            {
                var fileName = ParseFileName(OriginalName);
                if (fileName.StartsWith("."))
                    return baseName + fileName;
                else 
                    return baseName + " - " + fileName;
            }
        } 

        /// <summary>
        /// Creates a new Paizo Document to be renamed. 
        /// </summary>
        /// <param name="fileName">Name of the file path including extension</param>
        /// <param name="zipFolderName">Name of the folder the file is within.</param>
        public Document(string fileName, string zipFolderName)
        {
            OriginalName = TrimFileName(fileName);
            baseName = ParseFolderName(zipFolderName);
        }

        /// <summary>
        /// parses the folder name to create the new base file name.
        /// </summary>
        /// <param name="folderName">Name of the folder containing the documents</param>
        /// <returns>Base Name for the documents</returns>
        internal static string ParseFolderName(string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                return string.Empty;

            folderName = TrimFileName(folderName);
            folderName = folderName.Replace(".zip", "");
            folderName = folderName.Replace(".ZIP", "");

            folderName = folderName.Replace("-SingleFile", "");
            folderName = folderName.Replace("-FilePerChapter", "");
            folderName = folderName.Replace("-OneFilePerChapter", "");
            folderName = folderName.Replace("PDF", "");

            string replacement = " ";
            var spaceOutRegEx = new Regex("(?<=[a-z])(?=[A-Z])|(?<=[A-Z])(?=[A-Z][a-z])|(?<=[0-9])(?=[A-Z][a-z])|(?<=[a-zA-Z])(?=[0-9])");
            string splitFolderName = spaceOutRegEx.Replace(folderName, replacement);

            if (Regex.IsMatch(splitFolderName, @"\d"))
            {
                // find the first set of number and delete the rest. 
                string[] name = splitFolderName.Split(' ');
                splitFolderName = "";
                foreach(var word in name)
                {
                    splitFolderName += word + " ";
                    if (Regex.IsMatch(word, @"\d"))
                        break;

                }
            }
            return splitFolderName.Trim();
        }

        /// <summary>
        /// parses the file name to create a file name for extra content.
        /// </summary>
        /// <param name="fileName">Name of the file to be parsed</param>
        /// <returns>Additional file names or the file extension to use.</returns>
        internal static string ParseFileName(string fileName)
        {
            string ext = fileName.Substring(fileName.LastIndexOf("."));
            fileName = fileName.Replace(ext, "");
            fileName = fileName.Replace("-2nd", "");
            fileName = fileName.Replace("-3rd", "");
            fileName = fileName.Replace(" Printing", "");

            string[] names = fileName.Split(' ');
            fileName = "";
            foreach (string name in names)
            {
                if (string.IsNullOrWhiteSpace(name))
                    continue;
                if (name.StartsWith("PZO"))
                    continue;
                if (Regex.IsMatch(name, @"^[0-9\-]*$"))
                {
                    if (name == "-")
                    {
                        fileName += name;
                        continue;
                    }
                    if (name.Contains("-"))
                    {
                        string[] numbers = name.Split('-');
                        string pageNumbers = "";
                        for(int i = 0; i < numbers.Length; i++)
                        {
                            pageNumbers += int.Parse(numbers[i]).ToString() + "-";
                        }
                        pageNumbers = pageNumbers.Substring(0, pageNumbers.LastIndexOf("-"));
                        fileName += "p" + pageNumbers + " ";
                    }
                    else
                    {
                        int pageNumber = int.Parse(name);
                        fileName += "p" + pageNumber.ToString() + " ";
                    }
                }

                else 
                    fileName += name + " ";
            }
            


            return fileName.Trim() + ext;
        }

        internal static string TrimFileName(string filePath)
        {
            string fileName = filePath.Replace('/', '\\');
            if (fileName.Contains("\\"))
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            return fileName;
        }
    }
}
