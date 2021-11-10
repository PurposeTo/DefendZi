using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(IHealthNotification))]
[DisallowMultipleComponent]
public class IHealthNotificationComponent : InterfaceComponent<IHealthNotification> { }
