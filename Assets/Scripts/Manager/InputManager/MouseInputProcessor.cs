using System.Collections.Generic;
using System;
using UnityEngine;


public partial class MouseInputManager : Singleton<MouseInputManager>, IMainUpdateObserver
{
    private Camera viewCamera = null;
    private Vector3 leftLastPos = Vector3.zero;
    private Vector3 rightLastPos = Vector3.zero;
    private int hittedLayer = -1;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftLastPos = Input.mousePosition;

            GameObject hitGO = null;
            int allLayersMask = 0;
            trackedLayer.ForEach(layer => allLayersMask += layer != -1 ? layer : 0);

            if (Physics.Raycast(viewCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, allLayersMask))
                hitGO = hit.transform.gameObject;

            hittedLayer = hitGO == null ? -1 : 1 << hitGO.layer;
            Debug.Log("hittedLayer is " + hittedLayer);

            if (leftButtonClickHandleDict.TryGetValue(hittedLayer, out Action<GameObject> handles))
                handles.Invoke(hitGO);
        }

        if (Input.GetMouseButtonUp(0))
        {
            hittedLayer = -1;
        }

        if (Input.GetMouseButton(0))
        {
            if (leftButtonDragHandleDict.TryGetValue(hittedLayer, out Action<Vector3> handles))
                handles.Invoke(Input.mousePosition - leftLastPos);

            leftLastPos = Input.mousePosition;
        }
    }
}