namespace AmongUsBot
{
    internal static class Mathf
    {
        public static int Clamp(int value, int min, int max)
        {
            if (value > max) value = max;
            if (value < min) value = min;
            return value;
        }
    }
}
