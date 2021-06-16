using System.Collections.Generic;
using System.Linq;
using Desdiene;
using Desdiene.Extensions.UnityEngine;
using UnityEngine;

public class OldLevelGenerator : MonoBehaviour
{
    [SerializeField, NotNull] private GameObject obstacle;
    [SerializeField] private int distance = 5;
    [SerializeField] private int startLevelPoint = 6;
    [SerializeField] private int endLevelPoint = 1000;
    [SerializeField] private int randomCount = 2;

    private readonly float[] hights = { -7, -4.5f, -3.75f, -3, -2, -1, 0, 1, 2, 3, 3.75f, 4.5f, 7 };
    private float[] rotations;
    private readonly float eulerStep = 7.5f;

    private void Awake()
    {
        InitRotations();

        Math.Compare(ref startLevelPoint, ref endLevelPoint);
        List<int> randomCordinates = GetRandomCoordinates();
        List<Vector2> randomVectors = GetRandomVectors2(randomCordinates);
        SpawnObstacles(randomVectors);
    }

    private void InitRotations()
    {
        rotations = Enumerable.Range(0, (int)(360 / eulerStep))
            .Select(euler => euler * eulerStep)
            .ToArray();
    }

    private void SpawnObstacles(List<Vector2> vectors)
    {
        vectors.ForEach(vector =>
        {
            float randomRotation = rotations[Random.Range(0, rotations.Length)];
            Instantiate(obstacle, transform)
            .transform
            .SetPosition(new Vector2(vector.x * distance, vector.y))
            .SetRotation(Quaternion.Euler(0f, 0f, randomRotation));
        });
    }

    private List<int> GetRandomCoordinates()
    {
        List<int> randomCordinatesList = new List<int>();
        for (int i = 0; i < randomCount; i++)
        {
            IEnumerable<int> range = Enumerable.Range(startLevelPoint, endLevelPoint);
            randomCordinatesList.AddRange(range);
        }
        int[] randomCordinates = randomCordinatesList.ToArray();
        Randomizer.Shuffle(randomCordinates);
        return randomCordinates
            .ToList()
            .GetRange(0, randomCordinatesList.Count / randomCount)
            .ToList();
    }

    private List<Vector2> GetRandomVectors2(List<int> randomCordinates)
    {
        return randomCordinates
            .Select(x => new Vector2(x, hights[Random.Range(0, hights.Length)]))
            .ToList();
    }
}
