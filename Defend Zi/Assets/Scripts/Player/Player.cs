using UnityEngine;

[RequireComponent(typeof(PlayerPosition))]
[RequireComponent(typeof(PlayerControl))]
public class Player : MonoBehaviour, IUserControllable
{
    private IUserControllable control;

    private void Awake()
    {
        IPosition position = gameObject.GetComponent<PlayerPosition>();
        control = gameObject.GetComponent<PlayerControl>().Constructor(position);
    }

    public void Control(IUserInput input) => control.Control(input);
}
