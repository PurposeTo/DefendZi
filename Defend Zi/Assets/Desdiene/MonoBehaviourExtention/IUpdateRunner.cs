using System;

namespace Desdiene.MonoBehaviourExtension
{
    public interface IUpdateRunner
    {
        void AddUpdate(Action action);
        void RemoveUpdate(Action action);

        void AddLateUpdate(Action action);
        void RemoveLateUpdate(Action action);

        void AddFixedUpdate(Action action);
        void RemoveFixedUpdate(Action action);
    }
}
