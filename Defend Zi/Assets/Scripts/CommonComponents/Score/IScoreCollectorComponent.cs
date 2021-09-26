using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(IScoreCollector))]
[DisallowMultipleComponent]
public class IScoreCollectorComponent : InterfaceComponent<IScoreCollector> { }
