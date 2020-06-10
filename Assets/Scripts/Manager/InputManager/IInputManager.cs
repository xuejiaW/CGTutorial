using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputManager { }

public static class IInputManagerExtension
{
    public static void RegisterMessageHandle<T, U>(this IInputManager manager, Dictionary<U, Action<T>> handlesDict, Dictionary<U, int> trackedTargets, Action<T> handle, params U[] target)
    {
        Array.ForEach(target, (layer) =>
         {
             if (trackedTargets.ContainsKey(layer))
                 ++trackedTargets[layer];
             else
                 trackedTargets.Add(layer, 1);

             if (handlesDict.TryGetValue(layer, out Action<T> handleLists))
             {
                 // Do not add duplicate handles
                 if (Array.IndexOf(handleLists.GetInvocationList(), handle) == -1)
                     handlesDict[layer] += handle;
             }
             else
             {
                 handlesDict.Add(layer, handle);
             }
         });
    }

    public static void UnRegisterMessageHandle<T, U>(this IInputManager manager, Dictionary<U, Action<T>> handlesDict, Dictionary<U, int> trackedTargets, Action<T> handle, params U[] target)
    {
        Array.ForEach(target, (layer) =>
         {
             if (trackedTargets.ContainsKey(layer) && --trackedTargets[layer] <= 0)
                 trackedTargets.Remove(layer);

             if (handlesDict.ContainsKey(layer))
             {
                 handlesDict[layer] -= handle;
                 if (handlesDict[layer] == null)
                     handlesDict.Remove(layer);
             }
         });
    }
}
