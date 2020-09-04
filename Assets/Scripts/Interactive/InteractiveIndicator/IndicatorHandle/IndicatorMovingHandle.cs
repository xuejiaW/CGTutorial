using UnityEngine;

public class IndicatorMovingHandle : IndicatorHandleBase
{
    private Vector3 movingDirection = Vector3.zero;

    // Use global parameters in order to avoid allocating memory in Function DragIndicatorAxis
    private Vector3 targetGOTransPos = Vector3.zero;
    private Vector3 axisStartPointWorld = Vector3.zero;
    private Vector3 axisEndPointWorld = Vector3.zero;
    private Vector3 axisStartPointViewport = Vector3.zero;
    private Vector3 axisEndPointViewport = Vector3.zero;
    private Vector3 axisDeltaViewport = Vector2.zero;
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

        //World-View space only use half width and half height
        dragDeltaScreen[0] /= (Screen.width / 2); // To viewport coordinate
        dragDeltaScreen[1] /= (Screen.height / 2);

        // to calculate selected axis's delta position from startPoint to endPoint in screen coordinate
        axisStartPointWorld = indicatorAxisTrans.position + movingDirection;
        axisEndPointWorld = indicatorAxisTrans.position - movingDirection;
        axisStartPointViewport = MainManager.Instance.worldCamera.WorldToViewportPoint(axisStartPointWorld);
        axisEndPointViewport = MainManager.Instance.worldCamera.WorldToViewportPoint(axisEndPointWorld);
        axisDeltaViewport = Vector3.Normalize(axisStartPointViewport - axisEndPointViewport);    //axis start/end point position delta in viewport coordinate

        // project dragDeltaPosScreen on axisDeltaScreen, use the result as the coefficient
        // e.g axisDeltaScreen -> (0,1,0) && dragDeltaScreen -> (1,0,0) ==> projectionValue -> 0 ==> should not move target
        projectionValue = Vector3.Dot(dragDeltaScreen, axisDeltaViewport);
        axisDeltaViewport *= projectionValue;

        Vector3 position1 = MainManager.Instance.worldCamera.ViewportToWorldPoint(axisStartPointViewport);
        Vector3 position2 = MainManager.Instance.worldCamera.ViewportToWorldPoint(axisStartPointViewport + axisDeltaViewport);

        // move targetTranspos
        targetGOTransPos = targetModel.localPosition;
        targetGOTransPos += (position2 - position1);
        targetModel.localPosition = targetGOTransPos;

    }

}
