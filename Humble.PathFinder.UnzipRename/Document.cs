using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private string fileName = "";

        /// <summary>
        /// Gets the original name of the file assigned by Paizo. 
        /// </summary>
        public string OringalName { get; init; } = "";
        
        /// <summary>
        /// Gets the more human readable name for the file.
        /// </summary>
        public string NewName 
        {
            get { return fileName; }
            internal set
            {
                // todo: parse file name
                fileName = value;
            }
        } 

        public Document(string orignialName, string zipFolderName)
        {
            OringalName = orignialName;
            baseName = ParseFolderName(zipFolderName);

        }

        internal static string ParseFolderName(string folderName)
        {
            return folderName;
        }
    }
}
