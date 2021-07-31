using System;

namespace Desdiene
{
    // класс содержит в себе методы, которые не удалость разбить на классы.
    // все методы из данного класса без исключения подлежат занесению в подходящие классы.
    public class Common
    {
        public static void Swap<T>(ref T x, ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }

        public static T[] GetAllEnumValues<T>() where T : System.Enum
        {
            return (T[])Enum.GetValues(typeof(T));
        }
    }
}
