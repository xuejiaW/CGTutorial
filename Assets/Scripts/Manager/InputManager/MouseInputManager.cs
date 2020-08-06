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

    private Dictionary<int, Action<Vector3>> leftButtonDragDeltaHandlesDict = null;
    private Dictionary<int, Action<Vector3>> rightButtonDragDeltaHandlesDict = null;
    private Dictionary<int, Action<Vector3, Vector3>> leftButtonDragPosHandlesDict = null;
    private Dictionary<int, Action<Vector3, Vector3>> rightButtonDragPosHandlesDict = null;

    public override void Init()
    {
        base.Init();
        MainManager.Instance.RegisterObserver(this);

        leftButtonClickDownHandlesDict = new Dictionary<int, Action<GameObject>>();
        rightButtonClickDownHandlesDict = new Dictionary<int, Action<GameObject>>();
        leftButtonClickUpHandlesDict = new Dictionary<int, Action<GameObject>>();
        rightButtonClickUpHandlesDict = new Dictionary<int, Action<GameObject>>();

        leftButtonDragDeltaHandlesDict = new Dictionary<int, Action<Vector3>>();
        rightButtonDragDeltaHandlesDict = new Dictionary<int, Action<Vector3>>();

        leftButtonDragPosHandlesDict = new Dictionary<int, Action<Vector3, Vector3>>();
        rightButtonDragPosHandlesDict = new Dictionary<int, Action<Vector3, Vector3>>();

        leftTrackedLayers = new Dictionary<int, int>();
        rightTrackedLayer = new Dictionary<int, int>();
    }


    #region Register Handle
    public void RegisterClickDownMessageHandle(int button, Action<GameObject> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.RegisterMessageHandle(leftButtonClickDownHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.RegisterMessageHandle(rightButtonClickDownHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void RegisterDragMessageHandle(int button, Action<Vector3> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.RegisterMessageHandle(leftButtonDragDeltaHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.RegisterMessageHandle(rightButtonDragDeltaHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void RegisterDragMessageHandle(int button, Action<Vector3, Vector3> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.RegisterMessageHandle(leftButtonDragPosHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.RegisterMessageHandle(rightButtonDragPosHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void RegisterClickUpMessageHandle(int button, Action<GameObject> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.RegisterMessageHandle(leftButtonClickUpHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.RegisterMessageHandle(rightButtonClickUpHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    #endregion

    #region UnRegister Handle
    public void UnRegisterClickDownMessageHandle(int button, Action<GameObject> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.UnRegisterMessageHandle(leftButtonClickDownHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.UnRegisterMessageHandle(rightButtonClickDownHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void UnRegisterDragMessageHandle(int button, Action<Vector3> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.UnRegisterMessageHandle(leftButtonDragDeltaHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.UnRegisterMessageHandle(rightButtonDragDeltaHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void UnRegisterDragMessageHandle(int button, Action<Vector3, Vector3> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.UnRegisterMessageHandle(leftButtonDragPosHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.UnRegisterMessageHandle(rightButtonDragPosHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }

    public void UnRegisterClickUpMessageHandle(int button, Action<GameObject> handle, params int[] targetLayerLists)
    {
        if (button == 0)
            this.UnRegisterMessageHandle(leftButtonClickUpHandlesDict, leftTrackedLayers, handle, targetLayerLists);
        else if (button == 1)
            this.UnRegisterMessageHandle(rightButtonClickUpHandlesDict, rightTrackedLayer, handle, targetLayerLists);
    }
    #endregion
}
