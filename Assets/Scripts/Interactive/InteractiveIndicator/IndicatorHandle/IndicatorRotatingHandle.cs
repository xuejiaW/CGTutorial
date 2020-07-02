using UnityEngine;

public class IndicatorRotatingHandle : IndicatorHandleBase
{
    private Vector3 rotatingAxis = Vector3.zero;

    // Use global parameters in order to avoid allocating memory in Function DragIndicatorAxis
    private Vector3 targetGOTransRot = Vector3.zero;
    private Vector3 deltaNoRotAxisComp = Vector3.zero;

    public override void SetIndicator(InteractiveIndicatorView indicator)
    {
        base.SetIndicator(indicator);
    }

    public override void SetIndicatorAxis(string axis)
    {
        base.SetIndicatorAxis(axis);

        rotatingAxis = Vector3.Normalize(indicatorAxisTrans.up);
        targetGOTransRot = targetGOTrans.eulerAngles;
    }

    public override void DragDeltaIndicatorAxis(Vector3 dragDeltaScreen)
    {
        if (dragDeltaScreen == Vector3.zero) return;

        deltaNoRotAxisComp = dragDeltaScreen - Vector3.Dot(dragDeltaScreen, rotatingAxis) * rotatingAxis;
        targetGOTrans.Rotate(rotatingAxis, deltaNoRotAxisComp.GetAxisSum(), Space.World);
    }
}