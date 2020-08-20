using UnityEngine;

public class IndicatorMovingHandle : IndicatorHandleBase
{
    private Vector3 movingDirection = Vector3.zero;

    // Use global parameters in order to avoid allocating memory in Function DragIndicatorAxis
    private Vector3 targetGOTransPos = Vector3.zero;
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
        movingDirection = -indicatorAxisTrans.forward;
    }

    public override void DragDeltaIndicatorAxis(Vector3 dragDeltaScreen)
    {
        if (dragDeltaScreen == Vector3.zero) return;

        dragDeltaScreen *= InteractiveIndicatorCollection.Instance.indicatorSensitive;

        // to calculate selected axis's delta position from startPoint to endPoint in screen coordinate
        axisStartPointWorld = indicatorAxisTrans.position + movingDirection;
        axisEndPointWorld = indicatorAxisTrans.position - movingDirection;
        axisStartPointScreen = MainManager.Instance.worldCamera.WorldToViewportPoint(axisStartPointWorld);
        axisEndPointScreen = MainManager.Instance.worldCamera.WorldToViewportPoint(axisEndPointWorld);
        axisDeltaScreen = axisStartPointScreen - axisEndPointScreen;    //axis start/end point position delta in screen coordinate

        // project dragDeltaPosScreen on axisDeltaScreen, use the result as the coefficient
        // e.g axisDeltaScreen -> (0,1,0) && dragDeltaScreen -> (1,0,0) ==> projectionValue -> 0 ==> should not move target
        projectionValue = Vector3.Dot(dragDeltaScreen, axisDeltaScreen);

        // move targetTranspos
        targetGOTransPos = targetModel.localPosition;
        targetGOTransPos += projectionValue * movingDirection;
        targetModel.localPosition = targetGOTransPos;

        //FIXME: Should limit movement range within the camera view range
    }

}
