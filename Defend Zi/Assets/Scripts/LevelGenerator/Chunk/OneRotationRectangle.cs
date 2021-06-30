using System.Linq;
using Desdiene;
using Desdiene.Extensions.UnityEngine;
using UnityEngine;

public class OneRotationRectangle : Chunk
{
    [SerializeField, NotNull] private GameObject _obstacle;
    [SerializeField, NotNull] private AroundItsAxisMovement rotator;

    int[] hights = Enumerable.Range(-7, 7).ToArray();
    private readonly int[] directions = { -1, 1 };

    protected override void OnSpawn()
    {
        print(hights);

        _obstacle.transform
            .SetPositionOy(Randomizer.GetRandomItem(hights));

        float speed = Random.Range(30f, 90f);
        rotator.SetSpeed(Randomizer.GetRandomItem(directions) * speed);
    }
}
