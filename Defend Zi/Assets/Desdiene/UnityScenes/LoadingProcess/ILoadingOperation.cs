using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desdiene.UnityScenes.LoadingProcess
{
    public interface ILoadingOperation
    {
        /// <summary>
        /// Событие вызывается при включении состояния ожидания разрешения на активацию сцены
        /// </summary>
        event Action OnWaitingForAllowingToEnabling;

        /// <summary>
        /// Событие вызывается после загрузки и включении сцены.
        /// </summary>
        event Action OnLoadedAndEnabled;
    }
}
