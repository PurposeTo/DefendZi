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
        print(hights);

        _rotatingObstacle.transform
            .SetPositionOy(Randomizer.GetRandomItem(hights));

        float speed = Random.Range(30f, 90f);
        _rotatingObstacle.SetSpeed(Randomizer.GetRandomItem(directions) * speed);
    }
}