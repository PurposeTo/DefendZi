using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(IScoreNotification))]
[DisallowMultipleComponent]
public class IScoreNotificationComponent : InterfaceComponent<IScoreNotification> { }
