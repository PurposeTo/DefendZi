using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(IPositionAccessorNotifier))]
[DisallowMultipleComponent]
public class IPositionAccessorNotifierComponent : InterfaceComponent<IPositionAccessorNotifier> { }
