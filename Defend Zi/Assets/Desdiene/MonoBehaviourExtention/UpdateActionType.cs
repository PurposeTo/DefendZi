using System;
using System.Collections;
using System.Collections.Generic;

namespace Desdiene.MonoBehaviourExtension
{
    public static class UpdateActionType
    {
        private static Dictionary<Mode, Func<Func<bool>, Action<float>, IEnumerator>> _dict = new Dictionary<Mode, Func<Func<bool>, Action<float>, IEnumerator>>
        {
            { Mode.Update, (predicate, action) => Update.Routine(predicate, action) },
            { Mode.UpdateRealTime, (predicate, action) => Update.RealTimeRoutine(predicate, action) },
            { Mode.FixedUpdate, (predicate, action) => FixedUpdate.Routine(predicate, action) },
            { Mode.FixedUpdateRealTime, (predicate, action) => FixedUpdate.RealTimeRoutine(predicate, action) },
        };

        public enum Mode
        {
            Update,
            UpdateRealTime,
            FixedUpdate,
            FixedUpdateRealTime
        }

        public static IEnumerator GetIEnumerator(Mode updatingMode, Func<bool> predicate, Action<float> action)
        {
            return _dict[updatingMode].Invoke(predicate, action);
        }
    }
}
