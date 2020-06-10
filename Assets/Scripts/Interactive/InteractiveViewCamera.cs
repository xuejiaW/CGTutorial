using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveViewCamera : Singleton<InteractiveViewCamera>
{
    private Transform viewCameraTrans = null;
    private Vector2 currentViewCameraRot = Vector2.zero;
    private float maxPitchDegree = 85.0f;

    private Dictionary<KeyCode, bool> keycodesDownDict = null;

    public override void Init()
    {
        base.Init();
        keycodesDownDict = new Dictionary<KeyCode, bool>();

        MouseInputManager.Instance.RegisterClickDownMessageHandle(1, onRightClickDownEmpty, -1);
        MouseInputManager.Instance.RegisterClickUpMessageHandle(1, onRightClickUpEmpty, -1);
        MouseInputManager.Instance.RegisterDragMessageHandle(1, onMouseRightDrag, -1);

        viewCameraTrans = MainManager.Instance.viewCamera.transform;

        // KeyboardInputManager.Instance.RegisterKeyDownMessageHandle()
    }

    public void onRightClickDownEmpty(GameObject go)
    {
        Debug.Log("on right click down");
    }

    public void onRightClickUpEmpty(GameObject go)
    {
        Debug.Log("on right click up");
    }

    public void onMouseRightDrag(Vector3 dragDeltaScreen)
    {
        currentViewCameraRot.x += dragDeltaScreen.x;
        currentViewCameraRot.y -= dragDeltaScreen.y;
        currentViewCameraRot.x = Mathf.Repeat(currentViewCameraRot.x, 360);
        currentViewCameraRot.y = Mathf.Clamp(currentViewCameraRot.y, -maxPitchDegree, maxPitchDegree);

        viewCameraTrans.rotation = Quaternion.Euler(currentViewCameraRot.y, currentViewCameraRot.x, 0);
    }

}
