using System;
using System.Collections;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.TimeControls
{
    public class UpdateActionType : MonoBehaviour
    {
        private Dictionary<Type, Func<Func<bool>, Action<float>, IEnumerator>> _dict = new Dictionary<Type, Func<Func<bool>, Action<float>, IEnumerator>>
        {
            { Type.Update, (predicate, action) => Update.Routine(predicate, action) },
            { Type.Update, (predicate, action) => Update.RealTimeRoutine(predicate, action) },
            { Type.FixedUpdate, (predicate, action) => FixedUpdate.Routine(predicate, action) },
            { Type.FixedUpdateRealTime, (predicate, action) => FixedUpdate.RealTimeRoutine(predicate, action) },
        };

        public enum Type
        {
            Update,
            UpdateRealTime,
            FixedUpdate,
            FixedUpdateRealTime
        }

        public IEnumerator GetIEnumerator(Type scalingType, Func<bool> predicate, Action<float> action)
        {
            return _dict[scalingType].Invoke(predicate, action);
        }
    }
}
