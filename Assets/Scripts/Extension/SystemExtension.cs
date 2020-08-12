using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemExtension
{
    public static int IndexNotOf(this string text, char target)
    {
        for (int i = 0; i != text.Length; ++i)
        {
            if (text[i] != target)
                return i;
        }
        return text.Length;
    }
}
