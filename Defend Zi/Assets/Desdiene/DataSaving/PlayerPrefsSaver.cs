using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.DataSaving
{
    public class PlayerPrefsSaver : MonoBehaviourExt
    {
        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                PlayerPrefs.Save();
            }
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.Save();
        }
    }
}
