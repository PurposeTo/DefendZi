using System;
using Desdiene.MonoBehaviourExtension;

namespace Desdiene.Containers
{
    /// <summary>
    /// Содержит поле superMonoBehaviour, которое инициализируется конструктором
    /// </summary>
    public abstract class MonoBehaviourExtContainer
    {
        protected readonly MonoBehaviourExt monoBehaviourExt;

        public MonoBehaviourExtContainer(MonoBehaviourExt monoBehaviourExt)
        {
            this.monoBehaviourExt = monoBehaviourExt != null
                ? monoBehaviourExt
                : throw new ArgumentNullException(nameof(monoBehaviourExt));
        }
    }
}
