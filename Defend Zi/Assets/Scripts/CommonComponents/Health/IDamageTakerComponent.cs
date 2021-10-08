using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(IDamageTaker))]
[DisallowMultipleComponent]
public class IDamageTakerComponent : InterfaceComponent<IDamageTaker> { }
