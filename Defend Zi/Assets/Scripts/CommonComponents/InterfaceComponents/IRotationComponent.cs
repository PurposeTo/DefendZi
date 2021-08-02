using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(IRotation))]
[DisallowMultipleComponent]
public class IRotationComponent : InterfaceComponent<IRotation> { }
