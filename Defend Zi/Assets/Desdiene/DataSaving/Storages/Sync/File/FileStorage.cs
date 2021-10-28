using System;
using Desdiene.DataSaving.Datas;

namespace Desdiene.DataSaving.Storages
{
    public abstract class FileStorage<T> : Storage<T> where T : IValidData
    {
        protected FileStorage(string storageName, string baseFileName, string fileExtension)
            : base(storageName)
        {

            if (string.IsNullOrWhiteSpace(baseFileName))
            {
                throw new ArgumentException($"{nameof(baseFileName)} can't be null or empty");
            }

            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                throw new ArgumentException($"{nameof(fileExtension)} can't be null or empty");
            }

            BaseFileName = baseFileName;
            FileExtension = fileExtension;
        }

        protected string BaseFileName { get; }
        protected string FileExtension { get; }
        protected string FileName => BaseFileName + "." + FileExtension;
    }
}
