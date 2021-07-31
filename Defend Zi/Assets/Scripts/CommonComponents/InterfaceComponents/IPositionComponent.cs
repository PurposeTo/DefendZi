using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(IPosition))]
[DisallowMultipleComponent]
public class IPositionComponent : InterfaceComponent<IPosition> { }
