using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveIndicatorCollection : Singleton<InteractiveIndicatorCollection>
{
    // private Dictionary<InteractiveMethod, string> stateIndicatorPrefabDict = null;

    private InteractiveIndicator[] indicatorArray = null;
    public InteractiveIndicator this[InteractiveMethod state]
    {
        get
        {
            int index = (int)state;
            return indicatorArray[index];
        }
    }

    private InteractiveIndicator currentIndicator = null;

    public override void Init()
    {
        base.Init();

        indicatorArray = new InteractiveIndicator[3]
        {
            GameResourceManager.Instance.Instantiate("InteractiveIndicators/Moving").
                        GetComponent_AutoAdd<InteractiveIndicator>().SetHandle(new IndicatorMovingHandle()),
            GameResourceManager.Instance.Instantiate("InteractiveIndicators/Rotating").
                        GetComponent_AutoAdd<InteractiveIndicator>().SetHandle(new IndicatorRotatingHandle()),
            GameResourceManager.Instance.Instantiate("InteractiveIndicators/Scaling").
                        GetComponent_AutoAdd<InteractiveIndicator>().SetHandle(new IndicatorRotatingHandle())
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

        if (goName.IndexOf("Axis") != -1)
            currentIndicator.ClickIndicatorAxis(goName);
    }

    public void OnDragDeltaIndicator(Vector3 deltaPos)
    {
        currentIndicator.DragDeltaIndicatorAxis(deltaPos);
    }
}
