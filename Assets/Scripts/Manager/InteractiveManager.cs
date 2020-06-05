using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class InteractiveManager : Singleton<InteractiveManager>, IMainUpdateObserver
{
    public InteractiveState interactiveState { get; private set; }
    public event System.Action<InteractiveState> OnInteractiveStateUpdated = null;

    protected override void Init()
    {
        base.Init();

        MainManager.Instance.RegisterObserver(this);

        //Call Instance to ensure these singletons are created
        InteractiveIndicatorCollection.Instance.GetType();
        InteractiveGameObjectCollection.Instance.GetType();
    }

    public void Update()
    {
        if (Input.GetKeyDown("w"))
            SetTransformState(InteractiveState.MOVING);
        else if (Input.GetKeyDown("e"))
            SetTransformState(InteractiveState.ROTATING);
        else if (Input.GetKeyDown("r"))
            SetTransformState(InteractiveState.SCALING);
    }

    public void SetTransformState(InteractiveState state)
    {
        interactiveState = state;
        OnInteractiveStateUpdated?.Invoke(interactiveState);
    }
}