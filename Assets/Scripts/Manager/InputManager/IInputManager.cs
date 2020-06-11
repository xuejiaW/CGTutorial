using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputManager { }

public static class IInputManagerExtension
{
    public static void RegisterMessageHandle<T, U>(this IInputManager manager, Dictionary<U, T> handlesDict, Dictionary<U, int> trackedTargets, T handle, params U[] target) where T : Delegate
    {
        Array.ForEach(target, (layer) =>
         {
             if (trackedTargets.ContainsKey(layer))
                 ++trackedTargets[layer];
             else
                 trackedTargets.Add(layer, 1);

             if (handlesDict.TryGetValue(layer, out T handleLists))
             {
                 handlesDict[layer] = (T)Delegate.Combine(handleLists, handle);
             }
             else
             {
                 handlesDict.Add(layer, handle);
             }
         });
    }

    public static void UnRegisterMessageHandle<T, U>(this IInputManager manager, Dictionary<U, T> handlesDict, Dictionary<U, int> trackedTargets, T handle, params U[] target) where T : Delegate
    {
        Array.ForEach(target, (layer) =>
         {
             if (trackedTargets.ContainsKey(layer) && --trackedTargets[layer] <= 0)
                 trackedTargets.Remove(layer);

             if (handlesDict.TryGetValue(layer, out T handleLists))
             {
                 handlesDict[layer] = (T)Delegate.Remove(handleLists, handle);
                 if (handlesDict[layer] == null)
                     handlesDict.Remove(layer);
             }
         });
    }
}
