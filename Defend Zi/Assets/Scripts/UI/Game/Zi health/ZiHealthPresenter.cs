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
            health.OnValueChanged += UpdateValueView;
        };
    }


    private void OnDestroy()
    {
        health.OnValueChanged -= UpdateValueView;
    }


    private void UpdateValueView()
    {
        healthView.ShowHealth(health.Value);
    }
}
