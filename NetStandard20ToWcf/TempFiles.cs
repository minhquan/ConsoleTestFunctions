using System;
using System.Collections.Generic;
using System.Text;

namespace NetStandard20ToWcf
{
    public static class TempFiles
    {
        /// <summary>
        /// Writes the data out to a temporary file and returns the path to that file.
        /// </summary>
        public static string GetTempPath(byte[] data, string fileExtention)
        {
            if (data == null || fileExtention == null)
                return null;

            string fileName = GetPathForNewFile(fileExtention);
            System.IO.File.WriteAllBytes(fileName, data);
            return fileName;
        }

        /// <summary>
        /// Generates a full path to the temp file
        /// </summary>
        public static string GetPathForNewFile(string extension)
        {
            if (extension.Contains("."))
                extension = extension.Replace(".", "").Trim().ToLower();

            var webPublicPath = @"\\sqllive\WebPublic\WebPublic\";

            string fileName = String.Format(@"{2}Temp\{0}.{1}", Guid.NewGuid().ToString(), extension, webPublicPath);
            return fileName;
        }
    }
}
