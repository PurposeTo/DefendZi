using System.Collections;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControl.Pauser;
using Desdiene.TimeControl.Scale;
using UnityEngine;
using Zenject;

namespace Desdiene.TimeControl.Pausable
{
    /// <summary>
    /// Позволяет корректно ставить игровое время на паузу.
    /// Need to be a singleton!
    /// </summary>
    public class GlobalTimePausable : MonoBehaviourExt, ITimePausable
    {
        private PausableTime pausable;

        [Inject]
        public void Constructor(GlobalTimeScaler globalTimeScaler)
        {
            pausable = new PausableTime(globalTimeScaler);
        }

        public bool IsPause => pausable.IsPause;

        public void Add(ITimePauser pauser) => pausable.Add(pauser);

        public void Remove(ITimePauser pauser) => pausable.Remove(pauser);
    }
}
