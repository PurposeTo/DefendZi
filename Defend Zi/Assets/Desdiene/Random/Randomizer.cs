using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Desdiene.Random
{
    public static class Randomizer
    {
        /// <summary>
        /// Перемешивает массив элементов 
        /// </summary>
        public static void Shuffle<T>(T[] deck)
        {
            for (int i = 0; i < deck.Length; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, deck.Length);

                Common.Swap(ref deck[i], ref deck[randomIndex]);
            }
        }

        /// <summary>
        /// Получить случайные элементы массива
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="deck">Массив элементов для выбора. Не может быть пустым</param>
        /// <returns></returns>
        public static T[] GetRandomItems<T>(T[] deck)
        {
            if (deck == null || deck.Length == 0) throw new System.Exception("Deck can't being empty!");

            List<T> listOfReturnedItems = new List<T>();
            Shuffle(deck);

            // Минимальное включительное значение - 1, а максимально включительное значение - deck.Length
            int numberOfRandomIntems = UnityEngine.Random.Range(1, deck.Length + 1);

            for (int i = 0; i < numberOfRandomIntems; i++) listOfReturnedItems.Add(deck[i]);

            return listOfReturnedItems.ToArray();
        }

        /// <summary>
        /// Получить случайный элемент из массива
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="deck"></param>
        /// <returns></returns>
        public static T GetRandomItem<T>(T[] deck) => deck[UnityEngine.Random.Range(0, deck.Length)];

        public static T GetRandomEnumItem<T>() where T : System.Enum
        {
            return GetRandomItem(Common.GetAllEnumValues<T>());
        }

        public static T GetRandomItem<T>(ISelectableItem<T>[] selectableItems)
        {
            int total = (int)selectableItems.Sum(selectableItem => selectableItem.ChanceMass);
            int randomChoice = UnityEngine.Random.Range(0, total + 1); //случайное число. Границы - включительно.

            int currentCheck = 0; //вычисление текущего шанса выпадения объекта для проверки
            for (int i = 0; i < selectableItems.Length; i++)
            {
                currentCheck += (int)selectableItems[i].ChanceMass;

                if (currentCheck == 0) continue;

                if (randomChoice <= currentCheck) //проверяем, это текущий элемент?
                {
                    Debug.Log($"Choosed random item \"{selectableItems[i].Name}\" with index [{i}]");
                    return selectableItems[i].Item;
                }
            }

            throw new System.IndexOutOfRangeException($"Random item was not choosing! RandomChoice = {randomChoice}");
        }
    }
}
