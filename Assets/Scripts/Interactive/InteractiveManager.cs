using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class InteractiveManager : Singleton<InteractiveManager>
{
    public InteractiveState interactiveState { get; private set; }
    public event System.Action<InteractiveState> OnInteractiveStateUpdated = null;

    public override void Init()
    {
        base.Init();

        //Call Instance to ensure these singletons are created
        InteractiveIndicatorCollection.Instance.Init();
        InteractiveGameObjectCollection.Instance.Init();
        InteractiveViewCamera.Instance.Init();

        //TODO: Maybe should move all input handle in this class?

        KeyboardInputManager.Instance.RegisterKeyDownMessageHandle((key) => SetTransformState(InteractiveState.MOVING), KeyCode.W);
        KeyboardInputManager.Instance.RegisterKeyDownMessageHandle((key) => SetTransformState(InteractiveState.ROTATING), KeyCode.E);
        KeyboardInputManager.Instance.RegisterKeyDownMessageHandle((key) => SetTransformState(InteractiveState.SCALING), KeyCode.R);
    }

    public void SetTransformState(InteractiveState state)
    {
        interactiveState = state;
        OnInteractiveStateUpdated?.Invoke(interactiveState);
    }
}