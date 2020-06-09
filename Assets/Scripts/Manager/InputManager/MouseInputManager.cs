using System.Collections.Generic;
using System;
using UnityEngine;
public partial class MouseInputManager : Singleton<MouseInputManager>, IMainUpdateObserver
{
    //Dictionary<layer,Handle>: 
    //layer-> -1 indicates empty 
    private Dictionary<int, Action<GameObject>> leftButtonClickHandleDict = null;
    private Dictionary<int, Action<GameObject>> rightButtonClickHandleDict = null;
    private Dictionary<int, Action<Vector3>> leftButtonDragHandleDict = null;
    private Dictionary<int, Action<Vector3>> rightButtonDragHandleDict = null;
    private List<int> trackedLayer = new List<int>();


    protected override void Init()
    {
        base.Init();
        MainManager.Instance.RegisterObserver(this);

        leftButtonClickHandleDict = new Dictionary<int, Action<GameObject>>();
        rightButtonClickHandleDict = new Dictionary<int, Action<GameObject>>();
        leftButtonDragHandleDict = new Dictionary<int, Action<Vector3>>();
        rightButtonDragHandleDict = new Dictionary<int, Action<Vector3>>();
        trackedLayer = new List<int>();
    }

    public void RegisterLeftClickMessageHandle(Action<GameObject> handle, params int[] targetLayerLists)
    {
        RegisterMessageHandle<GameObject>(leftButtonClickHandleDict, handle, targetLayerLists);
    }

    public void RegisterLeftDragMessageHandle(Action<Vector3> handle, params int[] targetLayerLists)
    {
        RegisterMessageHandle<Vector3>(leftButtonDragHandleDict, handle, targetLayerLists);
    }

    public void UnRegisterLeftClickMessageHandle(Action<GameObject> handle, params int[] targetLayerLists)
    {
        UnRegisterMessageHandle<GameObject>(leftButtonClickHandleDict, handle, targetLayerLists);
    }

    public void UnRegisterLeftDragMessageHandle(Action<Vector3> handle, params int[] targetLayerLists)
    {
        UnRegisterMessageHandle<Vector3>(leftButtonDragHandleDict, handle, targetLayerLists);
    }

    private void RegisterMessageHandle<T>(Dictionary<int, Action<T>> handleDict, Action<T> handle, params int[] layerLists)
    {
        Array.ForEach(layerLists, (layer) =>
         {
             trackedLayer.DistinctAdd(layer);

             if (handleDict.TryGetValue(layer, out Action<T> handleLists))
             {
                 // Do not add duplicate handles
                 if (Array.IndexOf(handleLists.GetInvocationList(), handle) == -1)
                     handleDict[layer] += handle;
             }
             else
             {
                 handleDict.Add(layer, handle);
             }
         });
    }

    private void UnRegisterMessageHandle<T>(Dictionary<int, Action<T>> handleDict, Action<T> handle, params int[] targetLayerLists)
    {
        Array.ForEach(targetLayerLists, (layer) =>
         {
             trackedLayer.Remove(layer);
             if (handleDict.TryGetValue(layer, out Action<T> handleLists))
             {
                 handleLists -= handle;
             }
         });
    }
}
