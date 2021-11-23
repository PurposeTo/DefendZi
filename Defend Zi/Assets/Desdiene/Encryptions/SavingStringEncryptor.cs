using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Assets.Desdiene.Tools;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Tools;
using UnityEngine;

namespace Desdiene.Encryptions
{
    public sealed class SavingStringEncryptor : MonoBehaviourExtContainer
    {
        private const string _fileExtension = "txt";
        private const int salt = 100; // число не должно быть слишком большим, иначе будет ошибка
        private readonly string _hashDataFilePath;
        private readonly string _cryptoFileName;
        private readonly DeviceDataReader _deviceDataReader;

        public SavingStringEncryptor(MonoBehaviourExt mono, string baseFileName) : base(mono)
        {
            _cryptoFileName = $"{baseFileName}Alpha";
            _hashDataFilePath = new FilePath(FileNameWithExtension).Value;
            _deviceDataReader = new DeviceDataReader(mono, _hashDataFilePath);
        }

        private string FileNameWithExtension => _cryptoFileName + "." + _fileExtension;

        public string Encrypt(string data)
        {
            string saltedData = AddSalt(data);
            DeviceFile.WriteAllText(_hashDataFilePath, StringHash(saltedData));
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(saltedData));
        }

        public void Decrypt(string dataInBase64Encoding, Action<string> result)
        {
            if (string.IsNullOrEmpty(dataInBase64Encoding))
            {
                result?.Invoke(null);
                return;
            }

            if (!File.Exists(_hashDataFilePath))
            {
                result?.Invoke(null);
                return;
            }

            try
            {
                string saltedData = Encoding.UTF8.GetString(Convert.FromBase64String(dataInBase64Encoding));

                IsDataWasNotEdited(saltedData, (notEdited) =>
                {
                    bool edited = !notEdited;

                    if (edited)
                    {
                        result?.Invoke(null);
                        return;
                    }

                    string decryptedData = AddSalt(saltedData);
                    result?.Invoke(decryptedData);
                });
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
                result?.Invoke(null);
            }
        }

        public void Delete()
        {
            File.Delete(_hashDataFilePath);
        }

        private string StringHash(string data)
        {
            HashAlgorithm algorithm = SHA256.Create();
            StringBuilder stringBuilder = new StringBuilder();

            byte[] bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(data));
            foreach (byte item in bytes)
            {
                stringBuilder.Append(item.ToString("X2"));
            }

            return stringBuilder.ToString();
        }

        private void IsDataWasNotEdited(string data, Action<bool> result)
        {
            _deviceDataReader.Read((success, hashData) =>
            {
                if (!success)
                {
                    result?.Invoke(success);
                    return;
                }

                // Совпадает ли хэш считанных данных с хэшом ранее сохраненных данных?
                bool notEdited = StringHash(data) == hashData;
                result?.Invoke(notEdited);
            });
        }

        private string AddSalt(string data)
        {
            char[] dataAsCharArray = data.ToCharArray();
            StringBuilder saltedData = new StringBuilder();

            for (int i = 0; i < dataAsCharArray.Length; i++)
            {
                int saltedCharacter = Convert.ToInt32(dataAsCharArray[i]) ^ salt;
                saltedData.Append(Convert.ToChar(saltedCharacter));
            }

            return saltedData.ToString();
        }
    }
}
