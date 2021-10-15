using System;
using System.IO;
using System.Text;

namespace Assets.Desdiene.Tools
{
    /// <summary>
    /// Определяет методы записи в файл для path, использующих схему "file:///"
    /// </summary>
    public static class LocalFile
    {
        public static void WriteAllText(string path, string content)
        {
            WriteAllText(path, content, Encoding.Default);
        }

        public static void WriteAllText(string path, string content, Encoding encoding)
        {
            string localPath = new Uri(path).LocalPath;
            File.WriteAllText(localPath, content, encoding);
        }
    }
}
