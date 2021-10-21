using System;
using System.IO;
using System.Text;

namespace Assets.Desdiene.Tools
{
    /// <summary>
    /// Определяет методы записи в файл для пути, использующих схему "file:///"
    /// Путь к файлу на устройстве всегда начинается с этого префикса; 
    /// Но если передать путь с явным его указанием в File.WriteAllText, то тот не сможет его обработать. 
    /// Для этого в классе происходит нормализация пути.
    /// </summary>
    public static class DeviceFile
    {
        public static void WriteAllText(string path, string content)
        {
            WriteAllText(path, content, Encoding.Default);
        }

        public static void WriteAllText(string path, string content, Encoding encoding)
        {
            // Это нормализация пути, а не получение локального пути
            string localPath = new Uri(path).LocalPath;
            File.WriteAllText(localPath, content, encoding);
        }
    }
}
