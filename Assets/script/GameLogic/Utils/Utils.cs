using System;

namespace GoFire
{
    public static class Utils
    {
        public static void ArrayAppend<T>(ref T[] array, T value)
        {
            Array.Resize(ref array, array.Length + 1);
            array[^1] = value;
        }
    }
}