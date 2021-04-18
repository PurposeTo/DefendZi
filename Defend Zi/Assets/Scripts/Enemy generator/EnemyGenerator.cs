using System.Collections;
using UnityEngine;
using Desdiene.UnityEngineExtension;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    private float ZiRadius => GameObjectsHolder.Instance.Zi.Radius;

    private float MinSpawnArea => ZiRadius + 30f;
    private float MaxSpawnArea => ZiRadius + 40f;

    [SerializeField]
    private float cooldown;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(cooldown);
            Instantiate(enemy).transform
                .SetPosition(GetRandomRoundVector() * Random.Range(MinSpawnArea, MaxSpawnArea))
                .SetParent(transform);
        }
    }

    private Vector2 GetRandomRoundVector()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        return rotation * Vector3.up;
    }
}
