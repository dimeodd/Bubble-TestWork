using UnityEngine;

public static class Color32Equal
{
    public static bool CustomEquel(this Color32 a, Color32 b)
    {
        return a.r == b.r &
                a.g == b.g &
                a.b == b.b;
    }
}