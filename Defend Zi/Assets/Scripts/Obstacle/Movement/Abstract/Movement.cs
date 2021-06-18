using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public abstract class Movement : MonoBehaviourExt
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        Move(_speed * Time.fixedDeltaTime);
    }

    protected abstract void Move(float deltaDistance);
}
