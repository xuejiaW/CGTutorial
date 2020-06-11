using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveIndicator : MonoBehaviour
{
    private Transform thisTransform = null;
    private GameObject thisGameObject = null;
    public GameObject attachedGameObject = null;
    private IndicatorHandleBase indicatorHandle = null;

    //Awake will be called after gameobject is instantiated while Start will not
    public void Awake()
    {
        thisTransform = transform;
        thisGameObject = gameObject;
        thisGameObject.SetActive(false);
    }

    public InteractiveIndicator SetHandle(IndicatorHandleBase handle)
    {
        indicatorHandle = handle;
        indicatorHandle.SetIndicator(this);
        return this;
    }

    public void AddChild(InteractiveGameObject interactiveGO)
    {

        // Make thisTransform have the same transform with the interactiveGO
        thisTransform.SetParent(interactiveGO?.transform);
        thisTransform.localRotation = Quaternion.identity;
        thisTransform.localPosition = Vector3.zero;
        thisTransform.localScale = interactiveGO == null ? Vector3.one : interactiveGO.transform.localScale.GetInverse();

        thisTransform.SetParent(interactiveGO?.transform?.parent);
        thisGameObject.SetActive(interactiveGO != null);
        attachedGameObject = interactiveGO?.gameObject;

        interactiveGO?.transform?.SetParent(thisTransform);
    }

    public void RemoveChild(InteractiveGameObject interactiveGO)
    {
        if (interactiveGO == null) return;

        interactiveGO.transform.SetParent(thisTransform.parent);
    }

    public void ClickIndicatorAxis(string axisGame)
    {
        indicatorHandle.SetIndicatorAxis(axisGame);
    }

    public void DragDeltaIndicatorAxis(Vector3 deltaPos)
    {
        indicatorHandle.DragDeltaIndicatorAxis(deltaPos);
    }
}
