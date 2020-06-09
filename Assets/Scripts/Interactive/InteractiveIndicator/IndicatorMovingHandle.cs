using UnityEngine;

public class IndicatorMovingHandle : IIndicatorHandle
{
    private InteractiveIndicator indicator;
    private Transform axisTrans = null;
    private Transform targetTrans = null;

    //Use global parameters in order to avoid allocating memory in Function DragIndicatorAxis
    private Vector3 targetTransPos = Vector3.zero;
    private Vector3 axisTransForward = Vector3.zero;
    private Vector3 axisStartPointWorld = Vector3.zero;
    private Vector3 axisEndPointWorld = Vector3.zero;
    private Vector3 axisStartPointScreen = Vector3.zero;
    private Vector3 axisEndPointScreen = Vector3.zero;
    private Vector2 axisDeltaScreen = Vector2.zero;
    private float projectionValue = 0.0f;


    public void SetIndicator(InteractiveIndicator indicator)
    {
        this.indicator = indicator;
    }

    public void SetIndicatorAxis(string axis)
    {
        axisTrans = indicator?.transform.Find(axis);
        axisTransForward = -axisTrans.forward;

        targetTrans = indicator?.attachedGameObject.transform;
    }
    public void DragIndicatorAxis(Vector3 dragDeltaScreen)
    {
        axisStartPointWorld = axisTrans.position + axisTransForward;
        axisEndPointWorld = axisTrans.position - axisTransForward;

        axisStartPointScreen = MainManager.Instance.viewCamera.WorldToViewportPoint(axisStartPointWorld);
        axisEndPointScreen = MainManager.Instance.viewCamera.WorldToViewportPoint(axisEndPointWorld);
        axisDeltaScreen = axisStartPointScreen - axisEndPointScreen;    //axis start/end point position delta in screen coordinate

        //project dragDeltaPosScreen on axisDeltaScreen, use the result as the coefficient
        //e.g axisDeltaScreen -> (0,1,0) && dragDeltaScreen -> (1,0,0) ==> projectionValue -> 0 ==> should not move target
        projectionValue = Vector3.Dot(dragDeltaScreen, axisDeltaScreen);
        targetTransPos = targetTrans.position;
        targetTransPos += projectionValue * axisTransForward;
        targetTrans.position = targetTransPos;

        //FIXME: Should limit movement range within the camera view range
    }

}
