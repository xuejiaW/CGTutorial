using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IndicatorHandleBase
{
    protected InteractiveIndicatorView indicator = null;
    protected Transform indicatorAxisTrans = null;
    protected Transform targetGOTrans = null;

    public virtual void SetIndicator(InteractiveIndicatorView indicator)
    {
        this.indicator = indicator;
    }

    public virtual void SetIndicatorAxis(string axis)
    {
        indicatorAxisTrans = indicator?.transform.Find(axis);
        targetGOTrans = indicator?.transform;
    }

    public virtual void DragDeltaIndicatorAxis(Vector3 dragDeltaScreen) { }

    public virtual void DragPosIndicatorAxis(Vector3 startPos, Vector3 currentPos) { }
}
