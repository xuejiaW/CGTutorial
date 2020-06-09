using System.Collections.Generic;
using System;
using UnityEngine;

public partial class MouseInputManager : Singleton<MouseInputManager>, IMainUpdateObserver
{
    private List<int> leftTrackedLayers = new List<int>();
    private List<int> rightTrackedLayer = new List<int>();

    private Vector3 leftLastPos = Vector3.zero;
    private Vector3 rightLastPos = Vector3.zero;

    private int leftHittedLayer = -1;
    private int rightHittedLayer = -1;
    private GameObject leftHittedGO = null;
    private GameObject rightHittedGO = null;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnButtonClickDown(leftTrackedLayers, leftButtonClickDownHandlesDict, ref leftLastPos, ref leftHittedLayer, ref leftHittedGO);
        else if (Input.GetMouseButtonDown(1))
            OnButtonClickDown(rightTrackedLayer, rightButtonClickDownHandlesDict, ref rightLastPos, ref rightHittedLayer, ref rightHittedGO);

        if (Input.GetMouseButtonUp(0))
            OnButtonClickUp(leftButtonClickUpHandlesDict, ref leftHittedLayer, ref leftHittedGO);
        else if (Input.GetMouseButtonUp(1))
            OnButtonClickUp(rightButtonClickUpHandlesDict, ref rightHittedLayer, ref rightHittedGO);

        if (Input.GetMouseButton(0))
            OnButtonDrag(leftButtonDragHandlesDict, ref leftLastPos, ref leftHittedLayer);
        else if (Input.GetMouseButton(1))
            OnButtonDrag(rightButtonDragHandlesDict, ref rightLastPos, ref rightHittedLayer);
    }

    private void OnButtonClickDown(List<int> trackedLayers, Dictionary<int, Action<GameObject>> clickDownHandlesDict,
                                ref Vector3 lastPos, ref int hittedLayer, ref GameObject hittedGO)
    {
        lastPos = Input.mousePosition;

        hittedGO = null;
        int allLayersMask = 0;
        trackedLayers.ForEach(layer => allLayersMask += (layer != -1 ? layer : 0));

        if (Physics.Raycast(MainManager.Instance.viewCamera.ScreenPointToRay(Input.mousePosition),
                            out RaycastHit hit, 100, allLayersMask))
            hittedGO = hit.transform.gameObject;

        hittedLayer = hittedGO == null ? -1 : 1 << hittedGO.layer;

        if (clickDownHandlesDict.TryGetValue(hittedLayer, out Action<GameObject> handles))
            handles.Invoke(hittedGO);
    }

    private void OnButtonClickUp(Dictionary<int, Action<GameObject>> clickUpHandlesDict, ref int hittedLayer, ref GameObject hittedGO)
    {

        if (clickUpHandlesDict.TryGetValue(hittedLayer, out Action<GameObject> handles))
            handles.Invoke(hittedGO);
        hittedLayer = -1;
    }

    private void OnButtonDrag(Dictionary<int, Action<Vector3>> dragHandlesDict, ref Vector3 lastPos, ref int hittedLayer)
    {
        if (dragHandlesDict.TryGetValue(hittedLayer, out Action<Vector3> handles))
            handles.Invoke(Input.mousePosition - lastPos);

        lastPos = Input.mousePosition;
    }
}