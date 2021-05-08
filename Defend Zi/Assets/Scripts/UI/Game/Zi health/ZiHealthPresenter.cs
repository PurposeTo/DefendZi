using UnityEngine;

[RequireComponent(typeof(ZiHealthView))]
public class ZiHealthPresenter : MonoBehaviour
{
    private ZiHealthView healthView;
    private IStat<int> health;

    private void Awake()
    {
        healthView = GetComponent<ZiHealthView>();
        GameObjectsHolder.InitializedInstance += (instance) =>
        {
            health = GameObjectsHolder.Instance.ZiPresenter.Health.GetHealth();
            UpdateValueView();
            health.OnStatChange += UpdateValueView;
        };
    }


    private void OnDestroy()
    {
        health.OnStatChange -= UpdateValueView;
    }


    private void UpdateValueView()
    {
        healthView.SetHealth(health.Value);
    }
}
