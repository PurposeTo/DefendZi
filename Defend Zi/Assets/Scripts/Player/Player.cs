using UnityEngine;

[RequireComponent(typeof(PlayerPosition))]
[RequireComponent(typeof(PlayerControl))]
public class Player : MonoBehaviour, IUserControllable
{
    private PlayerPosition position;
    private PlayerControl control;

    private void Awake()
    {
        position = gameObject.GetComponent<PlayerPosition>();
        control = gameObject.GetComponent<PlayerControl>();
        control.Constructor(position);
    }

    public void Control(IUserInput input) => control.Control(input);
}
