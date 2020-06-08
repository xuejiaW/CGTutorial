using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemCollectionsExtension
{
    public static void DistinctAdd<T>(this IList<T> target, T data)
    {
        if (target.Contains(data))
            return;
        target.Add(data);
    }
    public static void LoopWork<T>(this IList<T> target, System.Action<int> work)
    {
        if (target == null || work == null)
            return;
        for (int i = 0; i != target.Count; ++i)
            work(i);
    }
}
