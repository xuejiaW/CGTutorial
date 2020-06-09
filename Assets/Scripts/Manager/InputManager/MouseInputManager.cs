using System.Collections.Generic;
using System;
using UnityEngine;
public partial class MouseInputManager : Singleton<MouseInputManager>, IMainUpdateObserver
{
    //Dictionary<layer,Handle>: 
    //layer should be obtained through api Layer.GetMask or through manually calculation like " 1<<9 " for layer9
    //-1 layer indicate for empty GameObject
    private Dictionary<int, Action<GameObject>> leftButtonClickDownHandlesDict = null;
    private Dictionary<int, Action<GameObject>> rightButtonClickDownHandlesDict = null;
    private Dictionary<int, Action<GameObject>> leftButtonClickUpHandlesDict = null;
    private Dictionary<int, Action<GameObject>> rightButtonClickUpHandlesDict = null;
    private Dictionary<int, Action<Vector3>> leftButtonDragHandlesDict = null;
    private Dictionary<int, Action<Vector3>> rightButtonDragHandlesDict = null;

    protected override void Init()
    {
        base.Init();
        MainManager.Instance.RegisterObserver(this);

        leftButtonClickDownHandlesDict = new Dictionary<int, Action<GameObject>>();
        rightButtonClickDownHandlesDict = new Dictionary<int, Action<GameObject>>();
        leftButtonClickUpHandlesDict = new Dictionary<int, Action<GameObject>>();
        rightButtonClickUpHandlesDict = new Dictionary<int, Action<GameObject>>();
        leftButtonDragHandlesDict = new Dictionary<int, Action<Vector3>>();
        rightButtonDragHandlesDict = new Dictionary<int, Action<Vector3>>();
        leftTrackedLayers = new List<int>();
        rightTrackedLayer = new List<int>();
    }

    public void RegisterClickDownMessageHandle(int button, Action<GameObject> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            RegisterMessageHandle(leftButtonClickDownHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            RegisterMessageHandle(rightButtonClickDownHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void RegisterClickUpMessageHandle(int button, Action<GameObject> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            RegisterMessageHandle(leftButtonClickUpHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            RegisterMessageHandle(rightButtonClickUpHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void RegisterDragMessageHandle(int button, Action<Vector3> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            RegisterMessageHandle(leftButtonDragHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            RegisterMessageHandle(rightButtonDragHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void UnRegisterClickDownMessageHandle(int button, Action<GameObject> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            UnRegisterMessageHandle(leftButtonClickDownHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            UnRegisterMessageHandle(rightButtonClickDownHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void UnRegisterClickUpMessageHandle(int button, Action<GameObject> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            UnRegisterMessageHandle(leftButtonClickUpHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            UnRegisterMessageHandle(rightButtonClickUpHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void UnRegisterDragMessageHandle(int button, Action<Vector3> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            UnRegisterMessageHandle(leftButtonDragHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            UnRegisterMessageHandle(rightButtonDragHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    private void RegisterMessageHandle<T>(Dictionary<int, Action<T>> handleDict, List<int> trackedLayers, Action<T> handle, params int[] layerLists)
    {
        Array.ForEach(layerLists, (layer) =>
         {
             trackedLayers.DistinctAdd(layer);

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

    private void UnRegisterMessageHandle<T>(Dictionary<int, Action<T>> handleDict, List<int> trackedLayers, Action<T> handle, params int[] targetLayerLists)
    {
        Array.ForEach(targetLayerLists, (layer) =>
         {
             trackedLayers.Remove(layer);
             if (handleDict.TryGetValue(layer, out Action<T> handleLists))
             {
                 handleLists -= handle;
             }
         });
    }
}
