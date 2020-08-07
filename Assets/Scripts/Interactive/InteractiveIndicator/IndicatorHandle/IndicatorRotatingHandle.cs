using UnityEngine;

public class IndicatorRotatingHandle : IndicatorHandleBase
{
    private Vector3 rotatingAxis = Vector3.zero;

    // Use global parameters in order to avoid allocating memory in Function DragIndicatorAxis
    private Vector3 deltaWithoutRotAxisComp = Vector3.zero;

    public override void SetIndicator(InteractiveIndicatorController indicator)
    {
        base.SetIndicator(indicator);
    }

    public override void SetIndicatorAxis(string axis)
    {
        base.SetIndicatorAxis(axis);

        rotatingAxis = Vector3.Normalize(indicatorAxisTrans.up);
    }

    public override void DragDeltaIndicatorAxis(Vector3 dragDeltaScreen)
    {
        if (dragDeltaScreen == Vector3.zero) return;
        dragDeltaScreen *= InteractiveIndicatorCollection.Instance.indicatorSensitive;

        // the component along the rotating Axis in dragDeltaScreen should be deleted
        deltaWithoutRotAxisComp = dragDeltaScreen - Vector3.Dot(dragDeltaScreen, rotatingAxis) * rotatingAxis;
        targetModel.localRotation = Quaternion.AngleAxis(deltaWithoutRotAxisComp.GetAxisSum(), rotatingAxis) * targetModel.localRotation;
    }
}