using System;
namespace Desdiene.DataStorageFactories.Validators
{
    public interface IDataValidator
    {
        bool IsValid();

        /// <summary>
        /// Если данные не прошли валидацию, починить их.
        /// </summary>
        void TryToRepair();
    }
}
