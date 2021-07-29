using UnityEngine;

public class TwoNonAdjacentNumbers
{
    private float _first;
    private float _second;
    private int _firstIndex;

    public void Get(float[] sourceArray, out float first, out float second)
    {
        InitFirst(sourceArray, out bool isLeftNeighborExists, out bool isRightNeighborExists);
        InitSecond(sourceArray, isLeftNeighborExists, isRightNeighborExists);
        first = _first;
        second = _second;
    }

    private void InitFirst(float[] sourceArray, out bool isLeftNeighborExists, out bool isRightNeighborExists)
    {
        _firstIndex = Random.Range(0, sourceArray.Length);
        _first = sourceArray[_firstIndex];

        isLeftNeighborExists = _firstIndex > 0;
        isRightNeighborExists = _firstIndex < sourceArray.Length - 1;
    }

    private void InitSecond(float[] sourceArray, bool isLeftNeighborExists, bool isRightNeighborExists)
    {
        float[] nonAdjacentToFirst = GetNonAdjacentToFirstNumbers(sourceArray, isLeftNeighborExists, isRightNeighborExists);
        _second = nonAdjacentToFirst[Random.Range(0, nonAdjacentToFirst.Length)];
    }

    private float[] GetNonAdjacentToFirstNumbers(float[] sourceArray, bool isLeftNeighborExists, bool isRightNeighborExists)
    {
        int counter = 0;
        float[] nonAdjacentToFirst;
        System.Predicate<int> predicate = new System.Predicate<int>((i) =>
        {
            return (i == _firstIndex) || (isLeftNeighborExists && i == _firstIndex - 1) || (isRightNeighborExists && i == _firstIndex + 1);
        });

        if (isLeftNeighborExists && isRightNeighborExists)
        {
            nonAdjacentToFirst = new float[sourceArray.Length - 3];
        }
        else nonAdjacentToFirst = new float[sourceArray.Length - 2];

        for (int i = 0; i < sourceArray.Length; i++)
        {
            if (predicate(i)) continue;
            nonAdjacentToFirst[counter] = sourceArray[i];
            counter++;
        }

        return nonAdjacentToFirst;
    }
}
