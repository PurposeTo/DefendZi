using System.Linq;
using Desdiene;
using Desdiene.Extensions.UnityEngine;
using UnityEngine;

public class OneRotationRectangle : Chunk
{
    [SerializeField, NotNull] private AroundItsAxisMovement _rotatingObstacle;

    int[] hights = Enumerable.Range(-7, 7).ToArray();
    private readonly int[] directions = { -1, 1 };

    protected override void OnSpawn()
    {
        _rotatingObstacle.transform
            .SetPositionOy(Randomizer.GetRandomItem(hights));

        float speed = Random.Range(15f, 60f);
        _rotatingObstacle.SetSpeed(Randomizer.GetRandomItem(directions) * speed);
    }
}
