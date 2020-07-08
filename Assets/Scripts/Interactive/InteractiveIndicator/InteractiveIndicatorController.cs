using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveIndicatorController : DisplayableEntityController
{
    public IndicatorHandleBase indicatorHandle = null;

    public void AddChild(InteractiveGameObjectView interactiveGO)
    {
        // Make thisTransform have the same transform with the interactiveGO
        model.parent = interactiveGO?.transform;
        model.localRotation = Quaternion.identity;
        model.localPosition = Vector3.zero;
        model.localScale = interactiveGO == null ? Vector3.one : interactiveGO.transform.localScale.GetInverse();

        model.parent = interactiveGO?.transform?.parent;
        model.active = interactiveGO != null;

        //TODO: change to set model parent after modifying the interactiveGO Model
        interactiveGO?.transform?.SetParent(model.view.transform);
    }

    public void RemoveChild(InteractiveGameObjectView interactiveGO)
    {
        if (interactiveGO == null) return;

        //TODO: change to set model parent after modifying the interactiveGO Model
        interactiveGO.transform.SetParent(model.parent);
    }

    public void ClickIndicatorAxis(string axisGame)
    {
        indicatorHandle.SetIndicatorAxis(axisGame);
    }

    public void DragDeltaIndicatorAxis(Vector3 deltaPos)
    {
        indicatorHandle.DragDeltaIndicatorAxis(deltaPos);
    }

    public InteractiveIndicatorController SetHandle(IndicatorHandleBase handle)
    {
        indicatorHandle = handle;
        indicatorHandle.SetIndicator(this);
        return this;
    }
}
