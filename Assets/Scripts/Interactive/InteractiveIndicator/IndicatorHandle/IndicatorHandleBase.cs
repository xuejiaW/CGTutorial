using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IndicatorHandleBase
{
    protected InteractiveIndicatorController indicator = null;
    protected Transform indicatorAxisTrans = null;
    protected DisplayableEntityModel targetModel = null;

    public virtual void SetIndicator(InteractiveIndicatorController indicator)
    {
        this.indicator = indicator;
    }

    public virtual void SetIndicatorAxis(string axis)
    {
        indicatorAxisTrans = indicator?.model.view.transform.Find(axis);
        targetModel = indicator?.model;
    }

    public virtual void DragDeltaIndicatorAxis(Vector3 dragDeltaScreen) { }

    public virtual void DragPosIndicatorAxis(Vector3 startPos, Vector3 currentPos) { }
}
