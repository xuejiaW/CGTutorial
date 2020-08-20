using System.Collections.Generic;
using System;
using UnityEngine;

public partial class MouseInputManager : Singleton<MouseInputManager>, IMainUpdateObserver
{
    private Dictionary<int, int> leftTrackedLayers = null;
    private Dictionary<int, int> rightTrackedLayer = null;

    private Vector3 leftLastPos = Vector3.zero;
    private Vector3 rightLastPos = Vector3.zero;

    private Vector3 leftClickPos = Vector3.zero;
    private Vector3 rightClickPos = Vector3.zero;

    private int leftHittedLayer = -1;
    private int rightHittedLayer = -1;
    private GameObject leftHittedGO = null;
    private GameObject rightHittedGO = null;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnButtonClickDown(leftTrackedLayers, leftButtonClickDownHandlesDict, ref leftLastPos, ref leftClickPos, ref leftHittedLayer, ref leftHittedGO);
        else if (Input.GetMouseButtonDown(1))
            OnButtonClickDown(rightTrackedLayer, rightButtonClickDownHandlesDict, ref rightLastPos, ref rightClickPos, ref rightHittedLayer, ref rightHittedGO);

        if (Input.GetMouseButtonUp(0))
            OnButtonClickUp(leftButtonClickUpHandlesDict, ref leftHittedLayer, ref leftHittedGO);
        else if (Input.GetMouseButtonUp(1))
            OnButtonClickUp(rightButtonClickUpHandlesDict, ref rightHittedLayer, ref rightHittedGO);

        if (Input.GetMouseButton(0))
            OnButtonDrag(leftButtonDragDeltaHandlesDict, leftButtonDragPosHandlesDict, ref leftLastPos, ref leftClickPos, ref leftHittedLayer);
        else if (Input.GetMouseButton(1))
            OnButtonDrag(rightButtonDragDeltaHandlesDict, rightButtonDragPosHandlesDict, ref rightLastPos, ref rightClickPos, ref rightHittedLayer);
    }

    private void OnButtonClickDown(Dictionary<int, int> trackedLayers, Dictionary<int, Action<GameObject>> clickDownHandlesDict,
                                ref Vector3 lastPos, ref Vector3 clickPos, ref int hittedLayer, ref GameObject hittedGO)
    {
        Vector3 mousePos = Input.mousePosition;

        if (!MainManager.Instance.worldCamera.pixelRect.Contains(mousePos)) // Only handle view Camera rect
            return;

        lastPos = mousePos;
        clickPos = mousePos;

        hittedGO = null;
        int allLayersMask = 0;

        foreach (KeyValuePair<int, int> pair in trackedLayers)
            allLayersMask += (pair.Key != -1 ? pair.Key : 0);

        if (Physics.Raycast(MainManager.Instance.worldCamera.ScreenPointToRay(mousePos),
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

    private void OnButtonDrag(Dictionary<int, Action<Vector3>> dragDeltaHandlesDict, Dictionary<int, Action<Vector3, Vector3>> dragPosHandlesDict,
                            ref Vector3 lastPos, ref Vector3 clickPos, ref int hittedLayer)
    {
        Vector3 mousePos = Input.mousePosition;
        if (!MainManager.Instance.worldCamera.pixelRect.Contains(mousePos)) // Only handle view Camera rect
            return;

        if (dragDeltaHandlesDict.TryGetValue(hittedLayer, out Action<Vector3> deltaHandles))
            deltaHandles.Invoke(mousePos - lastPos);

        if (dragPosHandlesDict.TryGetValue(hittedLayer, out Action<Vector3, Vector3> posHandles))
            posHandles.Invoke(clickPos, mousePos);

        lastPos = mousePos;
    }
}