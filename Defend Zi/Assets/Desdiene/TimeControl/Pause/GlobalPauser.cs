using System.Collections.Generic;
using System.Linq;
using Assets.Desdiene.TimeControl.Pause;
using Desdiene.Singleton;
using Desdiene.TimeControl.Scale;
using UnityEngine;

namespace Desdiene.TimeControl.Pause
{
    public class GlobalPauser : LazySingleton<GlobalPauser>
    {
        private readonly GlobalTimeScaler timeScaler = new GlobalTimeScaler();
        private readonly List<PausableGlobalTime> pausables = new List<PausableGlobalTime>();

        public void Add(PausableGlobalTime pausable)
        {
            if (pausable == null) throw new System.ArgumentNullException(nameof(pausable));

            if (pausables.Contains(pausable))
            {
                Debug.LogWarning($"GlobalPause is already contains {pausable} with name {pausable.Name} in pausable list!");
            }
            else
            {
                if (pausables.Any(item => item.Name == pausable.Name))
                {
                    Debug.LogError($"GlobalPause pausable list is already contains item with name {pausable.Name}, but it is not the same item!");
                }
                else
                {
                    pausable.OnPauseChanged += UpdateTotalPause;
                    pausables.Add(pausable);
                    UpdateTotalPause();
                }
            }
        }

        public void Remove(PausableGlobalTime pausable)
        {
            if (pausable == null) throw new System.ArgumentNullException(nameof(pausable));

            if (pausables.Contains(pausable))
            {
                pausable.OnPauseChanged -= UpdateTotalPause;
                pausables.Remove(pausable);
                UpdateTotalPause();
            }
            else Debug.LogWarning($"GlobalPause is NOT contains {pausable} with name {pausable.Name} in pausable list!");
        }

        public bool IsPause => timeScaler.IsPause;

        private void UpdateTotalPause() => timeScaler.SetPause(CalculateTotalPause());

        private bool CalculateTotalPause()
        {
            return pausables.Any(item => item.IsPause);
        }
    }
}
