namespace Desdiene.DataSaving.Datas
{
    public interface IValidData
    {
        bool IsValid();

        /// <summary>
        /// Если данные не прошли валидацию, починить их.
        /// </summary>
        void Repair();
    }
}
