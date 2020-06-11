using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class InteractiveManager : Singleton<InteractiveManager>
{
    public InteractiveMethod interactMethod { get; private set; }
    public event System.Action<InteractiveMethod> OnInteractMethodUpdated = null;

    private InteractiveIndicatorCollection indicatorManager = null;
    private InteractiveGameObjectCollection goManager = null;
    private InteractiveViewCamera viewCameraManager = null;

    private Dictionary<KeyCode, InteractiveMethod> keyCodeStateDict = null;

    //TODO: using key Q to turn on HandTool -> pan around the Screen
    private bool isInFlythroughMode = false;

    public override void Init()
    {
        base.Init();

        keyCodeStateDict = new Dictionary<KeyCode, InteractiveMethod>()
        {
            {KeyCode.W,InteractiveMethod.MOVING},
            {KeyCode.E,InteractiveMethod.ROTATING},
            {KeyCode.R,InteractiveMethod.SCALING},
        };
        indicatorManager = InteractiveIndicatorCollection.Instance;
        goManager = InteractiveGameObjectCollection.Instance;
        viewCameraManager = InteractiveViewCamera.Instance;

        #region  Interact InteractiveGameObject Operation Setting
        //Change interactive method on interactiveGameObject
        KeyboardInputManager.Instance.RegisterKeyDownMessageHandle(SetGOInteractMethod, KeyCode.W);
        KeyboardInputManager.Instance.RegisterKeyDownMessageHandle(SetGOInteractMethod, KeyCode.E);
        KeyboardInputManager.Instance.RegisterKeyDownMessageHandle(SetGOInteractMethod, KeyCode.R);

        //Select target interactiveGameObject
        MouseInputManager.Instance.RegisterClickDownMessageHandle(0, goManager.OnClickGameObject,
                                    LayerMask.GetMask("InteractiveGO"), -1);

        //Interact on selected interactiveGameObject
        MouseInputManager.Instance.RegisterClickDownMessageHandle(0, indicatorManager.OnClickIndicator,
                                    LayerMask.GetMask("InteractiveIndicator"));
        MouseInputManager.Instance.RegisterDragMessageHandle(0, indicatorManager.OnDragDeltaIndicator,
                                    LayerMask.GetMask("InteractiveIndicator"));
        #endregion

        #region  Flythrough Operation Setting
        //Enter flythrough mode and register related keycode
        MouseInputManager.Instance.RegisterClickDownMessageHandle(1, (go) =>
        {
            isInFlythroughMode = true;
            KeyboardInputManager.Instance.RegisterKeyDownMessageHandle(viewCameraManager.TurnOnFlythroughKey,
                                    KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
            KeyboardInputManager.Instance.RegisterKeyUpMessageHandle(viewCameraManager.TurnOffFlythroughKey,
                                    KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
            KeyboardInputManager.Instance.RegisterKeyMessageHandle(viewCameraManager.OnKeyPressedDown,
                                    KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
        }, -1);

        //Exit flythrough mode and unregister related keycode
        MouseInputManager.Instance.RegisterClickUpMessageHandle(1, (go) =>
        {
            isInFlythroughMode = false;
            KeyboardInputManager.Instance.UnRegisterKeyDownMessageHandle(viewCameraManager.TurnOnFlythroughKey,
                                    KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
            KeyboardInputManager.Instance.UnRegisterKeyUpMessageHandle(viewCameraManager.TurnOffFlythroughKey,
                                    KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
            KeyboardInputManager.Instance.UnRegisterKeyMessageHandle(viewCameraManager.OnKeyPressedDown,
                                    KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
        }, -1);
        #endregion

        #region ViewCamera Operation Setting
        //Modify viewCamera direction
        MouseInputManager.Instance.RegisterDragMessageHandle(1, viewCameraManager.OnMouseRightDrag, -1);
        #endregion
    }


    private void SetGOInteractMethod(KeyCode key)
    {
        if (isInFlythroughMode) return;
        interactMethod = keyCodeStateDict[key];
        OnInteractMethodUpdated?.Invoke(interactMethod);
    }
}