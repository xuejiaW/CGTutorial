using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputManager { }

public static class IInputManagerExtension
{
    public static void RegisterMessageHandle<U>(this IInputManager manager, Dictionary<U, Action> handlesDict, List<U> trackedTargets, Action handle, params U[] target)
    {
        Array.ForEach(target, (layer) =>
         {
             trackedTargets.DistinctAdd(layer);

             if (handlesDict.TryGetValue(layer, out Action handleLists))
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

    public static void RegisterMessageHandle<T, U>(this IInputManager manager, Dictionary<U, Action<T>> handlesDict, List<U> trackedTargets, Action<T> handle, params U[] target)
    {
        Array.ForEach(target, (layer) =>
         {
             trackedTargets.DistinctAdd(layer);

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

    public static void UnRegisterMessageHandle<U>(this IInputManager manager, Dictionary<U, Action> handlesDict, List<U> trackedTargets, Action handle, params U[] target)
    {
        Array.ForEach(target, (layer) =>
         {
             trackedTargets.Remove(layer);
             if (handlesDict.TryGetValue(layer, out Action handleLists))
             {
                 handleLists -= handle;
             }
         });
    }

    public static void UnRegisterMessageHandle<T, U>(this IInputManager manager, Dictionary<U, Action<T>> handlesDict, List<U> trackedTargets, Action<T> handle, params U[] target)
    {
        Array.ForEach(target, (layer) =>
         {
             trackedTargets.Remove(layer);
             if (handlesDict.TryGetValue(layer, out Action<T> handleLists))
             {
                 handleLists -= handle;
             }
         });
    }
}
