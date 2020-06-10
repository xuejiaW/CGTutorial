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

        MouseInputManager.Instance.RegisterClickDownMessageHandle(1, onRightClickDownEmpty, -1);
        MouseInputManager.Instance.RegisterClickUpMessageHandle(1, onRightClickUpEmpty, -1);
        MouseInputManager.Instance.RegisterDragMessageHandle(1, onMouseRightDrag, -1);

        KeyboardInputManager.Instance.RegisterKeyMessageHandle(OnKeyPressedDown,
                             KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
    }

    private void onRightClickDownEmpty(GameObject go)
    {
        KeyboardInputManager.Instance.RegisterKeyDownMessageHandle(TurnKeyDownOn,
                             KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
        KeyboardInputManager.Instance.RegisterKeyUpMessageHandle(TurnKeyDownOff,
                             KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
    }

    private void onRightClickUpEmpty(GameObject go)
    {
        KeyboardInputManager.Instance.UnRegisterKeyDownMessageHandle(TurnKeyDownOn,
                             KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
        KeyboardInputManager.Instance.UnRegisterKeyUpMessageHandle(TurnKeyDownOff,
                             KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
    }

    private void onMouseRightDrag(Vector3 dragDeltaScreen)
    {
        currentViewCameraRot.x += dragDeltaScreen.x;
        currentViewCameraRot.y -= dragDeltaScreen.y;
        currentViewCameraRot.x = Mathf.Repeat(currentViewCameraRot.x, 360);
        currentViewCameraRot.y = Mathf.Clamp(currentViewCameraRot.y, -maxPitchDegree, maxPitchDegree);

        viewCameraTrans.rotation = Quaternion.Euler(currentViewCameraRot.y, currentViewCameraRot.x, 0);
    }

    private void TurnKeyDownOn(KeyCode code) { keycodesDownDict[code] = true; }
    private void TurnKeyDownOff(KeyCode code) { keycodesDownDict[code] = false; }

    private void OnKeyPressedDown(float deltaTime)
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
