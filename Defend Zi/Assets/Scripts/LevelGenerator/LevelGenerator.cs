using System.Collections.Generic;
using System.Linq;
using Desdiene;
using Desdiene.Extensions.UnityEngine;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private int distance = 5;
    [SerializeField] private int startLevelPoint = 6;
    [SerializeField] private int endLevelPoint = 1000;
    [SerializeField] private int randomCount = 2;

    private readonly float[] hight = { -7, -4.5f, -3.75f, -3, -2, -1, 0, 1, 2, 3, 3.75f, 4.5f, 7 };

    private void Awake()
    {
        Math.Compare(ref startLevelPoint, ref endLevelPoint);
        List<int> randomCordinates = GetRandomCoordinates();
        List<Vector2> randomVectors = GetRandomVectors2(randomCordinates);
        SpawnObstacles(randomVectors);
    }

    private void SpawnObstacles(List<Vector2> vectors)
    {
        vectors.ForEach(vector =>
        {
            Instantiate(obstacle, transform)
            .transform
            .SetPosition(new Vector2(vector.x * distance, vector.y))
            .SetRotation(Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
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
            .Select(x => new Vector2(x, hight[Random.Range(0, hight.Length)]))
            .ToList();
    }
}
