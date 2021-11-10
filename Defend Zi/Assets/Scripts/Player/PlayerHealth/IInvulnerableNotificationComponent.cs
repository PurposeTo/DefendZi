using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(IInvulnerableNotification))]
[DisallowMultipleComponent]
public class IInvulnerableNotificationComponent : InterfaceComponent<IInvulnerableNotification> { }
