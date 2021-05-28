using System;
using Desdiene.MonoBehaviourExtention;

namespace Desdiene.Container
{
    /// <summary>
    /// Содержит поле superMonoBehaviour, которое инициализируется конструктором
    /// </summary>
    public abstract class MonoBehaviourExtContainer
    {
        protected readonly MonoBehaviourExt monoBehaviourExt;

        public MonoBehaviourExtContainer(MonoBehaviourExt monoBehaviourExt)
        {
            this.monoBehaviourExt = monoBehaviourExt
                ? monoBehaviourExt
                : throw new ArgumentNullException(nameof(monoBehaviourExt));
        }
    }
}
