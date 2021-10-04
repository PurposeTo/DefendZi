using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(ITriggerable))]
[DisallowMultipleComponent]
public class ITriggerableComponent : InterfaceComponent<ITriggerable> { }
