using UnityEngine;

public class Zi : MonoBehaviour
{
    public float Radius { get; private set; }

    private void Awake()
    {
        Radius = transform.localScale.x / 2;
    }


    #region ������ �������������� � ������� �������� "Zi"
    public Vector2 GetToZiVector(Vector2 position)
    {
        return (Vector2)gameObject.transform.position - position;
    }


    public Vector2 GetToZiDirection(Vector2 position)
    {
        return GetToZiVector(position).normalized;
    }


    public float GetToZiMagnitude(Vector2 position)
    {
        return GetToZiVector(position).magnitude;
    }


    public Vector2 GetFromZiVector(Vector2 position)
    {
        return GetToZiVector(position) * -1f;
    }


    public Vector2 GetFromZiDirection(Vector2 position)
    {
        return GetToZiDirection(position) * -1f;
    }


    public float GetFromZiMagnitude(Vector2 position)
    {
        return GetToZiMagnitude(position) * -1f;
    }
    #endregion
}
