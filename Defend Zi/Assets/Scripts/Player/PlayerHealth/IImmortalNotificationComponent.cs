using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(IImmortalNotification))]
[DisallowMultipleComponent]
public class IImmortalNotificationComponent : InterfaceComponent<IImmortalNotification> { }
