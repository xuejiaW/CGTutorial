using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    protected override void InitProcess()
    {
        base.InitProcess();
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

        //Select target interactiveGameObject
        MouseInputManager.Instance.RegisterClickDownMessageHandle(0, goManager.OnSelectGameObject,
                                    LayerMask.GetMask("InteractiveGO"), LayerMask.GetMask("InteractiveHelper"), -1); // -1 for empty gameObject
        //Interact on selected interactiveGameObject
        MouseInputManager.Instance.RegisterClickDownMessageHandle(0, indicatorManager.OnClickIndicator,
                                    LayerMask.GetMask("InteractiveIndicator"));
        MouseInputManager.Instance.RegisterDragMessageHandle(0, indicatorManager.OnDragDeltaIndicator,
                                    LayerMask.GetMask("InteractiveIndicator"));
        #endregion
    }

    public void RegisterInteractiveMethodSwitch()
    {
        KeyboardInputManager.Instance.RegisterKeyDownMessageHandle(SetGOInteractMethod, KeyCode.W);
        KeyboardInputManager.Instance.RegisterKeyDownMessageHandle(SetGOInteractMethod, KeyCode.E);
        KeyboardInputManager.Instance.RegisterKeyDownMessageHandle(SetGOInteractMethod, KeyCode.R);
    }

    public void UnRegisterInteractiveMethodSwitch()
    {
        KeyboardInputManager.Instance.UnRegisterKeyDownMessageHandle(SetGOInteractMethod, KeyCode.W);
        KeyboardInputManager.Instance.UnRegisterKeyDownMessageHandle(SetGOInteractMethod, KeyCode.E);
        KeyboardInputManager.Instance.UnRegisterKeyDownMessageHandle(SetGOInteractMethod, KeyCode.R);
    }

    public void RegisterFlythroughMode()
    {
        //Enter flythrough mode and register related keycode
        MouseInputManager.Instance.RegisterClickDownMessageHandle(1, EnterFlythroughMode, -1);
        //Exit flythrough mode and unregister related keycode
        MouseInputManager.Instance.RegisterClickUpMessageHandle(1, ExitFlythroughMode, 1);
        //Modify viewCamera direction
        MouseInputManager.Instance.RegisterDragMessageHandle(1, viewCameraManager.OnMouseRightDrag, -1);
    }

    public void UnRegisterFlythroughMode()
    {
        //Enter flythrough mode and register related keycode
        MouseInputManager.Instance.UnRegisterClickDownMessageHandle(1, EnterFlythroughMode, -1);
        //Exit flythrough mode and unregister related keycode
        MouseInputManager.Instance.UnRegisterClickUpMessageHandle(1, ExitFlythroughMode, 1);
        //Modify viewCamera direction
        MouseInputManager.Instance.UnRegisterDragMessageHandle(1, viewCameraManager.OnMouseRightDrag, -1);
    }

    private void EnterFlythroughMode(GameObject go) // the parameters only used to match event
    {
        isInFlythroughMode = true;
        KeyboardInputManager.Instance.RegisterKeyDownMessageHandle(viewCameraManager.TurnOnFlythroughKey,
                                KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
        KeyboardInputManager.Instance.RegisterKeyUpMessageHandle(viewCameraManager.TurnOffFlythroughKey,
                                KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
        KeyboardInputManager.Instance.RegisterKeyMessageHandle(viewCameraManager.OnKeyPressedDown,
                                KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
    }

    private void ExitFlythroughMode(GameObject go)// the parameters only used to match event 
    {

        isInFlythroughMode = false;
        KeyboardInputManager.Instance.UnRegisterKeyDownMessageHandle(viewCameraManager.TurnOnFlythroughKey,
                                KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
        KeyboardInputManager.Instance.UnRegisterKeyUpMessageHandle(viewCameraManager.TurnOffFlythroughKey,
                                KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
        KeyboardInputManager.Instance.UnRegisterKeyMessageHandle(viewCameraManager.OnKeyPressedDown,
                                KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D);
    }


    private void SetGOInteractMethod(KeyCode key)
    {
        if (isInFlythroughMode) return;
        interactMethod = keyCodeStateDict[key];
        OnInteractMethodUpdated?.Invoke(interactMethod);
    }
}