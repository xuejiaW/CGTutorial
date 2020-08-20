using UnityEngine;

public class IndicatorScalingHandle : IndicatorHandleBase
{
    private Vector3 scalingDirection = Vector3.zero;
    private float axisCoefficient = 1.0f;

    // Use global parameters in order to avoid allocating memory in Function DragIndicatorAxis
    private Vector3 targetGOTransScale = Vector3.zero;
    private Vector3 axisStartPointWorld = Vector3.zero;
    private Vector3 axisEndPointWorld = Vector3.zero;
    private Vector3 axisStartPointScreen = Vector3.zero;
    private Vector3 axisEndPointScreen = Vector3.zero;
    private Vector2 axisDeltaScreen = Vector2.zero;
    private float projectionValue = 0.0f;

    public override void SetIndicator(InteractiveIndicatorController indicator)
    {
        base.SetIndicator(indicator);
    }

    public override void SetIndicatorAxis(string axis)
    {
        base.SetIndicatorAxis(axis);
        Debug.Log("Axis is " + axis);

        if (axis == "XAxis")
            scalingDirection = Vector3.right;
        if (axis == "YAxis")
            scalingDirection = Vector3.up;
        if (axis == "ZAxis")
            scalingDirection = -Vector3.forward; //OpenGL has reverse world direction with Unity

        axisCoefficient = scalingDirection.GetAxisSum();
    }

    public override void DragDeltaIndicatorAxis(Vector3 dragDeltaScreen)
    {
        if (dragDeltaScreen == Vector3.zero) return;
        dragDeltaScreen *= InteractiveIndicatorCollection.Instance.indicatorSensitive;

        // to calculate selected axis's delta position from startPoint to endPoint in screen coordinate
        axisStartPointWorld = indicatorAxisTrans.position + scalingDirection;
        axisEndPointWorld = indicatorAxisTrans.position - scalingDirection;
        axisStartPointScreen = MainManager.Instance.worldCamera.WorldToViewportPoint(axisStartPointWorld);
        axisEndPointScreen = MainManager.Instance.worldCamera.WorldToViewportPoint(axisEndPointWorld);
        axisDeltaScreen = axisStartPointScreen - axisEndPointScreen;    //axis start/end point position delta in screen coordinate

        // project dragDeltaPosScreen on axisDeltaScreen, use the result as the coefficient
        // e.g axisDeltaScreen -> (0,1,0) && dragDeltaScreen -> (1,0,0) ==> projectionValue -> 0 ==> should not move target
        projectionValue = Vector3.Dot(dragDeltaScreen, axisDeltaScreen);

        // move targetTranspos
        targetGOTransScale = targetModel.localScale;
        targetGOTransScale += projectionValue * scalingDirection * axisCoefficient * 0.1f;
        targetGOTransScale.Clamp(0.05f, float.MaxValue);
        targetModel.localScale = targetGOTransScale;
    }


}