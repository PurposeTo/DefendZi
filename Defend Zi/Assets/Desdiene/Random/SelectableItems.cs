using Desdiene.Types.Percents;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Desdiene.Random
{
    public class SelectableItems<T> : ISelectableItems<T>
    {
        private readonly ISelectableItem<T>[] _selectableItems;

        public SelectableItems(ISelectableItem<T>[] selectableItems)
        {
            _selectableItems = selectableItems ?? throw new System.ArgumentNullException(nameof(selectableItems));
        }

        IPercentAccessor ISelectableItems<T>.GetChance(ISelectableItem<T> item)
        {
            uint totalMass = 0;
            foreach (ISelectableItem<T> itemInList in _selectableItems)
            {
                totalMass += itemInList.ChanceMass;
            }
            return new Percent(item.ChanceMass / totalMass);
        }

        T ISelectableItems<T>.GetRandom()
        {
            int total = (int)_selectableItems.Sum(selectableItem => selectableItem.ChanceMass);
            int randomChoice = UnityEngine.Random.Range(0, total + 1); //случайное число. Границы - включительно.

            int currentCheck = 0; //вычисление текущего шанса выпадения объекта для проверки
            for (int i = 0; i < _selectableItems.Length; i++)
            {
                currentCheck += (int)_selectableItems[i].ChanceMass;

                if (currentCheck == 0) continue;

                if (randomChoice <= currentCheck) //проверяем, это текущий элемент?
                {
                    Debug.Log($"Chose random item \"{_selectableItems[i].Name}\" with index [{i}]");
                    return _selectableItems[i].Item;
                }
            }

            throw new System.IndexOutOfRangeException($"Random item was not choosing! RandomChoice = {randomChoice}");
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            IEnumerable<T> items = _selectableItems.Select(it => it.Item);
            foreach (T item in items)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _selectableItems.GetEnumerator();
        }

    }
}
