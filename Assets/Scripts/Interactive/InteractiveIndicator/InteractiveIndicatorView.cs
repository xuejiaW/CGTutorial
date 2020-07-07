using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveIndicatorView : EntityView
{
    public GameObject attachedGameObject = null;
    // private IndicatorHandleBase indicatorHandle = null;

    //Awake will be called after gameobject is instantiated while Start will not
    protected override void Awake()
    {
        base.Awake();
        thisGameObject.SetActive(false);
    }

    // public InteractiveIndicatorView SetHandle(IndicatorHandleBase handle)
    // {
    //     indicatorHandle = handle;
    //     indicatorHandle.SetIndicator(model as InteractiveIndicatorModel);
    //     return this;
    // }

    // public void AddChild(InteractiveGameObjectView interactiveGO)
    // {

    //     // Make thisTransform have the same transform with the interactiveGO
    //     thisTransform.SetParent(interactiveGO?.transform);
    //     thisTransform.localRotation = Quaternion.identity;
    //     thisTransform.localPosition = Vector3.zero;
    //     thisTransform.localScale = interactiveGO == null ? Vector3.one : interactiveGO.transform.localScale.GetInverse();

    //     thisTransform.SetParent(interactiveGO?.transform?.parent);
    //     thisGameObject.SetActive(interactiveGO != null);
    //     attachedGameObject = interactiveGO?.gameObject;

    //     interactiveGO?.transform?.SetParent(thisTransform);
    // }

    // public void RemoveChild(InteractiveGameObjectView interactiveGO)
    // {
    //     if (interactiveGO == null) return;

    //     interactiveGO.transform.SetParent(thisTransform.parent);
    // }

    // public void ClickIndicatorAxis(string axisGame)
    // {
    //     indicatorHandle.SetIndicatorAxis(axisGame);
    // }

    // public void DragDeltaIndicatorAxis(Vector3 deltaPos)
    // {
    //     indicatorHandle.DragDeltaIndicatorAxis(deltaPos);
    // }
}
