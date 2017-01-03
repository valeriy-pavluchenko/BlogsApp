using System.IO;

namespace BlogsApp.DataAccess.Tools
{
    /// <summary>
    /// File reader
    /// </summary>
    public static class FileReader
    {
        public static string GetFileContentAsString(string path)
        {
            string text;
            using (var stream = File.OpenText(path))
            {
                text = stream.ReadToEnd();
            }

            return text;
        }
    }
}
