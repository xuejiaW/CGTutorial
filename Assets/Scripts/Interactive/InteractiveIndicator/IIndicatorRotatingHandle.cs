using UnityEngine;

public class IndicatorRotatingHandle : IIndicatorHandle
{
    private InteractiveIndicator indicator;
    private Transform indicatorAxisTrans = null;
    private Transform targetGOTrans = null;
    private Vector3 rotatingAxis = Vector3.zero;

    // Use global parameters in order to avoid allocating memory in Function DragIndicatorAxis
    private Vector3 targetGOTransRot = Vector3.zero;

    public void SetIndicator(InteractiveIndicator indicator)
    {
        this.indicator = indicator;
    }

    public void SetIndicatorAxis(string axis)
    {
        indicatorAxisTrans = indicator?.transform.Find(axis);
        targetGOTrans = indicator?.attachedGameObject.transform;

        rotatingAxis = Vector3.Normalize(indicatorAxisTrans.up);

        targetGOTransRot = targetGOTrans.eulerAngles;
    }

    public void DragIndicatorAxis(Vector3 dragDeltaScreen)
    {
        if (dragDeltaScreen == Vector3.zero) return;

        targetGOTrans.Rotate(rotatingAxis, 1, Space.World); //Ok, TODO: need to calculate coefficient
    }
}