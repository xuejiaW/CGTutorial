using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveIndicatorController : DisplayableEntityController
{
    public IndicatorHandleBase indicatorHandle = null;

    public void AddChild(DisplayableEntityModel interactiveGO)
    {
        // Make thisTransform have the same transform with the interactiveGO
        // in order to sync transform state
        model.parent = interactiveGO;
        model.localRotation = Quaternion.identity;
        model.localPosition = Vector3.zero;
        model.localScale = interactiveGO == null ? Vector3.one : interactiveGO.localScale.GetInverse();

        model.parent = interactiveGO?.parent;
        model.active = interactiveGO != null;

        interactiveGO?.SetParent(model);
    }

    public void RemoveChild(DisplayableEntityModel interactiveGO)
    {
        if (interactiveGO == null) return;

        interactiveGO?.SetParent(model?.parent);
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
