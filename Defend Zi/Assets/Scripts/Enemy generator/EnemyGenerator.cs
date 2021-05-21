using System.Collections;
using UnityEngine;
using Desdiene.Extensions.UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    private float ZiRadius => GameObjectsHolder.Instance.ZiPresenter.Zi.Radius;

    private float MinSpawnArea => ZiRadius + 40f;
    private float MaxSpawnArea => ZiRadius + 60f;

    [SerializeField]
    private float cooldown = 5;

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
