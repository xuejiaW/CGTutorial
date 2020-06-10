using System.Collections.Generic;
using System;
using UnityEngine;

public partial class MouseInputManager : Singleton<MouseInputManager>, IMainUpdateObserver, IInputManager
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

    public override void Init()
    {
        base.Init();
        MainManager.Instance.RegisterObserver(this);

        leftButtonClickDownHandlesDict = new Dictionary<int, Action<GameObject>>();
        rightButtonClickDownHandlesDict = new Dictionary<int, Action<GameObject>>();
        leftButtonClickUpHandlesDict = new Dictionary<int, Action<GameObject>>();
        rightButtonClickUpHandlesDict = new Dictionary<int, Action<GameObject>>();
        leftButtonDragHandlesDict = new Dictionary<int, Action<Vector3>>();
        rightButtonDragHandlesDict = new Dictionary<int, Action<Vector3>>();
        leftTrackedLayers = new Dictionary<int, int>();
        rightTrackedLayer = new Dictionary<int, int>();
    }

    public void RegisterClickDownMessageHandle(int button, Action<GameObject> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.RegisterMessageHandle(leftButtonClickDownHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.RegisterMessageHandle(rightButtonClickDownHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void RegisterClickUpMessageHandle(int button, Action<GameObject> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.RegisterMessageHandle(leftButtonClickUpHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.RegisterMessageHandle(rightButtonClickUpHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void RegisterDragMessageHandle(int button, Action<Vector3> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.RegisterMessageHandle(leftButtonDragHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.RegisterMessageHandle(rightButtonDragHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void UnRegisterClickDownMessageHandle(int button, Action<GameObject> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.UnRegisterMessageHandle(leftButtonClickDownHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.UnRegisterMessageHandle(rightButtonClickDownHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void UnRegisterClickUpMessageHandle(int button, Action<GameObject> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.UnRegisterMessageHandle(leftButtonClickUpHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.UnRegisterMessageHandle(rightButtonClickUpHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void UnRegisterDragMessageHandle(int button, Action<Vector3> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.UnRegisterMessageHandle(leftButtonDragHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.UnRegisterMessageHandle(rightButtonDragHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }
}
