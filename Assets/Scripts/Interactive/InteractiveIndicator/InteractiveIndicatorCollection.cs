using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveIndicatorCollection : Singleton<InteractiveIndicatorCollection>
{
    private Dictionary<InteractiveState, string> stateIndicatorPrefabDict = null;

    private InteractiveIndicator[] indicatorArray = null;
    public InteractiveIndicator this[InteractiveState state]
    {
        get
        {
            int index = (int)state;

            if (indicatorArray[index] == null)
                indicatorArray[index] = GameResourceManager.Instance.Instantiate(stateIndicatorPrefabDict[state])
                                        .GetComponent_AutoAdd<InteractiveIndicator>();
            return indicatorArray[index];
        }
    }

    private InteractiveIndicator currentIndicator = null;

    protected override void Init()
    {
        base.Init();

        indicatorArray = new InteractiveIndicator[3];

        stateIndicatorPrefabDict = new Dictionary<InteractiveState, string>()
        {
            {InteractiveState.MOVING,"InteractiveIndicators/Moving"},
            {InteractiveState.ROTATING,"InteractiveIndicators/Rotating"},
            {InteractiveState.SCALING,"InteractiveIndicators/Scaling"}
        };

        InteractiveManager.Instance.OnInteractiveStateUpdated += OnInteractiveStateUpdated;
        InteractiveGameObjectCollection.Instance.OnHoldingInteractiveGOUpdated += OnHoldingInteractiveGOUpdated;
        MouseInputManager.Instance.RegisterClickDownMessageHandle(0, OnClickInteractiveIndicator, LayerMask.GetMask("InteractiveIndicator"));
        MouseInputManager.Instance.RegisterDragMessageHandle(0, OnDrayHandle, LayerMask.GetMask("InteractiveIndicator"));
    }

    private void OnHoldingInteractiveGOUpdated(InteractiveGameObject oldInteractiveGo, InteractiveGameObject newInteractiveGO)
    {
        currentIndicator = this[InteractiveManager.Instance.interactiveState];
        currentIndicator.SetParent(newInteractiveGO);
    }

    public void OnInteractiveStateUpdated(InteractiveState state)
    {
        currentIndicator?.SetParent(null);

        currentIndicator = this[state];
        currentIndicator.SetParent(InteractiveGameObjectCollection.Instance.holdingInteractiveGo);
    }

    private void OnClickInteractiveIndicator(GameObject Go)
    {
        string goName = Go.name;
        Debug.Log("go name is " + goName);

        if (goName.IndexOf("Axis") != -1)
            currentIndicator.ClickIndicatorAxis(goName);
    }

    private void OnDrayHandle(Vector3 deltaPos)
    {
        currentIndicator.DragIndicatorAxis(deltaPos);
    }
}
