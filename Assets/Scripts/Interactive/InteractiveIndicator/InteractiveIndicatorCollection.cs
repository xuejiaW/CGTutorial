using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveIndicatorCollection : Singleton<InteractiveIndicatorCollection>
{
    private Dictionary<InteractiveMethod, string> stateIndicatorPrefabDict = null;

    private InteractiveIndicator[] indicatorArray = null;
    public InteractiveIndicator this[InteractiveMethod state]
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

    public override void Init()
    {
        base.Init();

        indicatorArray = new InteractiveIndicator[3];

        stateIndicatorPrefabDict = new Dictionary<InteractiveMethod, string>()
        {
            {InteractiveMethod.MOVING,"InteractiveIndicators/Moving"},
            {InteractiveMethod.ROTATING,"InteractiveIndicators/Rotating"},
            {InteractiveMethod.SCALING,"InteractiveIndicators/Scaling"}
        };

        InteractiveManager.Instance.OnInteractMethodUpdated += OnInteractiveStateUpdated;
        InteractiveGameObjectCollection.Instance.OnHoldingInteractiveGOUpdated += OnHoldingInteractiveGOUpdated;
    }

    private void OnHoldingInteractiveGOUpdated(InteractiveGameObject oldInteractiveGo, InteractiveGameObject newInteractiveGO)
    {
        currentIndicator = this[InteractiveManager.Instance.interactMethod];
        currentIndicator.SetParent(newInteractiveGO);
    }

    public void OnInteractiveStateUpdated(InteractiveMethod state)
    {
        currentIndicator?.SetParent(null);

        currentIndicator = this[state];
        currentIndicator.SetParent(InteractiveGameObjectCollection.Instance.holdingInteractiveGo);
    }

    public void OnClickIndicator(GameObject Go)
    {
        string goName = Go.name;
        Debug.Log("go name is " + goName);

        if (goName.IndexOf("Axis") != -1)
            currentIndicator.ClickIndicatorAxis(goName);
    }

    public void OnDragIndicator(Vector3 deltaPos)
    {
        currentIndicator.DragIndicatorAxis(deltaPos);
    }
}
