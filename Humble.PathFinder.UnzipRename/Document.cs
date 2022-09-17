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
        public string OringalName { get; private set; } = "";
        
        /// <summary>
        /// Gets the more human readable name for the file.
        /// </summary>
        public string NewName 
        {
            get
            {
                return baseName + ParseFileName(OringalName);
            }
        } 

        public Document(string fileName, string zipFolderName)
        {
            OringalName = fileName;
            baseName = ParseFolderName(zipFolderName);
        }

        internal static string ParseFolderName(string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                return string.Empty;

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
                if (name.StartsWith("PZO"))
                    continue;
                if (Regex.IsMatch(name, @"\d"))
                {
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
    }
}
