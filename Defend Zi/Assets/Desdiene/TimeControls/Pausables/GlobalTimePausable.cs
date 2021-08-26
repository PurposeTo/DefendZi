using System.Collections;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls.Pausers;
using Desdiene.TimeControls.Scales;
using UnityEngine;
using Zenject;

namespace Desdiene.TimeControls.Pausables
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
