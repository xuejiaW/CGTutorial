using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveViewCamera : Singleton<InteractiveViewCamera>
{
    private float maxPitchDegree = 85.0f;
    private Transform viewCameraTrans = null;
    private Vector2 currentViewCameraRot = Vector2.zero;

    private Dictionary<KeyCode, bool> keycodesDownDict = null;

    private float cameraMoveSpeed = 5.0f;
    private Vector3 cameraDeltaPos = Vector3.zero;

    public override void Init()
    {
        base.Init();
        keycodesDownDict = new Dictionary<KeyCode, bool>() {
                            { KeyCode.Q, false },{KeyCode.W,false},{KeyCode.E,false},
                            {KeyCode.A,false},{KeyCode.S,false},{KeyCode.D,false}};

        viewCameraTrans = MainManager.Instance.viewCamera.transform;
        currentViewCameraRot = viewCameraTrans.eulerAngles;// ignore the roll angle

    }

    public void OnMouseRightDrag(Vector3 dragDeltaScreen)
    {
        currentViewCameraRot.x += dragDeltaScreen.x;
        currentViewCameraRot.y -= dragDeltaScreen.y;
        currentViewCameraRot.x = Mathf.Repeat(currentViewCameraRot.x, 360);
        currentViewCameraRot.y = Mathf.Clamp(currentViewCameraRot.y, -maxPitchDegree, maxPitchDegree);

        viewCameraTrans.rotation = Quaternion.Euler(currentViewCameraRot.y, currentViewCameraRot.x, 0);
    }

    public void TurnOnFlythroughKey(KeyCode code) { keycodesDownDict[code] = true; }
    public void TurnOffFlythroughKey(KeyCode code) { keycodesDownDict[code] = false; }

    public void OnKeyPressedDown(float deltaTime)
    {
        cameraDeltaPos = Vector3.zero;

        if (keycodesDownDict[KeyCode.E])
            cameraDeltaPos += +deltaTime * viewCameraTrans.up * cameraMoveSpeed;
        if (keycodesDownDict[KeyCode.Q])
            cameraDeltaPos += -deltaTime * viewCameraTrans.up * cameraMoveSpeed;

        if (keycodesDownDict[KeyCode.W])
            cameraDeltaPos += +deltaTime * viewCameraTrans.forward * cameraMoveSpeed;
        if (keycodesDownDict[KeyCode.S])
            cameraDeltaPos += -deltaTime * viewCameraTrans.forward * cameraMoveSpeed;

        if (keycodesDownDict[KeyCode.D])
            cameraDeltaPos += +deltaTime * viewCameraTrans.right * cameraMoveSpeed;
        if (keycodesDownDict[KeyCode.A])
            cameraDeltaPos += -deltaTime * viewCameraTrans.right * cameraMoveSpeed;

        viewCameraTrans.position += cameraDeltaPos;
    }
}
