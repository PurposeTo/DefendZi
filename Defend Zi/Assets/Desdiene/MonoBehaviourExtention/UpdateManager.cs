using System;
using System.Collections.Generic;
using Desdiene.Singletons.Unity;
using UnityEngine;

namespace Desdiene.MonoBehaviourExtension
{
    public class UpdateManager : GlobalSingleton<UpdateManager>, IUpdateManager
    {
        private List<Action> _updates = new List<Action>();
        private List<Action> _lateUpdates = new List<Action>();
        private List<Action> _fixedUpdates = new List<Action>();

        private void Update()
        {
            for (int i = 0; i < _updates.Count; i++)
            {
                _updates[i]?.Invoke();
            }
        }

        private void LateUpdate()
        {
            for (int i = 0; i < _lateUpdates.Count; i++)
            {
                _lateUpdates[i]?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fixedUpdates.Count; i++)
            {
                _fixedUpdates[i]?.Invoke();
            }
        }

        public void AddUpdate(Action action) => _updates.Add(action);
        public void RemoveUpdate(Action action) => _updates.Remove(action);

        public void AddLateUpdate(Action action) => _lateUpdates.Add(action);
        public void RemoveLateUpdate(Action action) => _lateUpdates.Remove(action);

        public void AddFixedUpdate(Action action) => _fixedUpdates.Add(action);
        public void RemoveFixedUpdate(Action action) => _fixedUpdates.Remove(action);
    }
}
